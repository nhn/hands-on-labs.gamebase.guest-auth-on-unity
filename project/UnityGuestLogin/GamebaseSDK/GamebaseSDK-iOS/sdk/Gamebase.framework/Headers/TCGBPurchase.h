//
//  TCGBPurchase.h
//  Gamebase
//
//  Created by Park Panki on 2016. 6. 8..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "TCGBError.h"
#import "TCGBConstants.h"
#import "TCGBPurchasable.h"

@class TCGBPurchasableItem;
@class TCGBPurchasableReceipt;
@class TCGBPurchasableRetryTransactionResult;

/** The TCGBPurchase class provides several APIs related to purchasing processes.<br/>
 * Before using these APIs, You should be logged in. Because every TCGBPurchase API need informations that are obtained from TCGBServer.<br/>
 * If you do not have been authenticated, you will get **TCGB_ERROR_NOT_LOGGED_IN** error.
 */
@interface TCGBPurchase : NSObject


/**---------------------------------------------------------------------------------------
 * @name Request Item List
 *  ---------------------------------------------------------------------------------------
 */

/** This is the primary method for obtaining ItemList which is registered at ToastCloud IAP Console and Apple Itunes Connect.
 
 Request a item list which is purchasable. This list has items which are registered in both Market(AppStore) and ToastCloud IAP Console.
 
 @param completion      completion may return the NSArray of TCGBPurchasableItem.<br/>
 If there is an error, TCGBError will be returned.
 @warning   You should call this method after ```logged in```, otherwise you will get **TCGB_ERROR_NOT_LOGGED_IN** error in the completion.
 
 ### Usage Example
 
    - (void)viewDidLoad {
        [TCGBPurchase requestItemListPurchasableWithCompletion:^(NSArray *purchasableItemArray, TCGBError *error) {
            NSMutableArray *itemArrayMutable = [[NSMutableArray alloc] init];
            [purchasableItemArray enumerateObjectsUsingBlock:^(id  _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
                TCGBPurchasableItem *item = (TCGBPurchasableItem *)obj;
                 
                [itemArrayMutable addObject:item];
            }];
        }];
    }
 
 */
+ (void)requestItemListPurchasableWithCompletion:(void(^)(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error))completion;


/*! This is the method for obtaining ItemList which is registered at ToastCloud IAP Console.
 
 Request a item list which is purchasable. This list has items which are only registered in ToastCloud IAP Console, not Market(AppStore)
 
 @param completion      completion may return the NSArray of TCGBPurchasableItem.<br/>
 If there is an error, TCGBError will be returned.
 @warning   You should call this method after ```logged in```, otherwise you will get **TCGB_ERROR_NOT_LOGGED_IN** error in the completion.
 
 ### Usage Example
 
    - (void)viewDidLoad {
        [TCGBPurchase requestItemListAtIAPConsoleWithCompletion:^(NSArray *purchasableItemArray, TCGBError *error) {
            NSMutableArray *itemArrayMutable = [[NSMutableArray alloc] init];
            [purchasableItemArray enumerateObjectsUsingBlock:^(id  _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
                TCGBPurchasableItem *item = (TCGBPurchasableItem *)obj;
                [itemArrayMutable addObject:item];
            }];
        }];
    }
 
 */
+ (void)requestItemListAtIAPConsoleWithCompletion:(void(^)(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error))completion;


/**---------------------------------------------------------------------------------------
 * @name Request Purchasing Item
 *  ---------------------------------------------------------------------------------------
 */

