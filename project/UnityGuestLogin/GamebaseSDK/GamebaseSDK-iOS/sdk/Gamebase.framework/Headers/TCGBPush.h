//
//  TCGBPush.h
//  Gamebase
//
//  Created by NHNENT on 2016. 5. 31..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TCGBObject.h"

@class TCGBError;

extern NSString* const kTCGBPushEnabledKeyname;          // keyname of pushEnabled property
extern NSString* const kTCGBPushADAgreementKeyname;      // keyname of ADAgreement property
extern NSString* const kTCGBPushADAgreementNightKeyname; // keyname of ADAgreementNight property
extern NSString* const kTCGBPushDisplayLanguageCodeKeyname;  // keyname of displayLanguageCode property

/** The TCGBPushConfiguration class configures the behavior of TCGBPush.
 */
@interface TCGBPushConfiguration : TCGBObject

/**---------------------------------------------------------------------------------------
 * @name Properties
 *  ---------------------------------------------------------------------------------------
 */

/**
 Enable push
 */
@property (nonatomic, assign) BOOL pushEnabled;

/**
 Agreement of getting advertising push
 */
@property (nonatomic, assign) BOOL ADAgreement;

/**
 Agreement of getting advertising push at night
 */
@property (nonatomic, assign) BOOL ADAgreementNight;

/**
 Setting language of Push Message
 */
@property (nonatomic, strong) NSString* displayLanguageCode;

/**---------------------------------------------------------------------------------------
 * @name Initialization
 *  ---------------------------------------------------------------------------------------
 */

/**
 Creates a TCGBPushConfiguration instance with several properties.
 
 @param enable whether push enable or not.
 @param ADAgree `YES` if user agree on advertising push notification.
 @param ADAgreeNight `YES` if user agree on getting advertising push notification at night.
 */
+ (TCGBPushConfiguration *)pushConfigurationWithPushEnable:(BOOL)enable ADAgreement:(BOOL)ADAgree ADAgreementNight:(BOOL)ADAgreeNight;

/**
 Creates a TCGBPushConfiguration instance with several properties.
 
 @param enable whether push enable or not.
 @param ADAgree `YES` if user agree on advertising push notification.
 @param ADAgreeNight `YES` if user agree on getting advertising push notification at night.
 @param displayLanguage set language code for Push. (kTCPushKeyLanguage in TOAST Push SDK)
 */
+ (TCGBPushConfiguration *)pushConfigurationWithPushEnable:(BOOL)enable ADAgreement:(BOOL)ADAgree ADAgreementNight:(BOOL)ADAgreeNight displayLanguage:(NSString *)displayLanguage;

/**
 Creates a TCGBPushConfiguration instance with several properties.
 @param jsonString In this string, there should be `pushEnabled`, `adAgreement`, `adAgreementNight` keys in the json formatted string.
 */
+ (TCGBPushConfiguration *)pushConfigurationWithJSONString:(NSString *)jsonString;

@end

/** The TCGBPush class provides registering push token API to ToastCloud Push Server and querying push token API.
 */
@interface TCGBPush : NSObject

/**
 Register push token to ToastCloud Push Server.
 
 @param configuration The configuration which has pushEnabled, ADAgreement and AdAgreementNight.
 @param completion callback
 
 @see TCGBPushConfiguration
 */
+ (void)registerPushWithPushConfiguration:(TCGBPushConfiguration *)configuration completion:(void(^)(TCGBError *error))completion;

/**
 Query push token to ToastCloud Push Server.
 
 @param completion callback, this callback has TCGBPushConfiguration information.
 
 @see TCGBPushConfiguration
 */
+ (void)queryPushWithCompletion:(void(^)(TCGBPushConfiguration *configuration, TCGBError *error))completion;

/**
 Set SandboxMode.
 
 @param isSandbox `YES` if application is on the sandbox mode.
 */
+ (void)setSandboxMode:(BOOL)isSandbox;

@end
