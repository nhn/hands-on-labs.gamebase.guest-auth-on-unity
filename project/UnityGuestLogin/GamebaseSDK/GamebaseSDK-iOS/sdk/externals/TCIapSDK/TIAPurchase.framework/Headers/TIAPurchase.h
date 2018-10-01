//
//  TIAPurchase.h
//  TOAST In App Purchase
//
//  Created by NHNENT on 2014. 10. 22..
//  Copyright (c) 2014년 NHN Entertainment. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

/** @constant TOAST In App Purchase SDK Version */
#define TIA_PURCHASE_SDK_VERSION_STRING @"1.5.6"

/** @constant TOAST In App Purchase Error Domain  */
extern NSString *const TIAPurchaseErrorDomain;

/*!
 @typedef TIAPurchaseErrorCode enum
 
 Describes what error code (if any)
 */
typedef NS_ENUM(NSUInteger, TIAPurchaseErrorCode) {
    /** Network timeout error */
    TIAErrorNetworkTimeout              = 100,
    /** Should be register user identification */
    TIAErrorAuthorization               = 101,
    /** Unsupported device */
    TIAErrorUnsupportedDevice           = 102,
    /** Unsupported market */
    TIAErrorUnsupportedStore            = 103,
    /** User canceled */
    TIAErrorUserCanceled                = 104,
    /** Initialized error */
    TIAErrorInitializedError            = 105,
    /** HTTP response code is not 20x */
    TIAErrorServerUnknownError          = 106,
    /** API response is failure */
    TIAErrorApiResponseError            = 107,
    /** App Store occured error */
    TIAErrorAppStoreError               = 108,
    /** App Store occured purchase process error */
    TIAErrorAppStorePurchaseError       = 109,
    /** Cash was insufficient for ONGATE */
    TIAErrorOnGateCashError             = 115,
    /** Connection fail to Store in IAP Server */
    TIAErrorConnectionFail              = 113,
    /** App Store remains paymentQueue. retry purchase */
    TIARetryPurchase                    = 116
};

/*!
 A block that to register for a callback with the results of that request once the connection completes.
 
 @discussion
 The call occurs on the UI Thread.
 
 @param result  The result of the request. This is a translation of JSON data to 'NSDictionany' and 'NSArray' objects.
 This is nil if there was an error.
 
 @param error   The 'NSError' representing any error that occurred.
 */
typedef void(^TIARequestHandler)(id result, NSError *error);


/*!
 The 'TIAPurchase' represents a single connection to In App Purchase to service a request.
 */
@interface TIAPurchase : NSObject

/*!
Enable debug mode

@param isDebuggable if isDebuggable value is true, Enable to debug mode
 
 */
+ (void) setDebugMode:(BOOL) isDebuggable;

/*!
 Simple method to regist appId
 
 @param appId   represent appId which is registered in Web Console
 
 @param error   The 'NSError' representing any error that occured
 
 */
+ (BOOL) registerAppId:(NSString *)appId error:(NSError **)error;

/*!
 Simple method to regist store id
 
 @param storeId   represent storeId. you can register 'AS' or 'ONGATE'
 
 @param error   The 'NSError' representing any error that occured
 
 */
+ (BOOL) registerStoreId:(NSString *)storeId error:(NSError **)error;

/*!
 Simple method to server phase
 
 @param serverPhase   represent server phase. you can register 'ALPHA' or 'BETA' or 'REAL' (default. REAL)
 
 @param error   The 'NSError' representing any error that occured
 
 */
+ (BOOL) registerServerPhase:(NSString *)serverPhase error:(NSError **)error;

/*!
 Simple method to register user identification.
 
 @param userId  represent to user identification
 
 @param error   The 'NSError' representing any error that occurred.
 */
+ (BOOL) registerUserId:(NSString *)userId
                  error:(NSError **)error;
    
/*!
 Starts a request for purchase process
 
 @param baseViewController  represent to current view controller
 
 @param itemId              represent to item id
 
 @param handler             handler's result has that translation of JSON data to 'NSDictionany'
 
 @return [result data]
 @return paymentSeq - generated payment id
 @return itemSeq - represent item id
 @return purchaseToken - represent token for validation.
 
 */
+ (instancetype) startPurchaseWithViewController:(UIViewController*)baseViewController
                                          itemId:(long)itemId
                               completionHandler:(TIARequestHandler)handler;

/*!
 Request purchases, Purchases means the payment is successfully completed,
 Does not appear in later history is consumed by 3rd party server.
 
 @param handler     handler's result has that translation of JSON data to 'NSArray'
 
 @return [data of array]
 @return paymentSeq - generated payment id
 @return itemSeq - represent item id
 @return purchaseToken - represent token for validation.
 */
+ (instancetype) purchasesWithCompletionHandler:(TIARequestHandler)handler;

/*!
 Request for item list which is registered in webConsole and in itunesConnect
 
 @param handler     handler's result has that translation of JSON data to 'NSArray'
 @return [result data of array]
 @return itemSeq - item id
 @return itemName - item name
 @return usingStatus - item status on IAP server
 @return regYmdt - item registration date on IAP server
 @return appName - app name
 @return marketId - market id (AS : APPLE STORE)
 @return marketItemId - market item id (product id)
 @return currency - represent to item currency
 @return price - represent to item price
 @return localizedPrice - represent to localized item price
 */
+ (instancetype) itemListWithCompletionHandler:(TIARequestHandler)handler;

/*!
 Request for item list which is registered in webConsole but not in itunesConnect
 
 @param handler                     handler's result has that translation of JSON data to 'NSArray'
 
 @return [result data of array]
 @return itemSeq - item id
 @return itemName - item name
 @return usingStatus - item status on IAP server
 @return regYmdt - item registration date on IAP server
 @return appName - app name
 @return marketId - market id (AS : APPLE STORE)
 @return marketItemId - market item id (product id)
 */
+ (instancetype) getTCConsoleItemsWithCompletionHandler:(TIARequestHandler)handler;


/*!
 Request processes IncompletePurchases
 
 @param handler             handler's result has that translation of JSON data to 'NSDictionany'
 
 @return [result data of NSDictionany]
 @return successList - success data of 'NSArray'
 @return failList - fail data of 'NSArray'
 @return [list data of NSArray(successList or failList)]
 @return paymentSeq - generated payment id
 @return itemSeq - represent item id
 @return purchaseToken - represent token for validation
 @return marketItemId - market item id (product id)
 @return currency - represent to item currency
 @return price - represent to item price
 */
+ (instancetype) processesIncompletePurchasesWithCompletionHandler:(TIARequestHandler)handler;

/*!
 Return SDK Version
 
 @return SDK Version
 */
+ (NSString *)sdkVersion;
@end

