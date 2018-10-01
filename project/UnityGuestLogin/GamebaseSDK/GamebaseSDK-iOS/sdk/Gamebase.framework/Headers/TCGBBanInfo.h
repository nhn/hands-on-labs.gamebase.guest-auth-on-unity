//
//  TCGBBanInfo.h
//  Gamebase
//
//  Created by simbs on 2017. 9. 4..
//  Copyright © 2017년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface TCGBBanInfo : NSObject
@property (nonatomic, strong) NSString*     userId;
@property (nonatomic, strong) NSString*     banType;
@property (nonatomic, strong) NSNumber*     beginDate;
@property (nonatomic, strong) NSNumber*     endDate;
@property (nonatomic, strong) NSString*     message;
@property (nonatomic, strong) NSString*     csInfo;
@property (nonatomic, strong) NSString*     csUrl;
@end
