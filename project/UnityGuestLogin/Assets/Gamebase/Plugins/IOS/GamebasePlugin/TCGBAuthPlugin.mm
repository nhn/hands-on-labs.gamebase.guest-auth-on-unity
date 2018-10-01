#import "TCGBAuthPlugin.h"
#import "DelegateManager.h"
#import <Gamebase/Gamebase.h>
#import "TCGBUnityDictionaryJSON.h"
#import "UnityMessageSender.h"
#import "UnityMessage.h"
#import "NativeMessage.h"

#define AUTH_API_LOGIN                                  @"gamebase://login"
#define AUTH_API_LOGIN_ADDITIONAL_INFO                  @"gamebase://loginWithAdditionalInfo"
#define AUTH_API_LOGIN_CREDENTIAL_INFO                  @"gamebase://loginWithCredentialInfo"
#define AUTH_API_LOGIN_FOR_LAST_LOGGED_IN_PROVIDER      @"gamebase://loginForLastLoggedInProvider"
#define AUTH_API_LOGOUT                                 @"gamebase://logout"
#define AUTH_API_ADD_MAPPING                            @"gamebase://addMapping"
#define AUTH_API_ADD_MAPPING_CREDENTIAL_INFO            @"gamebase://addMappingWithCredentialInfo"
#define AUTH_API_ADD_MAPPING_ADDITIONAL_INFO            @"gamebase://addMappingWithAdditionalInfo"
#define AUTH_API_REMOVE_MAPPING                         @"gamebase://removeMapping"
#define AUTH_API_WITH_DRAW_ACCOUT                       @"gamebase://withdraw"
#define AUTH_API_WITH_ISSUE_TRANSFER_KEY                @"gamebase://issueTransferKey"
#define AUTH_API_WITH_REQUEST_TRANSFER                   @"gamebase://requestTransfer"
#define AUTH_API_GET_AUTH_MAPPING_LIST                  @"gamebase://getAuthMappingList"
#define AUTH_API_GET_AUTH_PROVIDER_USERID               @"gamebase://getAuthProviderUserID"
#define AUTH_API_GET_AUTH_PROVIDER_ACCESSTOKEN          @"gamebase://getAuthProviderAccessToken"
#define AUTH_API_GET_AUTH_PROVIDER_PROFILE              @"gamebase://getAuthProviderProfile"
#define AUTH_API_GET_BAN_INFO                           @"gamebase://getBanInfo"


@implementation TCGBAuthPlugin

- (instancetype)init {
    if ((self = [super init]) == nil) {
        return nil;
    }

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_LOGIN target:self selector:@selector(login:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_LOGIN_ADDITIONAL_INFO target:self selector:@selector(loginWithAdditionalInfo:)];

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_LOGIN_CREDENTIAL_INFO target:self selector:@selector(loginWithCredentialInfo:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_LOGIN_FOR_LAST_LOGGED_IN_PROVIDER target:self selector:@selector(loginForLastLoggedInProvider:)];

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_LOGOUT target:self selector:@selector(logout:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_ADD_MAPPING target:self selector:@selector(addMapping:)];

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_ADD_MAPPING_CREDENTIAL_INFO target:self selector:@selector(addMappingWithCredentialInfo:)];

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_ADD_MAPPING_ADDITIONAL_INFO target:self selector:@selector(addMappingWithAdditionalInfo:)];
    
    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_REMOVE_MAPPING target:self selector:@selector(removeMapping:)];

    [[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_WITH_DRAW_ACCOUT target:self selector:@selector(withdraw:)];
	
	[[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_WITH_ISSUE_TRANSFER_KEY target:self selector:@selector(issueTransferKey:)];
	
	[[DelegateManager sharedDelegateManager] addAsyncDelegate:AUTH_API_WITH_REQUEST_TRANSFER target:self selector:@selector(requestTransfer:)];
	
    [[DelegateManager sharedDelegateManager] addSyncDelegate:AUTH_API_GET_AUTH_MAPPING_LIST target:self selector:@selector(getAuthMappingList:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:AUTH_API_GET_AUTH_PROVIDER_USERID target:self selector:@selector(getAuthProviderUserID:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:AUTH_API_GET_AUTH_PROVIDER_ACCESSTOKEN target:self selector:@selector(getAuthProviderAccessToken:)];
    
    [[DelegateManager sharedDelegateManager] addSyncDelegate:AUTH_API_GET_AUTH_PROVIDER_PROFILE target:self selector:@selector(getAuthProviderProfile:)];

    [[DelegateManager sharedDelegateManager] addSyncDelegate:AUTH_API_GET_BAN_INFO target:self selector:@selector(getBanInfo:)];
    
    return self;
    }

-(void)login:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    
    [TCGBGamebase loginWithType:convertedDic[@"providerName"] viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken description] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)loginWithAdditionalInfo:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    
    [TCGBGamebase loginWithType:convertedDic[@"providerName"] additionalInfo:convertedDic[@"additionalInfo"] viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)loginWithCredentialInfo:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    
    [TCGBGamebase loginWithCredential:convertedDic viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
        
    }];
}

