#import "TCGBNetworkPlugin.h"
#import "DelegateManager.h"
#import <Gamebase/Gamebase.h>
#import "TCGBUnityDictionaryJSON.h"
#import "UnityMessageSender.h"
#import "UnityMessage.h"
#import "NativeMessage.h"

#define NETWORK_API_GET_TYPE                            @"gamebase://getType"
#define NETWORK_API_GET_TYPE_NAME                       @"gamebase://getTypeName"
#define NETWORK_API_IS_CONNECTED                        @"gamebase://isConnected"
#define NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER      @"gamebase://addOnChangedStatusListener"
#define NETWORK_API_REMOVE_ON_CHANGED_STATUS_LISTENER   @"gamebase://removeOnChangedStatusListener"

@implementation TCGBNetworkPlugin

@synthesize gameObjectName = _gameObjectName;
@synthesize requestMethodName = _requestMethodName;
@synthesize handle = _handle;
@synthesize networkStatus = _networkStatus;

- (instancetype)init {
    if ((self = [super init]) == nil) {
        return nil;
    }
    
    __block TCGBNetworkPlugin *tempSelf = self;
    
    _networkStatus = ^(NetworkStatus status) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER handle:tempSelf.handle TCGBError:nil jsonData:[@(status) stringValue] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:tempSelf.gameObjectName requestMethodName:tempSelf.requestMethodName];
    };
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NETWORK_API_GET_TYPE target:self selector:@selector(getType:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NETWORK_API_GET_TYPE_NAME target:self selector:@selector(getTypeName:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NETWORK_API_IS_CONNECTED target:self selector:@selector(isConnected:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER target:self selector:@selector(addOnChangedStatusListener:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:NETWORK_API_REMOVE_ON_CHANGED_STATUS_LISTENER target:self selector:@selector(removeOnChangedStatusListener:)];
    
    return self;
}

-(NSString*)getType:(UnityMessage*)message {
    NSString* result = [@([TCGBNetwork type]) stringValue];
    return result;
}

-(NSString*)getTypeName:(UnityMessage*)message {
    NSString* result = [TCGBNetwork typeName];
    return result;
}

-(NSString*)isConnected:(UnityMessage*)message {
    NSMutableDictionary *contentDictionary = [[NSMutableDictionary alloc]init];
    [contentDictionary setValue:[NSNumber numberWithBool:[TCGBNetwork isConnected]] forKey:@"isConnected"];
    
    NSString* result = [contentDictionary JSONString];
    if(result == nil)
        return @"";
    
    return result;
}

-(NSString*)addOnChangedStatusListener:(UnityMessage*)message {
    _handle = message.handle;
    _gameObjectName = message.gameObjectName;
    _requestMethodName = message.requestMethodName;
    
    NSMutableDictionary *contentDictionary = [[NSMutableDictionary alloc]init];
   
        [contentDictionary setValue:[NSNumber numberWithBool:[TCGBNetwork addObserverOnChangedNetworkStatusWithHandler:_networkStatus]] forKey:@"isSuccess"];
    
    NSString* result = [contentDictionary JSONString];
    if(result == nil)
        return @"";
    
    return result;
}

-(NSString*)removeOnChangedStatusListener:(UnityMessage*)message {
    _handle = -1;
    _gameObjectName = nil;
    _requestMethodName = nil;
    
    NSMutableDictionary *contentDictionary = [[NSMutableDictionary alloc]init];
    [contentDictionary setValue:[NSNumber numberWithBool:[TCGBNetwork removeObserverOnChangedNetworkStatusWithHandler:_networkStatus]] forKey:@"isSuccess"];
    
    NSString* result = [contentDictionary JSONString];
    if(result == nil)
        return @"";
    
    return result;
}
@end
