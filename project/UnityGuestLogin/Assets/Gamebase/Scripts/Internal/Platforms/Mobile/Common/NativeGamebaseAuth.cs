#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

using System;
using System.Collections.Generic;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class NativeGamebaseAuth : IGamebaseAuth
    {
        protected class GamebaseAuth
        {
            public const string AUTH_API_LOGIN                               = "gamebase://login";
            public const string AUTH_API_LOGIN_ADDITIONAL_INFO               = "gamebase://loginWithAdditionalInfo";
            public const string AUTH_API_LOGIN_CREDENTIAL_INFO               = "gamebase://loginWithCredentialInfo";
            public const string AUTH_API_LOGIN_FOR_LAST_LOGGED_IN_PROVIDER   = "gamebase://loginForLastLoggedInProvider";
            public const string AUTH_API_LOGOUT                              = "gamebase://logout";
            public const string AUTH_API_ADD_MAPPING                         = "gamebase://addMapping";
            public const string AUTH_API_ADD_MAPPING_CREDENTIAL_INFO         = "gamebase://addMappingWithCredentialInfo";
            public const string AUTH_API_ADD_MAPPING_ADDITIONAL_INFO         = "gamebase://addMappingWithAdditionalInfo";
            public const string AUTH_API_REMOVE_MAPPING                      = "gamebase://removeMapping";
            public const string AUTH_API_WITH_DRAW_ACCOUT                    = "gamebase://withdraw";
            public const string AUTH_API_ISSUE_TRANSFER_KEY                  = "gamebase://issueTransferKey";
            public const string AUTH_API_REQUEST_TRANSFER                    = "gamebase://requestTransfer";
            public const string AUTH_API_GET_AUTH_MAPPING_LIST               = "gamebase://getAuthMappingList";
            public const string AUTH_API_GET_AUTH_PROVIDER_USERID            = "gamebase://getAuthProviderUserID";
            public const string AUTH_API_GET_AUTH_PROVIDER_ACCESSTOKEN       = "gamebase://getAuthProviderAccessToken";
            public const string AUTH_API_GET_AUTH_PROVIDER_PROFILE           = "gamebase://getAuthProviderProfile";
            public const string AUTH_API_GET_BAN_INFO                        = "gamebase://getBanInfo";
        }

        protected INativeMessageSender  messageSender   = null;
        protected string                CLASS_NAME      = string.Empty;
        

        public NativeGamebaseAuth()
        {
            Init();
        }

        virtual protected void Init()
        {
            messageSender.Initialize(CLASS_NAME);

            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_LOGIN,                                DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_LOGIN_ADDITIONAL_INFO,                DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_LOGIN_CREDENTIAL_INFO,                DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>, OnLogin);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_LOGIN_FOR_LAST_LOGGED_IN_PROVIDER,    DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_ADD_MAPPING,                          DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_ADD_MAPPING_CREDENTIAL_INFO,          DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>, OnAddMapping);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_ADD_MAPPING_ADDITIONAL_INFO,          DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_ISSUE_TRANSFER_KEY,                   DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.TransferKeyInfo>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_REQUEST_TRANSFER,                     DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Auth.AuthToken>);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_REMOVE_MAPPING,                       DelegateManager.SendErrorDelegateOnce, OnRemoveMapping);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_LOGOUT,                               DelegateManager.SendErrorDelegateOnce, OnLogout);
            DelegateManager.AddDelegate(GamebaseAuth.AUTH_API_WITH_DRAW_ACCOUT,                     DelegateManager.SendErrorDelegateOnce, OnWithdraw);
        }

        virtual public void Login(string providerName, int handle)
        {
            if (AuthAdapterManager.Instance.IsSupportedIDP(providerName))
            {
                InvokeCredentialInfoMethod(providerName, handle, "Login");
            }
            else
            {
                CallNativeLogin(providerName, handle);
            }
        }

        virtual public void Login(string providerName, Dictionary<string, object> additionalInfo, int handle)
        {
            var vo              = new NativeRequest.Auth.LoginWithAdditionalInfo();
            vo.providerName     = providerName;
            vo.additionalInfo   = additionalInfo;

            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_LOGIN_ADDITIONAL_INFO, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void Login(Dictionary<string, object> credentialInfo, int handle)
        {

            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_LOGIN_CREDENTIAL_INFO, handle: handle, jsonData: JsonMapper.ToJson(credentialInfo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void LoginForLastLoggedInProvider(int handle)
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_LOGIN_FOR_LAST_LOGGED_IN_PROVIDER, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void AddMapping(string providerName, int handle)
        {
            if (AuthAdapterManager.Instance.IsSupportedIDP(providerName))
            {
                InvokeCredentialInfoMethod(providerName, handle, "AddMapping");
            }
            else
            {
                CallNativeMapping(providerName, handle);
            }
        }

        virtual public void AddMapping(Dictionary<string, object> credentialInfo, int handle)
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_ADD_MAPPING_CREDENTIAL_INFO, handle: handle, jsonData: JsonMapper.ToJson(credentialInfo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void AddMapping(string providerName, Dictionary<string, object> additionalInfo, int handle)
        {
            var vo              = new NativeRequest.Auth.AddMappingWithAdditionalInfo();
            vo.providerName     = providerName;
            vo.additionalInfo   = additionalInfo;

            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_ADD_MAPPING_ADDITIONAL_INFO, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void RemoveMapping(string providerName, int handle)
        {
            var vo              = new NativeRequest.Auth.RemoveMapping();
            vo.providerName     = providerName;

            GamebaseExtraDataHandler.RegisterExtraData(handle, providerName);

            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_REMOVE_MAPPING, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void Logout(int handle)
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_LOGOUT, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void Withdraw(int handle)
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_WITH_DRAW_ACCOUT, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void IssueTransferKey(long expiresIn, int handle)
        {
            var vo = new NativeRequest.Auth.IssueTransferKey();
            vo.expiresIn = expiresIn;

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_ISSUE_TRANSFER_KEY, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void RequestTransfer(string transferKey, int handle)
        {
            var vo = new NativeRequest.Auth.RequestTransfer();
            vo.transferKey = transferKey;

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_REQUEST_TRANSFER, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public List<string> GetAuthMappingList()
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_GET_AUTH_MAPPING_LIST));
            return JsonMapper.ToObject<List<string>>(messageSender.GetSync(jsonData));
        }

        virtual public string GetAuthProviderUserID(string providerName)
        {
            if (true == AuthAdapterManager.Instance.IsUsableAdapter(providerName))
            {
                return AuthAdapterManager.Instance.GetIDPData<string>(providerName, AuthAdapterManager.MethodName.GET_IDP_USER_ID);
            }
            else
            {
                string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_GET_AUTH_PROVIDER_USERID, jsonData: providerName));
                return messageSender.GetSync(jsonData);
            }            
        }

        virtual public string GetAuthProviderAccessToken(string providerName)
        {
            if (true == AuthAdapterManager.Instance.IsUsableAdapter(providerName))
            {
                return AuthAdapterManager.Instance.GetIDPData<string>(providerName, AuthAdapterManager.MethodName.GET_IDP_ACCESS_TOKEN);
            }
            else
            {
                string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_GET_AUTH_PROVIDER_ACCESSTOKEN, jsonData: providerName));
                return messageSender.GetSync(jsonData);
            }
        }

        virtual public GamebaseResponse.Auth.AuthProviderProfile GetAuthProviderProfile(string providerName)
        {
            if (true == AuthAdapterManager.Instance.IsUsableAdapter(providerName))
            {
                return AuthAdapterManager.Instance.GetIDPData<GamebaseResponse.Auth.AuthProviderProfile>(providerName, AuthAdapterManager.MethodName.GET_IDP_PROFILE);
            }
            else
            {
                string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_GET_AUTH_PROVIDER_PROFILE, jsonData: providerName));
                string jsonString   = messageSender.GetSync(jsonData);

                if (true == string.IsNullOrEmpty(jsonString))
                {
                    return null;
                }
                else
                {
                    return JsonMapper.ToObject<GamebaseResponse.Auth.AuthProviderProfile>(jsonString);
                }
            }
        }

        virtual public GamebaseResponse.Auth.BanInfo GetBanInfo()
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_GET_BAN_INFO));
            string jsonString   = messageSender.GetSync(jsonData);

            if (true == string.IsNullOrEmpty(jsonString))
            {
                return null;
            }
            else
            {
                return JsonMapper.ToObject<GamebaseResponse.Auth.BanInfo>(jsonString);
            }
        }

        #region Handlers for IDP logout
        virtual protected void OnLogin(NativeMessage message)
        {
            GamebaseError error = message.GetGamebaseError();

            string providerName = GamebaseExtraDataHandler.GetExtraData(message.handle);
            GamebaseExtraDataHandler.UnregisterExtraData(message.handle);

            if (false == Gamebase.IsSuccess(error))
            {
                AuthAdapterManager.Instance.IDPLogout(providerName);
            }
        }

        virtual protected void OnAddMapping(NativeMessage message)
        {
            GamebaseError error = message.GetGamebaseError();

            string providerName = GamebaseExtraDataHandler.GetExtraData(message.handle);
            GamebaseExtraDataHandler.UnregisterExtraData(message.handle);

            if (false == Gamebase.IsSuccess(error))
            {
                AuthAdapterManager.Instance.IDPLogout(providerName);
            }
        }

        virtual protected void OnRemoveMapping(NativeMessage message)
        {
            GamebaseError error = message.GetGamebaseError();

            string providerName = GamebaseExtraDataHandler.GetExtraData(message.handle);
            GamebaseExtraDataHandler.UnregisterExtraData(message.handle);

            if (true == Gamebase.IsSuccess(error))
            {
                AuthAdapterManager.Instance.IDPLogout(providerName);
            }
        }

        virtual protected void OnWithdraw(NativeMessage message)
        {
            GamebaseError error = message.GetGamebaseError();
            if (true == Gamebase.IsSuccess(error))
            {
                AuthAdapterManager.Instance.IDPLogoutAll();
            }
        }

        virtual protected void OnLogout(NativeMessage message)
        {
            GamebaseError error = message.GetGamebaseError();
            if (true == Gamebase.IsSuccess(error))
            {
                AuthAdapterManager.Instance.IDPLogoutAll();
            }
        }
        #endregion

        virtual protected void InvokeCredentialInfoMethod(string providerName, int handle, string methodName)
        {
            bool hasAdapter = AuthAdapterManager.Instance.CreateIDPAdapter(providerName);

            if (hasAdapter)
            {
                AuthAdapterManager.Instance.GetIDPCredentialInfo(providerName, (credentialInfo, adapterError) =>
                {
                    if (Gamebase.IsSuccess(adapterError))
                    {
                        GamebaseExtraDataHandler.RegisterExtraData(handle, providerName);

                        if (methodName.Equals("Login", StringComparison.Ordinal))
                        {
                            Login(credentialInfo, handle);
                        }
                        else if (methodName.Equals("AddMapping", StringComparison.Ordinal))
                        {
                            AddMapping(credentialInfo, handle);
                        }
                    }
                    else
                    {
                        var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);

                        if (null == callback)
                        {
                            return;
                        }

                        AuthAdapterManager.Instance.IDPLogout(providerName);
                        callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, "AOSGamebaseAuth", error: adapterError));
                    }
                });
            }
            else
            {
                GamebaseLog.Debug("Call native method", this, "InvokeCredentialInfoMethod");

                if (true == methodName.Equals("Login", StringComparison.Ordinal))
                {
                    CallNativeLogin(providerName, handle);
                }
                else if (true == methodName.Equals("AddMapping", StringComparison.Ordinal))
                {
                    CallNativeMapping(providerName, handle);
                }
            }
        }

        protected void CallNativeLogin(string providerName, int handle)
        {
            var vo          = new NativeRequest.Auth.Login();
            vo.providerName = providerName;

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_LOGIN, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        protected void CallNativeMapping(string providerName, int handle)
        {
            var vo          = new NativeRequest.Auth.AddMapping();
            vo.providerName = providerName;

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseAuth.AUTH_API_ADD_MAPPING, handle: handle, jsonData: JsonMapper.ToJson(vo), gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }
    }
}

#endif