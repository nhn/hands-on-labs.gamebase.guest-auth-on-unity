//
//  TCGBNetwork.h
//  Gamebase
//
//  Created by NHNENT on 2017. 1. 9..
//  Copyright © 2017년 NHN Entertainment Corp. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TCGBConstants.h"

#ifndef TCGBNETWORK_H
#define TCGBNETWORK_H

typedef void(^NetworkChangedHandler)(NetworkStatus status);

/** The TCGBNetwork class indicates Network status.
 */
@interface TCGBNetwork : NSObject

/**---------------------------------------------------------------------------------------
 * @name Network Status
 *  ---------------------------------------------------------------------------------------
 */

/**
 @return NetworkStatus
 @see NetworkStatus
 */
+ (NetworkStatus)type;

/**
 @return Stringify to NetworkStatus
 @see NetworkStatus
 */
+ (NSString *)typeName;

/**
 @return `YES` if network is reachable.
 */
+ (BOOL)isConnected;

/**
 @param handler When network status is changed, this handler is called.
 @return Result that invoked method is whether succeeded or failed.
 @deprecated As of release 1.8.0, use TCGBGamebase.addObserver: method instead.
 */
+ (BOOL)addObserverOnChangedNetworkStatusWithHandler:(NetworkChangedHandler)handler
DEPRECATED_MSG_ATTRIBUTE("Use TCGBGamebase.addObserver method instead");

/**
 @param handler what you want to remove.
 @return Result that invoked method is whether succeeded or failed.
 @deprecated As of release 1.8.0, use TCGBGamebase.removeObserver: method instead.
 */
+ (BOOL)removeObserverOnChangedNetworkStatusWithHandler:(NetworkChangedHandler)handler
DEPRECATED_MSG_ATTRIBUTE("Use TCGBGamebase.removeObserver method instead");

@end

#endif
