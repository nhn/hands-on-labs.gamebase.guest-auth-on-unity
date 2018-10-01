#import "TCGBPurchasePlugin.h"
#import "DelegateManager.h"
#import <Gamebase/Gamebase.h>
#import "TCGBUnityDictionaryJSON.h"
#import "UnityMessageSender.h"
#import "UnityMessage.h"
#import "NativeMessage.h"

#define PURCHASE_API_REQUEST_PURCHASE                       @"gamebase://requestPurchase"
#define PURCHASE_API_REQUEST_ITEM_LIST_OF_NOT_CONSUMED      @"gamebase://requestItemListOfNotConsumed"
#define PURCHASE_API_REQUEST_RETYR_TRANSACTION              @"gamebase://requestRetryTransaction"
#define PURCHASE_API_REQUEST_ITEM_LIST_PURCHASABLE          @"gamebase://requestItemListPurchasable"
#define PURCHASE_API_REQUEST_ITEM_LIST_AT_AP_CONSOLE        @"gamebase://requestItemListAtIAPConsole"
#define PURCHASE_API_SET_STORE_CODE                         @"gamebase://setStoreCode"
#define PURCHASE_API_GET_STORE_CODE                         @"gamebase://getStoreCode"


@implementation TCGBPurchasePlugin

- (instancetype)init {
    if ((self = [super init]) == nil) {
        return nil;
    }
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:PURCHASE_API_REQUEST_PURCHASE target:self selector:@selector(requestPurchase:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:PURCHASE_API_REQUEST_ITEM_LIST_OF_NOT_CONSUMED target:self selector:@selector(requestItemListOfNotConsumed:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:PURCHASE_API_REQUEST_RETYR_TRANSACTION target:self selector:@selector(requestRetryTransaction:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:PURCHASE_API_REQUEST_ITEM_LIST_PURCHASABLE target:self selector:@selector(requestItemListPurchasable:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:PURCHASE_API_REQUEST_ITEM_LIST_AT_AP_CONSOLE target:self selector:@selector(requestItemListAtIAPConsole:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:PURCHASE_API_SET_STORE_CODE target:self selector:@selector(setStoreCode:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:PURCHASE_API_GET_STORE_CODE target:self selector:@selector(getStoreCode:)];
    
    return self;
}

-(void)requestPurchase:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    [TCGBPurchase requestPurchaseWithItemSeq:[convertedDic[@"itemSeq"] longValue] viewController:UnityGetGLViewController() completion:^(TCGBPurchasableReceipt *purchasableReceipt, TCGBError *error) {
        
        NSMutableDictionary* jsonDic = [NSMutableDictionary dictionary];
        if(purchasableReceipt != nil)
        {
            jsonDic[@"itemSeq"] = [NSNumber numberWithLong:purchasableReceipt.itemSeq];
            jsonDic[@"price"] = [NSNumber numberWithLong:purchasableReceipt.price];
            jsonDic[@"currency"] = purchasableReceipt.currency;
            jsonDic[@"paymentSeq"] = purchasableReceipt.paymentSeq;
            jsonDic[@"purchaseToken"] = purchasableReceipt.purchaseToken;
        }
        
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[jsonDic JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(void)requestItemListOfNotConsumed:(UnityMessage*)message {
    [TCGBPurchase requestItemListOfNotConsumedWithCompletion:^(NSArray<TCGBPurchasableReceipt *> *purchasableReceiptArray, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[purchasableReceiptArray JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(void)requestRetryTransaction:(UnityMessage*)message {
    [TCGBPurchase requestRetryTransactionWithCompletion:^(TCGBPurchasableRetryTransactionResult *transactionResult, TCGBError *error) {
        
        NSMutableDictionary* jsonDic = [NSMutableDictionary dictionary];
        if(transactionResult != nil)
        {
            jsonDic[@"successList"] = transactionResult.successList;
            jsonDic[@"failList"] = transactionResult.failList;
        }
        
        
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[jsonDic JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(void)requestItemListPurchasable:(UnityMessage*)message {
    [TCGBPurchase requestItemListPurchasableWithCompletion:^(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[purchasableItemArray JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(void)requestItemListAtIAPConsole:(UnityMessage*)message {
    [TCGBPurchase requestItemListAtIAPConsoleWithCompletion:^(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[purchasableItemArray JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(NSString*)setStoreCode:(UnityMessage*)message {
    [TCGBPurchase setStoreCode:message.jsonData];
    return @"";
}

-(NSString*)getStoreCode:(UnityMessage*)message {
    NSString *storeCode = [TCGBPurchase storeCode];
    
    return storeCode;
}
@end
