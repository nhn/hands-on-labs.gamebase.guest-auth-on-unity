//
//  TCGBUtil.h
//  Gamebase
//
//  Created by NHNENT on 2016. 6. 30..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "TCGBConstants.h"

#ifndef TCGBUTIL_H
#define TCGBUTIL_H


#define LOG_VERBOSE(s, ...) do {\
[TCGBUtil logVerboseWithFormat:@"[%@:(%d)] %@", [[NSString stringWithUTF8String:__FILE__] lastPathComponent], __LINE__, [NSString stringWithFormat:(s), ##__VA_ARGS__]]; \
} while (0)

#define LOG_DEBUG(s, ...) do {\
[TCGBUtil logDebugWithFormat:@"[%@:(%d)] %@", [[NSString stringWithUTF8String:__FILE__] lastPathComponent], __LINE__, [NSString stringWithFormat:(s), ##__VA_ARGS__]]; \
} while (0)

#define LOG_INFO(s, ...) do {\
[TCGBUtil logInfoWithFormat:@"[%@:(%d)] %@", [[NSString stringWithUTF8String:__FILE__] lastPathComponent], __LINE__, [NSString stringWithFormat:(s), ##__VA_ARGS__]]; \
} while (0)

#define LOG_WARN(s, ...) do {\
[TCGBUtil logWarnWithFormat:@"[%@:(%d)] %@", [[NSString stringWithUTF8String:__FILE__] lastPathComponent], __LINE__, [NSString stringWithFormat:(s), ##__VA_ARGS__]]; \
} while (0)

#define LOG_ERROR(s, ...) do {\
[TCGBUtil logErrorWithFormat:@"[%@:(%d)] %@", [[NSString stringWithUTF8String:__FILE__] lastPathComponent], __LINE__, [NSString stringWithFormat:(s), ##__VA_ARGS__]]; \
} while (0)

typedef void(^alertActionBlock)(id buttonTitle);

/** The TCGBUtil class provides convenient and useful methods.
 */
@interface TCGBUtil : NSObject

#pragma mark - Gamebase Informations
/**---------------------------------------------------------------------------------------
 * @name Utilities
 *  ---------------------------------------------------------------------------------------
 */

/**
 @return Gamebase Version
 */
+ (NSString *)gamebaseVersion;

#pragma mark - Device Informations
/**
 @return IDFA, Advertising Identifier.
 */
+ (NSString *)idfa;

/**
 @return IDFV, Vendor Identifier.
 */
+ (NSString *)idfv;

/**
 @return UUID, 4 random bytes string.
 */
+ (NSString *)uuid;

/**
 @return OS Code, actually "IOS".
 */
+ (NSString *)osCode;

/**
 @return OS Version.
 */
+ (NSString *)osVersion;

/**
 @return OS Version dot-formatted.
 */
+ (NSArray *)osVersionSeperatedByDot;

/**
 @return `YES` if majorVersion is greater than or equal to majorVersion.
 @param majorVersion version to be compared with OS version.
 @param later if this value sets YES, it returns whether OS version is greater than or equal to majorVersion.
 */
+ (BOOL)isiOSVersion:(int)majorVersion orLater:(BOOL)later;

/**
 @return Device model.
 */
+ (NSString *)deviceModel;

/**
 @return Preferred launguage.
 */
+ (NSString *)deviceLanguage;

/**
 @return language code by NSLocaleLanguageCode of NSLocale. (ex. "en", "ko", "ja", "zh", "vi")
 */
+ (NSString *)deviceLocaleLanguageCode;

/**
 @return ISO country code. First, usimCountryCode is returned, if there is not usim, deviceCountryCode is returned.
 @see usimCountryCode
 @see deviceCountryCode
 */
+ (NSString *)country;

/**
 @return Usim country code is returned. If there is not usim, "ZZ" is returned.
 */
+ (NSString *)usimCountryCode;

/**
 @return Locale country code is returned.
 */
+ (NSString *)deviceCountryCode;

/**
 @return Cellular service provider's code.
 */
+ (NSString *)carrierCode;

/**
 @return Cellular service provider's name.
 */
+ (NSString *)carrierName;

#pragma mark - app informations
/**
 @return Main bundle's identifier.
 */
+ (NSString *)bundleID;

#pragma mark - Data & Time
/**
 @return Unix epoch time, it is millisecods, 13 digits.
 */
+ (uint64_t)unixEpochTime;

#pragma mark - JSON
/**
 @return value object which is in the json formatted dictionary and have a dottedString key.
 
    ## Example
    NSDictionary json;
    json = @{
        @"key1" : @"value1",
        @"key2" : @{
            @"key2-1" : @"value2-1",
            @"key2-2" : @[@"value2-2[0]", @"value2-2[1]"]
        },
        @"key3" : @"value3"
    }
 
    NSString *dottedString = @"key2.key2-2[1]";
    NSString *extractedString = [TCGBUtil extractValueFrom:json searchString:dottedString];
    NSLog(@"%@", extractedString);
    // It would be > "value2-2[1]"
 
 */
+ (id)extractValueFrom:(NSDictionary *)json searchString:(NSString *)dottedString;

#pragma mark - Encoding
/**---------------------------------------------------------------------------------------
 * @name Encoding
 *  ---------------------------------------------------------------------------------------
 */

/**
 @return `YES` if the targetString is URL encoded.
 */
+ (BOOL)isURLEncoded:(NSString *)targetString;

/**
 @return URL Encoded String.
 @param string The string to be encoded.
 */
+ (NSString *)URLEncodedString:(NSString *)string;

/**
 @return URL Encoded String.
 @param data The data to be encoded.
 */
+ (NSString *)URLEncodedStringWithData:(NSData *)data;

#pragma mark - System UI
/**---------------------------------------------------------------------------------------
 * @name System UI
 *  ---------------------------------------------------------------------------------------
 */

/**
 Show Toast.

 @param message The message to be shown in the toast.
 @param duration The time interval to be exposed.
 @deprecated As of release 1.4.0, use showToastWithMessage:length: method instead.
 */
+ (void)showToastWithMessage:(NSString *)message duration:(NSTimeInterval)duration
DEPRECATED_MSG_ATTRIBUTE("Use showToastWithMessage:length: method instead");

/**
 Show Toast.
 
 @param message The message to be shown in the toast.
 @param length The time interval to be exposed. GamebaseToastLengthLong (3.5 seconds), GamebaseToastLengthShort (2 seconds)
 */
+ (void)showToastWithMessage:(NSString *)message length:(GamebaseToastLength)length;

/**
 Show Alert View with async completion handler

 @param title The title of alert
 @param message The message of alert
 @param completion The callback of alert when user has tapped the 'OK(확인)' button
 */
+ (void)showAlertWithTitle:(NSString *)title message:(NSString *)message completion:(void(^)(void))completion;

/**
 Show Alert View
 
 @param title The title of alert
 @param message The message of alert
 */
+ (void)showAlertWithTitle:(NSString *)title message:(NSString *)message;
 
/**
 Show Alert.
 
 @param title The title to be shown in the alert.
 @param message The message to be shown in the alert.
 @param alertActionBlocks The block to be set to UIAlertView or UIAlertController according to ios version.
 @deprecated As of release 1.8.0, use showAlertWithTitle:message:completion: method instead.
 */
+ (void)showAlertWithTitle:(NSString *)title message:(NSString *)message blocks:(NSDictionary<NSString *, alertActionBlock> *)alertActionBlocks
DEPRECATED_MSG_ATTRIBUTE("Use showAlertWithTitle:message:completion: method instead");

//TODO: ADD DESCRIPTIONS
+ (UIViewController *)topMostViewcontroller;

/**
 Show Alert.
 
 'alertActionBlocks' The block to be set to UIAlertView or UIAlertController according to ios version.
 
 @param title The title to be shown in the alert.
 @param message The message to be shown in the alert.
 @param positiveTitle The positive button title to be shown in the alert.
 @param negativeTitle The negative button title to be shown in the alert.
 @param positiveBlock The positive block to be executed after click positive button.
 @param negativeBlock The negative block to be executed after click negative button.
 @deprecated As of release 1.8.0, use showAlertWithTitle:message:completion: method instead.
 */
+ (void)showAlertWithTitle:(NSString *)title message:(NSString *)message
             positiveTitle:(NSString *)positiveTitle positiveBlock:(alertActionBlock)positiveBlock
             negativeTitle:(NSString *)negativeTitle negativeBlock:(alertActionBlock)negativeBlock
DEPRECATED_MSG_ATTRIBUTE("Use showAlertWithTitle:message:completion: method instead");

#pragma mark - Log
/**---------------------------------------------------------------------------------------
 * @name Logging
 *  ---------------------------------------------------------------------------------------
 */

/**
 Verbose Log
 
 @param format Log
 @see TCGBLogLevel // << ??? This is in internal (TCGBUtility.h)
 */
+ (void)logVerboseWithFormat:(NSString *)format, ...;

/**
 Debug Log
 
 @param format Log
 */
+ (void)logDebugWithFormat:(NSString *)format, ...;

/**
 Info Log
 
 @param format Log
 */
+ (void)logInfoWithFormat:(NSString *)format, ...;

/**
 Warning Log
 
 @param format Log
 */
+ (void)logWarnWithFormat:(NSString *)format, ...;

/**
 Error Log
 
 @param format Log
 */
+ (void)logErrorWithFormat:(NSString *)format, ...;

/**
 Log
 
 @param logString Log
 
 @warning If os version is greater than or equal to ios 10.
 */
+ (void)logWithString:(NSString *)logString;

/**
 OS Log
 
 @param newString Log
 @warning It can be only used in ios 10.
 */
+ (void)osLog:(NSString *)newString;

@end

#endif

