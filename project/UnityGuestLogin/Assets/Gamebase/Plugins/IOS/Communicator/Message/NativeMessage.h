#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>

@interface NativeMessage : NSObject {
    NSString* _scheme;
    int _handle;
    NSString* _gamebaseError;
    NSString* _jsonData;
    NSString* _extraData;
}

@property (nonatomic, strong) NSString* scheme;
@property (nonatomic, assign) int handle;
@property (nonatomic, strong) NSString* gamebaseError;
@property (nonatomic, strong) NSString* jsonData;
@property (nonatomic, strong) NSString* extraData;

-(id)initWithMessage:(NSString*)scheme handle:(int)handle TCGBError:(TCGBError*)gamebaseError jsonData:(NSString*)jsonData extraData:(NSString*)extraData;;

-(NSString*)toJsonString;

@end
