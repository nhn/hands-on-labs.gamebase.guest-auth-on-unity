#import <Foundation/Foundation.h>
#import "NativeMessage.h"
#import "TCGBUnityDictionaryJSON.h"

@implementation NativeMessage

@synthesize scheme = _scheme;
@synthesize handle = _handle;
@synthesize gamebaseError = _gamebaseError;
@synthesize jsonData = _jsonData;
@synthesize extraData = _extraData;


-(id)initWithMessage:(NSString*)scheme handle:(int)handle TCGBError:(TCGBError*)gamebaseError jsonData:(NSString*)jsonData extraData:(NSString*)extraData{
    if(self = [super init]) {
        
        self.scheme = scheme;
        self.handle = handle;
        if(gamebaseError != nil)
            self.gamebaseError = [gamebaseError jsonString];
        if(jsonData != nil)
            self.jsonData = jsonData;
        if(extraData != nil)
            self.extraData = extraData;
    }
    return self;
}

-(NSString*)toJsonString {
    NSMutableDictionary* jsonDic = [NSMutableDictionary dictionary];
    jsonDic[@"scheme"] = _scheme;
    jsonDic[@"handle"] = [NSNumber numberWithInt:_handle];
    jsonDic[@"gamebaseError"] = _gamebaseError;
    jsonDic[@"jsonData"] = _jsonData;
    jsonDic[@"extraData"] = _extraData;
    
    NSString* jsonString = [jsonDic JSONString];
    
    return jsonString;
}

@end

