#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>

typedef void (^ServerPushMessage)(TCGBServerPushMessage*);
typedef void (^ObserverMessage)(TCGBObserverMessage*);

@interface TCGBGamebasePlugin : NSObject

@property (nonatomic, strong) ServerPushMessage serverPushMessage;
@property (nonatomic, strong) ObserverMessage observerMessage;
@property (nonatomic, strong) NSString *observerGameObjectName;
@property (nonatomic, strong) NSString *observerRequestMethodName;
@property (nonatomic, assign) int observerHandle;
@property (nonatomic, strong) NSString *serverPushEventGameObjectName;
@property (nonatomic, strong) NSString *serverPushEventRequestMethodName;
@property (nonatomic, assign) int serverPushEventHandle;

@end
