#if UNITY_EDITOR || UNITY_STANDALONE
using System.Collections.Generic;
using System.Text;
using Toast.Gamebase.Single.Communicator;
using UnityEngine;

namespace Toast.Gamebase.Single.Standalone
{
    public class StandaloneGamebaseAuth : CommonGamebaseAuth
    {
        private const string SCHEME_AUTH_LOGIN = "gamebase://toast.gamebase/auth";
        private const string ACCESS_TOKEN_KEY = "token";

        public StandaloneGamebaseAuth()
        {
            Domain = typeof(StandaloneGamebaseAuth).Name;
        }

        public override void Login(string providerName, int handle)
        {
            CheckLaunchingStatusExpire(() =>
            {
                LoginWithProviderName(providerName, handle);
            });
        }

        public override void Login(string providerName, Dictionary<string, object> additionalInfo, int handle)
        {
            if (false == AuthAdapterManager.Instance.IsSupportedIDP(providerName))
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                GamebaseErrorNotifier.FireNotSupportedAPI(
                    this,
                    string.Format("LoginWithAdditionalInfo({0})", providerName),
                    callback);
                GamebaseCallbackHandler.UnregisterCallback(handle);
                return;
            }

            if (false == CanLogin(handle))
            {
                return;
            }
            
            bool hasAdapter = AuthAdapterManager.Instance.CreateIDPAdapter(providerName);

