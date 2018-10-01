//
//  TCGBTCPushCommon.h
//  GamebasePushAdapter
//
//  Created by NHNENT on 2016. 5. 20..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>

#define GamebasePushAdapterVersion @"1.1.2"

@class TCGBPushConfiguration;
@class TCGBError;

extern NSString* const kTCGBPushChangedNotificationSettingsNotification;         // post notification with options NSDictionary

@interface TCGBPushCommon : NSObject

+ (TCGBPushCommon *)sharedPushCommon;

+ (void)registerWithPushConfiguration:(TCGBPushConfiguration *)pushConfiguration completion:(void(^)(TCGBError* error))completion;

+ (void)queryWithPushConfiguration:(TCGBPushConfiguration *)pushConfiguration completion:(void(^)(TCGBPushConfiguration* pushConfig, TCGBError *error))completion;

+ (void)setSandboxMode:(NSNumber *)isSandbox;

+ (void)setAppKey:(NSString *)appkey userID:(NSString *)userID serverURL:(NSString *)serverURL;

+ (NSString *)versionString;

@end
