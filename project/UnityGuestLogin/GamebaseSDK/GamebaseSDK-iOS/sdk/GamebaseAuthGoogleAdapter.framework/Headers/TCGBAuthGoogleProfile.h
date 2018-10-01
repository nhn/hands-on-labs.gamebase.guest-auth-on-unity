//
//  TCGBAuthGoogleProfile.h
//  GamebaseAuthGoogleAdapter
//
//  Created by NHNEnt on 2018. 5. 24..
//  Copyright © 2018년 NHNEnt. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface TCGBAuthGoogleProfile : NSObject <NSCopying>
@property (nonatomic, strong) NSString* ID;
@property (nonatomic, strong) NSString* email;
@property (nonatomic, strong) NSString* displayName;
@property (nonatomic, strong) NSString* imageURL;

+ (TCGBAuthGoogleProfile *)googleProfileWithDictionary:(NSDictionary *)dictionary;
- (id)initWithDictionary:(NSDictionary *)dictionary;
@end


