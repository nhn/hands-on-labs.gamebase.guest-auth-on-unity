#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>

typedef void (^OnNetworkStatus)(NetworkStatus);

@interface TCGBNetworkPlugin : NSObject

@property (nonatomic, strong) OnNetworkStatus networkStatus;
@property (nonatomic, strong) NSString *gameObjectName;
@property (nonatomic, strong) NSString *requestMethodName;
@property (nonatomic, assign) int handle;

@end
