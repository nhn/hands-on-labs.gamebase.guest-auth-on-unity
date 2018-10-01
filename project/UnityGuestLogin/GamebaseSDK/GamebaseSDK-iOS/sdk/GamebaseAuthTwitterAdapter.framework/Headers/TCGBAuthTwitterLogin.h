//
//  HSPTwitterConnector.h
//  HSPIdpConnector
//
//  Created by Park Panki on 2016. 7. 20..
//
//
#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>
#import "TCGBAuthTwitterLoginWebViewController.h"
#import "TCGBAuthTwitterLoginOAuthAPI.h"
#import "TCGBAuthTwitterErrorConstants.h"

#define GamebaseAuthTwitterAdapterVersion @"1.0.2"

#pragma mark - TCGBAuthTwitterLogin
@interface TCGBAuthTwitterLogin : NSObject <TCGBAuthAdapterDelegate, TCGBAuthTwitterLoginWebViewDelegate, TCGBAuthTwitterLoginOAuthAPIDelegate>

@property (nonatomic, strong) NSString* consumerKey;
@property (nonatomic, strong) NSString* consumerSecret;
@property (nonatomic, strong) TCGBProviderAuthCredential *credential;
@property (nonatomic, strong) TCGBAuthTwitterLoginWebViewController *webViewController;
@property (nonatomic, strong) TCGBAuthTwitterLoginOAuthAPI *oauthAPI;
@property (nonatomic, strong) UIViewController* loginViewController;

@end
