//
//  TCGBAuthPaycoLogin.h
//  TCGBAuthPaycoAdapter
//
//  Created by Park Panki on 2016. 11. 28..
//  Copyright © 2016년 NHN Entertainment. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>
#import <PIDThirdPartyAuth/PIDThirdPartyAuth.h>

#define GamebaseAuthPaycoAdapterVersion @"1.2.1"

@interface TCGBAuthPaycoLogin : NSObject <TCGBAuthAdapterDelegate, PIDThirdPartyAuthDelegate>

@property (nonatomic, strong) NSString *provider;
@property (nonatomic, strong) NSString *appId;
@property (nonatomic, strong) NSString *appSecret;
@property (nonatomic, strong) NSString *accessToken;
@property (nonatomic, strong) TCGBProviderAuthCredential* credential;

@end
