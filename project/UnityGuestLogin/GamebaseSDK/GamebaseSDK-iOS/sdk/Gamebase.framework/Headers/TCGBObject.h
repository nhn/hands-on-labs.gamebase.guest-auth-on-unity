//
//  TCGBJSONDelegate.h
//  Gamebase
//
//  Created by NHNEnt on 2018. 4. 20..
//  Copyright © 2018년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol TCGBObject <NSObject>
@required
- (NSString *)JSONString;
- (NSString *)JSONPrettyString;
@end

@interface TCGBObject : NSObject <TCGBObject>

@end