/** This is the method to request purchasing item which identifier is itemSeq. There is a viewController parameter and you may put your top most viewController. If you don't, this method will find out top most view controller and put it in the parameter.
 
 Request Purchasing Item that has itemId.
 
 @param itemSeq         itemID which you want to purchase.
 @param viewController  represent to current viewcontroller.
 @param completion      completion may return the TCGBPurchasableReceipt instance.<br/>
 If there is an error, TCGBError will be returned.
 @warning   You should call this method after ```logged in```, otherwise you will get **TCGB_ERROR_NOT_LOGGED_IN** error in the completion.
 
 ### Usage Example

    - (void)purchasingItem:(long)itemSeq {
        [TCGBPurchase requestPurchaseWithItemSeq:itemSeq viewController:self completion:^(TCGBPurchasableReceipt *purchasableReceipt, TCGBError *error) {
            if (error.code == TCGB_ERROR_PURCHASE_USER_CANCELED) {
                dispatch_async(dispatch_get_main_queue(), ^{
                    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Item Purchase"
                                                                        message:[NSString stringWithFormat:@"You Canceled."]
                                                                       delegate:nil
                                                              cancelButtonTitle:@"OK"
                                                              otherButtonTitles:nil, nil];
         
                    [alertView show];
                });
            } else if (error) {
                dispatch_async(dispatch_get_main_queue(), ^{
                    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Item Purchase"
                                             message:[NSString stringWithFormat:@"There was an Error : %ld", (long)error.code]
                                                                       delegate:nil
                                                              cancelButtonTitle:@"OK"
                                                              otherButtonTitles:nil, nil];
         
                    [alertView show];
                });
            }
        }];
    }
 
 */
+ (void)requestPurchaseWithItemSeq:(long)itemSeq viewController:(UIViewController *)viewController completion:(void(^)(TCGBPurchasableReceipt *purchasableReceipt, TCGBError *error))completion;


/**---------------------------------------------------------------------------------------
 * @name Request non-consumed Item List
 *  ---------------------------------------------------------------------------------------
 */

/** This method provides an item list that have non-consumed.
 
 Request a Item List which is not consumed. You should deliver this itemReceipt to your game server to consume it or request consuming API to ToastCloud IAP Server. You may call this method after logged in and deal with these non-consumed items.
 
 @param completion      completion may return the NSArray of TCGBPurchasableReceipt instances.<br/>
 This instance has the paymentSequence, itemSequence, PurchaseToken information.<br/>
 If there is an error, TCGBError will be returned.
 @warning   You should call this method after ```logged in```, otherwise you will get **TCGB_ERROR_NOT_LOGGED_IN** error in the completion.
 
 ### Usage Example

    - (void)viewDidLoad {
        [TCGBPurchase requestItemListOfNotConsumedWithCompletion:^(NSArray<TCGBPurchasableReceipt *> *purchasableReceiptArray, TCGBError *error) {
            // should deal with this not-consumed items.
        }];
    }
 
 */
+ (void)requestItemListOfNotConsumedWithCompletion:(void(^)(NSArray<TCGBPurchasableReceipt *> *purchasableReceiptArray, TCGBError *error))completion;


/**---------------------------------------------------------------------------------------
 * @name Request retry purchasing processes
 *  ---------------------------------------------------------------------------------------
 */

/** This method is for retrying failed purchasing proccesses.
 
 Request a retrying transaction which is not completed to IAP Server
 
 @param completion      completion may return the TCGBPurchasableRetryTransactionResult which has two member variables which are named 'successList' and 'failList'.<br/>
 These two variables are array of TCGBPurchasableReceipt.<br/>
 Each key has a list of TCGBPurchasableReceipt that has uncompleted purchase information.<br/>
 If there is an error, TCGBError will be returned.
  
 ### Usage Example

    - (void)viewDidLoad {
        [TCGBPurchase requestRetryTransactionWithCompletion:^(TCGBPurchasableRetryTransactionResult *transactionResult, TCGBError *error) {
            // should deal with this retry transaction result.
            // if succeeded, you may send result to your gameserver and add item to user.
        }];
    }
 
 */
+ (void)requestRetryTransactionWithCompletion:(void(^)(TCGBPurchasableRetryTransactionResult *transactionResult, TCGBError *error))completion;



/**---------------------------------------------------------------------------------------
 * @name Settter and Getter Store
 *  ---------------------------------------------------------------------------------------
 */

/** This is a method for set up the store. It should be available StoreCode such as "AS". In normal cases, This method is not needed. Most of application uses "AS"(Apple AppStore) as a store.
 If not, you should set this value for using a specific store.
 
 Set a specific store for access this store.
 
 @param storeCode      storeCode represents store that item is registered and where you can buy item.
 */
+ (void)setStoreCode:(NSString *)storeCode;


/** This method returns a store code that is set up.
 
 @return storecode that is already set up.
 */
+ (NSString *)storeCode;


@end
