#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>

@interface UnityMessage : NSObject {
    NSString* _scheme;
    int _handle;
    NSString* _jsonData;
    NSString* _extraData;
    NSString* _gameObjectName;
    NSString* _requestMethodName;
}

@property (nonatomic, strong) NSString* scheme;
@property (nonatomic, assign) int handle;
@property (nonatomic, strong) NSString* jsonData;
@property (nonatomic, strong) NSString* extraData;
@property (nonatomic, strong) NSString* gameObjectName;
@property (nonatomic, strong) NSString* requestMethodName;

@end
