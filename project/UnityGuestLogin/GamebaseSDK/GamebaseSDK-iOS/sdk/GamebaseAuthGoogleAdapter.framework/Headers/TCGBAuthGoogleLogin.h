//
//  TCGBAuthGoogleLogin.h
//  GamebaseAuthGoogleAdapter
//
//  Created by NHNEnt on 2018. 4. 25..
//  Copyright © 2018년 NHNEnt. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>
#import <SafariServices/SafariServices.h>

#import "TCGBAuthGoogleConstants.h"
#import "TCGBAuthGoogleProfile.h"

#define GamebaseAuthGoogleAdapterVersion @"1.0.1"

typedef void(^adapterLoginCompletionHandler)(id userInfo, TCGBError* error);

@interface TCGBAuthGoogleLogin : NSObject <TCGBAuthAdapterDelegate, SFSafariViewControllerDelegate>

@property (nonatomic, strong) NSString* clientID;
@property (nonatomic, strong) NSString* loginPageBaseURL;
@property (nonatomic, strong) NSString* loginCallbackBaseURL;

@property (nonatomic, strong) TCGBProviderAuthCredential* credential;

@property (nonatomic, strong) NSString* scheme;                         // ios_only_scheme
@property (nonatomic, strong) NSString* googleAccessToken;              //회원 로그인 페이지에서 authorizationCode를 accessToken으로 변환하여 스킴으로 넘겨주고 있음.
@property (nonatomic, strong) TCGBAuthGoogleProfile* googleProfile;              // /plus/v1/people/me?

@end