-(void)loginForLastLoggedInProvider:(UnityMessage*)message {
    [TCGBGamebase loginForLastLoggedInProviderWithViewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)logout:(UnityMessage*)message {
    [TCGBGamebase logoutWithViewController:UnityGetGLViewController() completion:^(TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:nil extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)addMapping:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    [TCGBGamebase addMappingWithType:convertedDic[@"providerName"] viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)addMappingWithCredentialInfo:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    
    [TCGBGamebase addMappingWithCredential:convertedDic viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
    }];
}

-(void)addMappingWithAdditionalInfo:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    [TCGBGamebase addMappingWithType:convertedDic[@"providerName"] additionalInfo:convertedDic[@"additionalInfo"] viewController:UnityGetGLViewController() completion:^(id authToken, TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken JSONString] extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)removeMapping:(UnityMessage*)message {
    NSDictionary* convertedDic = [message.jsonData JSONDictionary];
    [TCGBGamebase removeMappingWithType:convertedDic[@"providerName"] viewController:UnityGetGLViewController() completion:^(TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:nil extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)withdraw:(UnityMessage*)message {
    [TCGBGamebase withdrawWithViewController:UnityGetGLViewController() completion:^(TCGBError *error) {
        NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:nil extraData:nil];
        [[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];

    }];
}

-(void)issueTransferKey:(UnityMessage*)message {
	NSDictionary* convertedDic = [message.jsonData JSONDictionary];
	[TCGBGamebase issueTransferKeyWithExpiresIn:([convertedDic[@"expiresIn"] longValue]) completion:^(TCGBTransferKeyInfo *transferKeyInfo, TCGBError *error) {
		NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[transferKeyInfo description] extraData:nil];
		[[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
	}];
}

-(void)requestTransfer:(UnityMessage*)message {
	NSDictionary* convertedDic = [message.jsonData JSONDictionary];
	[TCGBGamebase requestTransferWithTransferKey:convertedDic[@"transferKey"] completion:^(id authToken, TCGBError *error) {
		NativeMessage* requestMessage = [[NativeMessage alloc]initWithMessage:message.scheme handle:message.handle TCGBError:error jsonData:[authToken description] extraData:nil];
		[[UnityMessageSender sharedUnityMessageSender] sendMessage:requestMessage gameObjectName:message.gameObjectName requestMethodName:message.requestMethodName];
	}];
}

-(NSString*)getAuthMappingList:(UnityMessage*)message {
    NSString* result = [[TCGBGamebase authMappingList] JSONStringFromArray];
    return result;
}

-(NSString*)getAuthProviderUserID:(UnityMessage*)message {
    NSString* result = [TCGBGamebase authProviderUserIDWithIDPCode:message.jsonData];
    return result;
}

-(NSString*)getAuthProviderAccessToken:(UnityMessage*)message {
    NSString* result = [TCGBGamebase authProviderAccessTokenWithIDPCode:message.jsonData];
    return result;
}

-(NSString*)getAuthProviderProfile:(UnityMessage*)message {
    TCGBAuthProviderProfile* profil = [TCGBGamebase authProviderProfileWithIDPCode:message.jsonData];
    if(profil == nil)
    {
        return nil;
    }
    NSString* result = [profil description];
    return result;
}

-(NSString*)getBanInfo:(UnityMessage*)message {
    TCGBBanInfo* info = [TCGBGamebase banInfo];
    if(info == nil)
    {
        return nil;
    }
    NSString* result = [info description];    
    return result;
}
@end
