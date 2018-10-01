#import <Foundation/Foundation.h>

typedef void (^LaunchingStatus)(NSDictionary*);

@interface TCGBLaunchingPlugin : NSObject

@property (nonatomic, strong) LaunchingStatus launchingStatus;
@property (nonatomic, strong) NSString *gameObjectName;
@property (nonatomic, strong) NSString *requestMethodName;
@property (nonatomic, assign) int handle;

@end
