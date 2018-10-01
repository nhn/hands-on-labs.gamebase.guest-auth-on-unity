//
//  TCGBAuthProviderProfile.h
//  Gamebase
//
//  Created by NHNENT on 2017. 3. 16..
//  Copyright © 2017년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface TCGBAuthProviderProfile : NSObject

@property (nonatomic, strong, readonly) NSString*     userID;
@property (nonatomic, strong, readonly) NSDictionary* information;

+ (TCGBAuthProviderProfile *)authProviderProfileWithUserID:(NSString *)userID information:(NSDictionary *)information;

@end
