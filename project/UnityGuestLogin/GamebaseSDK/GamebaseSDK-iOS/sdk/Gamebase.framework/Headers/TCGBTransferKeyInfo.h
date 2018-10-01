//
//  TCGBIssueTransferKey.h
//  Gamebase
//
//  Created by NHNEnt on 2018. 4. 13..
//  Copyright © 2018년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TCGBObject.h"

extern NSString * const kTCGBIssueTransferKey;
extern NSString * const kTCGBIssueTransferKeyRegDate;
extern NSString * const kTCGBIssueTransferKeyExpireDate;

@interface TCGBTransferKeyInfo : TCGBObject
@property (nonatomic, strong) NSString* transferKey;
@property (nonatomic, assign) long regDate;
@property (nonatomic, assign) long expireDate;

+ (TCGBTransferKeyInfo *)transferKeyInfoWithTransferKey:(NSString *)transferKey regDate:(long)regDate expireDate:(long)expireDate;
+ (TCGBTransferKeyInfo *)transferKeyInfoWithDictionary:(NSDictionary *)dictionary;
+ (TCGBTransferKeyInfo *)transferKeyInfoWithJSONString:(NSString *)JSONString;

- (NSString *)stringOfExpireDateByCurrentLocale;
@end