            if (false == hasAdapter)
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, message: GamebaseStrings.AUTH_ADAPTER_NOT_FOUND_NEED_SETUP));
                GamebaseCallbackHandler.UnregisterCallback(handle);
            }

            AuthAdapterManager.Instance.IDPLogin(additionalInfo, (adapterError) =>
            {
                if (Gamebase.IsSuccess(adapterError))
                {
                    var IDPAccessToken = AuthAdapterManager.Instance.GetIDPData<string>(providerName, AuthAdapterManager.MethodName.GET_IDP_ACCESS_TOKEN);
                    var requestVO = AuthMessage.GetIDPLoginMessage(providerName, IDPAccessToken);
                    RequestGamebaseLogin(requestVO, handle);

                    return;
                }

                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                if (null == callback)
                {
                    return;
                }

                GamebaseCallbackHandler.UnregisterCallback(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, Domain, error: adapterError));
                AuthAdapterManager.Instance.IDPLogout(providerName);
            });
        }

        public override void Logout(int handle)
        {
            if (false == GamebaseUnitySDK.UseWebview)
            {
                base.Logout(handle);
                return;
            }

            if (false == CanLogout(handle))
            {
                return;
            }

            var requestVO = AuthMessage.GetLogoutMessage();

            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(handle);

                if (null == callback)
                {
                    return;
                }

                GamebaseCallbackHandler.UnregisterCallback(handle);
                
                DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                Heartbeat.Instance.StopHeartbeat();

                callback(null);
            });
        }

        public override void Withdraw(int handle)
        {
            if (false == GamebaseUnitySDK.UseWebview)
            {
                base.Withdraw(handle);
                return;
            }

            if (false == CanLogout(handle))
            {
                return;
            }

            var requestVO = AuthMessage.GetWithdrawMessage();

            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(handle);

                if (null == callback)
                {
                    return;
                }

                GamebaseCallbackHandler.UnregisterCallback(handle);
                
                DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                Heartbeat.Instance.StopHeartbeat();

                callback(null);
            });
        }

        private void LoginWithProviderName(string providerName, int handle)
        {
            if (false == GamebaseUnitySDK.UseWebview)
            {
                base.Login(providerName, handle);
                return;
            }

            if (false == IsSupportedIDPByWebview(providerName))
            {
                base.Login(providerName, handle);
                return;
            }

            if (false == CanLogin(handle))
            {
                return;
            }

            if (GamebaseAuthProvider.GUEST == providerName)
            {
                var requestVO = AuthMessage.GetIDPLoginMessage(providerName);
                RequestGamebaseLogin(requestVO, handle);
                return;
            }

            bool hasAdapter = WebviewAdapterManager.Instance.CreateWebviewAdapter("standalonewebviewadapter");
            if (false == hasAdapter)
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                GamebaseCallbackHandler.UnregisterCallback(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, message: GamebaseStrings.WEBVIEW_ADAPTER_NOT_FOUND));
                return;
            }

            isAuthenticationAlreadyProgress = true;

            GetAccessToken(providerName, (requestVO) =>
            {
                if (null == requestVO)
                {
                    isAuthenticationAlreadyProgress = false;
                    LoginFailedCallback(handle);
                    return;
                }

                RequestGamebaseLogin(requestVO, handle);
            });
        }
        
        private void GetAccessToken(string providerName, System.Action<WebSocketRequest.RequestVO> callback)
        {
            WebSocketRequest.RequestVO requestVO = null;

            GamebaseResponse.Launching.LaunchingInfo launchingInfo = Gamebase.Launching.GetLaunchingInformations();

            if( null == launchingInfo.launching.app.loginUrls || 
                true == string.IsNullOrEmpty(launchingInfo.launching.app.loginUrls.gamebaseUrl))
            {
#if UNITY_EDITOR
                GamebaseLog.Error("You need to switch platform the Standalone.", this, "GetAccessToken");
#else                
                GamebaseLog.Debug("launchingInfo.launching.standalone is null.", this, "GetAccessToken");
#endif
                callback(requestVO);
                return;
            }

            if(null == launchingInfo)
            {
                GamebaseLog.Debug("launchingInfo is null.", this, "GetAccessToken");
                callback(requestVO);
                return;
            }

            GamebaseResponse.Launching.LaunchingInfo.GamebaseLaunching.APP.LaunchingIDPInfo launchingIDPInfo = launchingInfo.launching.app.idP["gbid"];

            if(null == launchingIDPInfo)
            {
                GamebaseLog.Debug("gbid is null.", this, "GetAccessToken");
                callback(requestVO);
                return;
            }

            string clientID = launchingIDPInfo.clientId;

            StringBuilder url = new StringBuilder(launchingInfo.launching.app.loginUrls.gamebaseUrl);
            url.Append("?clientId=").Append(clientID);
            url.Append("&snsCd=").Append(providerName);
            url.Append("&svcUrl=").Append(WWW.EscapeURL(SCHEME_AUTH_LOGIN));
            url.Append("&tokenKind=").Append("SNS");

            WebviewAdapterManager.Instance.ShowWebView(
                url.ToString(),
                null,
                (error) =>
                {
                    callback(requestVO);
                },
                new List<string>() { SCHEME_AUTH_LOGIN },
                (scheme, error) =>
                {
                    WebviewAdapterManager.SchemeInfo schemeInfo = WebviewAdapterManager.Instance.ConvertURLToSchemeInfo(scheme);

                    if (true == schemeInfo.parameterDictionary.ContainsKey(ACCESS_TOKEN_KEY))
                    {
                        var IDPAccessToken = schemeInfo.parameterDictionary[ACCESS_TOKEN_KEY];
                        requestVO = AuthMessage.GetIDPLoginMessage(providerName, IDPAccessToken);

                        WebviewAdapterManager.Instance.CloseWebView();
                    }
                }
                );
        }

        private bool IsSupportedIDPByWebview(string providerName)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            return providerName == GamebaseAuthProvider.GUEST ||
                   providerName == GamebaseAuthProvider.FACEBOOK ||
                   providerName == GamebaseAuthProvider.GOOGLE ||
                   providerName == GamebaseAuthProvider.NAVER ||
                   providerName == GamebaseAuthProvider.PAYCO;
#elif UNITY_FACEBOOK || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            return providerName == GamebaseAuthProvider.GUEST;
#endif
        }

        private void LoginFailedCallback(int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseCallbackHandler.UnregisterCallback(handle);
            callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED));
        }
    }
}
#endif