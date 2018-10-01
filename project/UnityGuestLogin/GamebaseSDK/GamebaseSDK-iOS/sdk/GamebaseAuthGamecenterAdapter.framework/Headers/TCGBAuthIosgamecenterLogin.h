//
//  TCGBAuthGamecenterLogin.h
//  TCGBAuthGamecenterAdapter
//
//  Created by NHNENT on 2016. 12. 13..
//  Copyright © 2016년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Gamebase/Gamebase.h>
#import <GameKit/GameKit.h>

#define GamebaseAuthGamecenterAdapterVersion @"1.2.2"

@interface TCGBAuthIosgamecenterLogin : NSObject <TCGBAuthAdapterDelegate>

@property (nonatomic, strong) NSString*         clientId;           // bundleId

@property (nonatomic, weak)   GKLocalPlayer*    localPlayer;
@property (nonatomic, strong) NSDictionary*     gameCenterAuthInfo;
@property (nonatomic, strong) NSString*         playerID;
@property (nonatomic, assign) BOOL              alreadyAuthenticated;
@property (nonatomic, assign) BOOL              callbackForAuthHandler;
@property (atomic, assign) BOOL                 isLoginViewPresented;

@property (nonatomic, strong) NSString*         snsToken;           // clientId + playerId


@end
