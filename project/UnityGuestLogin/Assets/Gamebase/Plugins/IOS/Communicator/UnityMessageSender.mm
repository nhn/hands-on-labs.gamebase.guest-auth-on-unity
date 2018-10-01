#import "UnityMessageSender.h"
#import "TCGBUnityDictionaryJSON.h"

@implementation UnityMessageSender

@synthesize gameObjectName = _gameObjectName;
@synthesize requestMethodName = _requestMethodName;
@synthesize message = _message;

+(UnityMessageSender*)sharedUnityMessageSender {
    static dispatch_once_t onceToken;
    static UnityMessageSender* instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[UnityMessageSender alloc] init];
    });
    return instance;
}

-(void)sendMessage:(NativeMessage*)message gameObjectName:(NSString*)gameObjectName requestMethodName:(NSString*)requestMethodName {
    [self setMessage:message];
    [self setGameObjectName:gameObjectName];
    [self setRequestMethodName:requestMethodName];
    [self sendMessage];
}

-(void)sendMessage {
    if (self.gameObjectName == nil){
    }
    else {
        [TCGBUtil logDebugWithFormat:@"[TCGB][Plugin][UnityMessageSender] sendMessage jsonString : %s", [[self.message toJsonString] UTF8String]];
        UnitySendMessage([self.gameObjectName UTF8String], [self.requestMethodName UTF8String], [[self.message toJsonString] UTF8String]);
    }
}
@end