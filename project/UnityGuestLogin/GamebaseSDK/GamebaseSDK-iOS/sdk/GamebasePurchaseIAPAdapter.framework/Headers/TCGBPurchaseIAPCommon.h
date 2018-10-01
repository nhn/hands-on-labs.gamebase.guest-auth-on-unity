//
//  TCGBPurchaseIAPCommon.h
//  TCGBPurchaseIAPAdapter
//
//  Created by Park Panki on 2016. 6. 7..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <Gamebase/Gamebase.h>

#define GamebasePurchaseIAPAdapterVersion @"1.2.2"

@interface TCGBPurchaseIAPCommon : NSObject <TCGBPurchasable>

#pragma mark - protocol TCGBPurchasable
- (void)initializePurchaseWithAppID:(NSString *)appID store:(NSString *)store userID:(NSString *)userId enableDebugMode:(BOOL)isDebugMode;

- (void)requestPurchaseWithItemSeq:(long)itemSeq viewController:(UIViewController *)viewController completion:(void(^)(TCGBPurchasableReceipt *purchasableReceipt, TCGBError *error))completion;

- (void)requestItemListOfNotConsumedWithCompletion:(void(^)(NSArray<TCGBPurchasableReceipt *> *purchasableReceiptArray, TCGBError *error))completion;

- (void)requestRetryTransactionWithCompletion:(void(^)(TCGBPurchasableRetryTransactionResult *transactionResult, TCGBError *error))completion;

- (void)requestItemListPurchasableWithCompletion:(void(^)(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error))completion;

- (void)requestItemListAtIAPConsoleWithCompletion:(void(^)(NSArray<TCGBPurchasableItem *> *purchasableItemArray, TCGBError *error))completion;

+ (NSString *)versionString;

@end
