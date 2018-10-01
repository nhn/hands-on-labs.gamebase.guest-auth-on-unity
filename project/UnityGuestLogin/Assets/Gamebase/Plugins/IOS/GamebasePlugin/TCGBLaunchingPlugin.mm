#import "TCGBLaunchingPlugin.h"
#import "DelegateManager.h"
#import <Gamebase/Gamebase.h>
#import "TCGBUnityDictionaryJSON.h"
#import "UnityMessageSender.h"
#import "UnityMessage.h"
#import "NativeMessage.h"

#define NLAUNCHING_API_ADD_UPDATE_STATUSLISTENER        @"gamebase://addOnUpdateStatusListener"
#define NLAUNCHING_API_REMOVE_UPDATE_STATUSLISTENER     @"gamebase://removeOnUpdateStatusListener"
#define NLAUNCHING_API_GET_LAUNCHING_INFORMATIONS       @"gamebase://getLaunchingInformations"
#define NLAUNCHING_API_GET_LAUNCHING_STATUS             @"gamebase://getLaunchingStatus"

@implementation TCGBLaunchingPlugin

@synthesize gameObjectName = _gameObjectName;
@synthesize requestMethodName = _requestMethodName;
@synthesize handle = _handle;
@synthesize launchingStatus = _launchingStatus;


- (instancetype)init {
    if ((self = [super init]) == nil) {
        return nil;
    }
    
    __block TCGBLaunchingPlugin *tempSelf = self;
    
    _launchingStatus = ^(NSDictionary* launchingStatus){
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:NLAUNCHING_API_ADD_UPDATE_STATUSLISTENER handle:tempSelf.handle TCGBError:nil jsonData:[launchingStatus JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:tempSelf.gameObjectName requestMethodName:tempSelf.requestMethodName];
    };
    
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NLAUNCHING_API_GET_LAUNCHING_INFORMATIONS target:self selector:@selector(getLaunchingInformations:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NLAUNCHING_API_GET_LAUNCHING_STATUS target:self selector:@selector(getLaunchingStatus:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NLAUNCHING_API_ADD_UPDATE_STATUSLISTENER target:self selector:@selector(addOnUpdateStatusListener:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NLAUNCHING_API_REMOVE_UPDATE_STATUSLISTENER target:self selector:@selector(removeOnUpdateStatusListener:)];
    
    return self;
}

-(NSString*)getLaunchingInformations:(UnityMessage*)message {
    return [[TCGBLaunching launchingInformations] JSONString];
}

-(NSString*)getLaunchingStatus:(UnityMessage*)message {
    NSString* result = [@([TCGBLaunching launchingStatus]) stringValue];
    return result;
}


-(NSString*)addOnUpdateStatusListener:(UnityMessage*)message {
    _handle = message.handle;
    _gameObjectName = message.gameObjectName;
    _requestMethodName = message.requestMethodName;
    
    NSMutableDictionary *contentDictionary = [[NSMutableDictionary alloc] init];
    
    [contentDictionary setValue:[NSNumber numberWithBool:[TCGBLaunching addObserverOnChangedStatusNotification:_launchingStatus]] forKey:@"isSuccess"];
    
    NSString* result = [contentDictionary JSONString];
    if(result == nil)
        return @"";
    
    return result;
}

-(NSString*)removeOnUpdateStatusListener:(UnityMessage*)message{
    _handle = -1;
    _gameObjectName = nil;
    _requestMethodName = nil;
    
    NSMutableDictionary *contentDictionary = [[NSMutableDictionary alloc]init];
    
    [contentDictionary setValue:[NSNumber numberWithBool:[TCGBLaunching removeObserverOnChangedStatusNotification:_launchingStatus]] forKey:@"isSuccess"];
    
    NSString* result = [contentDictionary JSONString];
    if(result == nil)
        return @"";
    
    return result;
}

@end
