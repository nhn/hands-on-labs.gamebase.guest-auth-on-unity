#import <Foundation/Foundation.h>
#import "NativeMessage.h"

@interface UnityMessageSender : NSObject {
    NSString* _gameObjectName;
    NSString* _requestMethodName;
    NativeMessage* _message;
}

@property (nonatomic, strong) NSString* gameObjectName;
@property (nonatomic, strong) NSString* requestMethodName;
@property (nonatomic, strong) NativeMessage* message;

+(UnityMessageSender*)sharedUnityMessageSender;

-(void)sendMessage:(NativeMessage*)message gameObjectName:(NSString*)gameObjectName requestMethodName:(NSString*)requestMethodName;

@end
