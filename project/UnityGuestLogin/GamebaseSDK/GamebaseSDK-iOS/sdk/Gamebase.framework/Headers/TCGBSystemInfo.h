//
//  TCGBSystemInfo.h
//  Gamebase
//
//  Created by NHNENT on 2016. 6. 30..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface TCGBSystemInfo : NSObject

+ (NSString *)zoneType;
+ (NSString *)getUUID;
+ (NSString *)UDID;
+ (NSString *)ADID;
+ (NSString *)carrierCode;
+ (NSString *)carrierName;
+ (NSString *)countryCode;
+ (NSString *)languageCode;

@end
