//
//  TCGBWebView.h
//  TCGBWebKit
//
//  Created by Park Panki on 2016. 12. 20..
//  Copyright © 2016년 NHN Entertainment. All rights reserved.
//

#import <UIKit/UIKit.h>

@class TCGBError;
@class TCGBWebURL;
@class TCGBWebViewConfiguration;
@class TCGBWebViewController;
@protocol TCGBWebViewDelegate;

typedef void(^TCGBWebViewCloseCompletion)(TCGBError *error);
typedef void(^TCGBWebViewSchemeEvent)(NSString *fullUrl, TCGBError *error);

extern NSString * const kTCGBWebKitDomain;
extern NSString * const kTCGBWebKitBundleName;

/** The TCGBWebView class represents the entry point for **launching WebView**.
 */
@interface TCGBWebView : NSObject

/**---------------------------------------------------------------------------------------
 * @name Properties
 *  ---------------------------------------------------------------------------------------
 */

/**
 
 This property is a global configuration for launching webview.<br/>
 When you handle the webview without any configuration, TCGBWebView set its configuration with this value.
 */
@property (nonatomic, strong) TCGBWebViewConfiguration *defaultWebConfiguration;

/**
 
 This dictionary contains key-action objectes.<br/>
 Each key represents custom scheme, and each action is completionHandler.
 @see addCustomScheme:block:
 @see removeCustomScheme:
 */
@property (nonatomic, strong) NSMutableDictionary *schemeCallbackDictionary;

/**---------------------------------------------------------------------------------------
 * @name Initialization
 *  ---------------------------------------------------------------------------------------
 */

/**
 
 Creates and returns an `TCGBWebView` object.
 */
+ (instancetype)sharedTCGBWebView;

/**---------------------------------------------------------------------------------------
 * @name Launching WebView
 *  ---------------------------------------------------------------------------------------
 */

/**
 Show Web Browser that has navigationbar and fullscreen.
 
 @param urlString The string value for target url
 @param viewController The presenting view controller
 @warning If viewController is nil, TCGBWebView set it to top most view controller automatically.
 
 @since Added 1.4.0.
 @deprecated As of release 1.5.0, use showWebViewWithURL:viewController:configuration:closeCompletion:schemeList:schemeEvent: method instead.
 */
+ (void)showWebBrowserWithURL:(NSString *)urlString viewController:(UIViewController*)viewController
DEPRECATED_MSG_ATTRIBUTE("Use showWebViewWithURL:viewController:configuration:closeCompletion:schemeList:schemeEvent: method instead.");


/**
 Show WebView that is not for local url.
 
 @param urlString The string value for target url
 @param viewController The presenting view controller
 @warning If viewController is nil, TCGBWebView set it to top most view controller automatically.
 @param configuration This configuration is applied to the behavior of webview.
 @warning If configuration is nil, TCGBWebView set it to default value. It is described in `TCGBWebViewConfiguration`.
 
 @since Added 1.4.0.
 @deprecated As of release 1.5.0, use showWebViewWithURL:viewController:configuration:closeCompletion:schemeList:schemeEvent: method instead.
 */
+ (void)showWebViewWithURL:(NSString *)urlString viewController:(UIViewController*)viewController configuration:(TCGBWebViewConfiguration *)configuration
DEPRECATED_MSG_ATTRIBUTE("Use use showWebViewWithURL:viewController:configuration:closeCompletion:schemeList:schemeEvent: method instead.");


/**
 Show WebView that is not for local url.
 
 @param urlString The string value for target url
 @param viewController The presenting view controller
 @warning If viewController is nil, TCGBWebView set it to top most view controller automatically.
 @param configuration This configuration is applied to the behavior of webview.
 @warning If configuration is nil, TCGBWebView set it to default value. It is described in `TCGBWebViewConfiguration`.
 @param closeCompletion This completion would be called when webview is closed
 @param schemeList This schemeList would be filtered every web view request and call schemeEvent
 @param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
 
 @since Added 1.5.0.
 */
+ (void)showWebViewWithURL:(NSString *)urlString
            viewController:(UIViewController*)viewController
             configuration:(TCGBWebViewConfiguration *)configuration
           closeCompletion:(TCGBWebViewCloseCompletion)closeCompletion
                schemeList:(NSArray<NSString *> *)schemeList
               schemeEvent:(TCGBWebViewSchemeEvent)schemeEvent;


/**
 Show WebView for local html (or other web resources)
 
 @param filePath The string value for target local path.
 @param bundle where the html file is located.
 @warning If bundle is nil, TCGBWebView set it to main bundle automatically.
 @param viewController The presenting view controller
 @warning If viewController is nil, TCGBWebView set it to top most view controller automatically.
 @param configuration This configuration is applied to the behavior of webview.
 @warning If configuration is nil, TCGBWebView set it to default value. It is described in `TCGBWebViewConfiguration`.
 @param closeCompletion This completion would be called when webview is closed
 @param schemeList This schemeList would be filtered every web view request and call schemeEvent
 @param schemeEvent This schemeEvent would be called when web view request matches one of the schemeLlist
 
 @since Added 1.5.0.
 */
+ (void)showWebViewWithLocalURL:(NSString *)filePath
                         bundle:(NSBundle *)bundle
                 viewController:(UIViewController*)viewController
                  configuration:(TCGBWebViewConfiguration *)configuration
                closeCompletion:(TCGBWebViewCloseCompletion)closeCompletion
                     schemeList:(NSArray<NSString *> *)schemeList
                    schemeEvent:(TCGBWebViewSchemeEvent)schemeEvent;

+ (void)showWebViewWithDefaultHTML:(NSString *)defaultHTML viewController:(UIViewController *)viewController configuration:(TCGBWebViewConfiguration *)configuration closeCompletion:(TCGBWebViewCloseCompletion)closeCompletion schemeList:(NSArray<NSString *> *)schemeList schemeEvent:(TCGBWebViewSchemeEvent)schemeEvent;


/**
 Open the Browser with urlString
 
 @param urlString The URL to be loaded.
 @warning If urlString is not valid, to open browser would be failed. Please check the url before calling.
 
 @since Added 1.5.0.
 */
+ (void)openWebBrowserWithURL:(NSString *)urlString;


/**
 Close the presenting Webview
 
 @since Added 1.5.0.
 */
+ (void)closeWebView;




/**---------------------------------------------------------------------------------------
 * @name Managing Custom Scheme
 *  ---------------------------------------------------------------------------------------
 */

/**
 Add Custom Scheme
 
 @param schemeString This string is the key that is called in html or the other place.
 @param handler This handler is a block which will be excuted by calling the schemeString, the key.
 */
+ (void)addCustomScheme:(NSString *)schemeString block:(void(^)(UIViewController<TCGBWebViewDelegate> *viewController, TCGBWebURL *webURL))handler;

/**
 Remove Custom Scheme
 
 @param schemeString This string is the key of `schemeCallbackDictionary`.
 */
+ (void)removeCustomScheme:(NSString *)schemeString;

@end





/** The TCGBWebVoewDelegate is a UIViewController delegate.
 */
@protocol TCGBWebViewDelegate <NSObject>

@required

@optional
- (void)viewDidAppear:(BOOL)animated;
- (void)webViewDidStartLoad:(UIWebView *)webView;
- (void)webViewDidFinishLoad:(UIWebView *)webView;
- (BOOL)webView:(UIWebView *)webView shouldStartLoadWithRequest:(NSURLRequest *)request navigationType:(UIWebViewNavigationType)navigationType;
- (void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error;
- (void)viewDidDisappear:(BOOL)animated;
- (void)close;
- (void)goBack;
- (NSString *)stringByEvaluatingJavaScriptFromString:(NSString *)script;
- (void)webView:(UIWebView *)webView shouldStartLoadWithRequest:(NSURLRequest *)request navigationType:(UIWebViewNavigationType)navigationType completion:(void(^)(BOOL shouldStartLoad))completion;

@property (nonatomic, weak) UIView *rootView;

@end
