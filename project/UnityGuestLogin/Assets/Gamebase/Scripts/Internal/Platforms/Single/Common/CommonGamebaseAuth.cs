#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System;
using System.Collections.Generic;
using System.Text;
using Toast.Gamebase.LitJson;
using Toast.Gamebase.Single.Communicator;

namespace Toast.Gamebase.Single
{
    public class CommonGamebaseAuth : IGamebaseAuth
    {
        private string domain;
        protected bool isAuthenticationAlreadyProgress = false;

        public string Domain
        {
            get
            {
                if (true == string.IsNullOrEmpty(domain))
                {
                    return typeof(CommonGamebaseAuth).Name;
                }

                return domain;
            }
            set
            {
                domain = value;
            }
        }
        
        public virtual void Login(string providerName, int handle)
        {
            CheckLaunchingStatusExpire(()=> 
            {
                LoginWithProviderName(providerName, handle);
            });
        }

        private void LoginWithProviderName(string providerName, int handle)
        {
            if (false == AuthAdapterManager.Instance.IsSupportedIDP(providerName))
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                GamebaseErrorNotifier.FireNotSupportedAPI(
                    this,
                    string.Format("Login({0})", providerName),
                    callback);
                GamebaseCallbackHandler.UnregisterCallback(handle);
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

            bool hasAdapter = AuthAdapterManager.Instance.CreateIDPAdapter(providerName);

            if (false == hasAdapter)
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, message: GamebaseStrings.AUTH_ADAPTER_NOT_FOUND_NEED_SETUP));
                GamebaseCallbackHandler.UnregisterCallback(handle);
            }

            AuthAdapterManager.Instance.IDPLogin((adapterError) =>
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

        public virtual void Login(string providerName, Dictionary<string, object> additionalInfo, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "Login(additionalInfo)",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void Login(Dictionary<string, object> credentialInfo, int handle)
        {
            CheckLaunchingStatusExpire(() =>
            {
                LoginWithCredentialInfo(credentialInfo, handle);
            });
        }

        private void LoginWithCredentialInfo(Dictionary<string, object> credentialInfo, int handle)
        {
            if (false == CanLogin(handle))
            {
                return;
            }

            if (null == credentialInfo ||
                false == credentialInfo.ContainsKey(GamebaseAuthProviderCredential.PROVIDER_NAME) ||
                false == (credentialInfo.ContainsKey(GamebaseAuthProviderCredential.ACCESS_TOKEN) && false == credentialInfo.ContainsKey(GamebaseAuthProviderCredential.AUTHORIZATION_CODE)))
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_INVALID_IDP_INFO, Domain));
                GamebaseCallbackHandler.UnregisterCallback(handle);
                return;
            }

            var providerName = (string)credentialInfo[GamebaseAuthProviderCredential.PROVIDER_NAME];
            var accessToken = string.Empty;
            var authCode = string.Empty;

            if (true == credentialInfo.ContainsKey(GamebaseAuthProviderCredential.ACCESS_TOKEN))
            {
                accessToken = (string)credentialInfo[GamebaseAuthProviderCredential.ACCESS_TOKEN];
            }

            if (true == credentialInfo.ContainsKey(GamebaseAuthProviderCredential.AUTHORIZATION_CODE))
            {
                authCode = (string)credentialInfo[GamebaseAuthProviderCredential.AUTHORIZATION_CODE];
            }

            var requestVO = AuthMessage.GetIDPLoginMessage(providerName, accessToken, authCode);
            RequestGamebaseLogin(requestVO, handle);
        }

        public void LoginForLastLoggedInProvider(int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this, 
                "LoginForLastLoggedInProvider",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void AddMapping(string providerName, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "AddMapping",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void AddMapping(string providerName, Dictionary<string, object> additionalInfo, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "AddMapping(additionalInfo)",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void AddMapping(Dictionary<string, object> credentialInfo, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "AddMapping(additionalInfo)",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void RemoveMapping(string providerName, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RemoveMapping",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public virtual void Logout(int handle)
        {
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
                #region error
                //if (error == null)
                //{
                //    var vo = JsonMapper.ToObject<AuthResponse.LogoutInfo>(response);
                //    if (vo.header.isSuccessful)
                //        DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                //    else
                //        error = GamebaseErrorUtil.CreateGamebaseErrorByServerErrorCode(requestVO.apiId, vo.header.resultCode, Domain, vo.header.resultMessage);
                //}

                //callback(error);
                #endregion

                // Regardless of the error, it is considered a success.
                DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                callback(null);

                Heartbeat.Instance.StopHeartbeat();
                AuthAdapterManager.Instance.IDPLogoutAll();
                PurchaseAdapterManager.Instance.Destroy();
            });
        }

        public virtual void Withdraw(int handle)
        {
            if (false == CanLogout(handle))
            {
                return;
            }

            var requestVO = AuthMessage.GetWithdrawMessage();

            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(handle);
                if (null == callback)
                {
                    return;
                }
                GamebaseCallbackHandler.UnregisterCallback(handle);

                if (null == error)
                {
                    var vo = JsonMapper.ToObject<AuthResponse.WithdrawInfo>(response);
                    if (true == vo.header.isSuccessful)
                    {
                        DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                        Heartbeat.Instance.StopHeartbeat();
                        AuthAdapterManager.Instance.IDPLogoutAll();
                        PurchaseAdapterManager.Instance.Destroy();
                    }
                    else
                    {
                        if (GamebaseServerErrorCode.MEMBER_ALREADY_WITHDRAWN == vo.header.resultCode)
                        {
                            DataContainer.RemoveData(VOKey.Auth.LOGIN_INFO);
                            Heartbeat.Instance.StopHeartbeat();
                        }
                        else
                        {
                            error = GamebaseErrorUtil.CreateGamebaseErrorByServerErrorCode(requestVO.transactionId, requestVO.apiId, vo.header, Domain);
                            GamebaseSystemPopup.Instance.ShowErrorPopup(error);
                        }
                    }
                }
                else
                {
                    GamebaseSystemPopup.Instance.ShowErrorPopup(error);
                }
                callback(error);
            });
        }

        public void IssueTransferKey(long expiresIn, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.TransferKeyInfo>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "IssueTransferKey",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public void RequestTransfer(string transferKey, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RequestTransfer",
                callback);
            GamebaseCallbackHandler.UnregisterCallback(handle);
        }

        public List<string> GetAuthMappingList()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetAuthMappingList");
            return null;
        }

        public string GetAuthProviderUserID(string providerName)
        {
            if (true == providerName.Equals(GamebaseAuthProvider.GUEST, StringComparison.Ordinal))
            {
                return string.Empty;
            }

            return AuthAdapterManager.Instance.GetIDPData<string>(providerName, AuthAdapterManager.MethodName.GET_IDP_USER_ID);
        }

        public string GetAuthProviderAccessToken(string providerName)
        {
            if (true == providerName.Equals(GamebaseAuthProvider.GUEST, StringComparison.Ordinal))
            {
                return string.Empty;
            }

            return AuthAdapterManager.Instance.GetIDPData<string>(providerName, AuthAdapterManager.MethodName.GET_IDP_ACCESS_TOKEN);
        }

        public GamebaseResponse.Auth.AuthProviderProfile GetAuthProviderProfile(string providerName)
        {
            if (GamebaseAuthProvider.GUEST == providerName)
            {
                GamebaseLog.Debug("Guest does not have profile information.", this, "GetAuthProviderProfile");
                return null;
            }   

            if (true == string.IsNullOrEmpty(Gamebase.GetUserID()))
            {
                GamebaseLog.Debug(GamebaseStrings.NOT_LOGGED_IN, this, "GetAuthProviderProfile");
                return null;
            }

            return AuthAdapterManager.Instance.GetIDPData<GamebaseResponse.Auth.AuthProviderProfile>(providerName, AuthAdapterManager.MethodName.GET_IDP_PROFILE);
        }

        protected bool CanLogin(int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);

            if (true == GamebaseUnitySDK.IsInitialized)
            {
                if (false == CommonGamebaseLaunching.IsPlayable())
                {
                    if (null == callback)
                    {
                        GamebaseLog.Warn(GamebaseStrings.AUTH_NOT_PLAYABLE, this, "Login");
                        return false;
                    }
                    GamebaseCallbackHandler.UnregisterCallback(handle);
                    callback(null, new GamebaseError(GamebaseErrorCode.AUTH_NOT_PLAYABLE, Domain));
                    return false;
                }
            }
            else
            {
                if (null == callback)
                {
                    GamebaseLog.Warn(GamebaseStrings.NOT_INITIALIZED, this, "Login");
                    return false;
                }
                GamebaseCallbackHandler.UnregisterCallback(handle);
                callback(null, new GamebaseError(GamebaseErrorCode.NOT_INITIALIZED, Domain));
                return false;
            }

            if (true == string.IsNullOrEmpty(Gamebase.GetUserID()))
            {
                if(false == isAuthenticationAlreadyProgress)
                {
                    return true;
                }
                else
                {
                    if (null == callback)
                    {
                        GamebaseLog.Warn(GamebaseStrings.AUTH_ALREADY_IN_PROGRESS_ERROR, this, "Login");
                        return false;
                    }
                    GamebaseCallbackHandler.UnregisterCallback(handle);
                    callback(null, new GamebaseError(GamebaseErrorCode.AUTH_ALREADY_IN_PROGRESS_ERROR, Domain));
                    return false;
                }
            }

            if (null == callback)
            {
                return false;
            }

            GamebaseCallbackHandler.UnregisterCallback(handle);
            callback(null, new GamebaseError(GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED, Domain, GamebaseStrings.ALREADY_LOGGED_IN));
            return false;
        }

        protected bool CanLogout(int handle)
        {
            if (true == string.IsNullOrEmpty(Gamebase.GetUserID()))
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(handle);
                if (null == callback)
                {
                    return false;
                }
                GamebaseCallbackHandler.UnregisterCallback(handle);
                callback(new GamebaseError(GamebaseErrorCode.NOT_LOGGED_IN, Domain));
                return false;
            }

            return true;
        }

        protected void RequestGamebaseLogin(WebSocketRequest.RequestVO requestVO, int handle)
        {
            isAuthenticationAlreadyProgress = true;

            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken>>(handle);
                if (null == callback)
                {
                    return;
                }

                GamebaseCallbackHandler.UnregisterCallback(handle);

                if (null == error)
                {
                    var vo = JsonMapper.ToObject<AuthResponse.LoginInfo>(response);
                    if (true == vo.header.isSuccessful)
                    {
                        DataContainer.SetData(VOKey.Auth.LOGIN_INFO, vo);
                        Heartbeat.Instance.StartHeartbeat();
                    }
                    else
                    {
                        error = GamebaseErrorUtil.CreateGamebaseErrorByServerErrorCode(requestVO.transactionId, requestVO.apiId, vo.header, Domain);
                        if (null != vo.ban)
                        {
                            DataContainer.SetData(VOKey.Auth.BAN_INFO, vo.ban);

                            GamebaseSystemPopup.Instance.ShowErrorPopup(error, vo);
                        }
                    }
                }
                else
                {
                    GamebaseSystemPopup.Instance.ShowErrorPopup(error);
                }

                isAuthenticationAlreadyProgress = false;

                if (null == error)
                {
                    callback(JsonMapper.ToObject<GamebaseResponse.Auth.AuthToken>(response), error);
                }
                else
                {
                    callback(null, error);
                }
            });
        }
        
        protected void CheckLaunchingStatusExpire(Action callback)
        {
            if (CommunicatorConfiguration.launchingExpire >= GamebaseLaunchingImplementation.Instance.GetLaunchingStatusExpire())
            {
                GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> launchingInfoCallback = (launchingInfo, error) =>
                {
                    callback();
                };

                int handle = GamebaseCallbackHandler.RegisterCallback(launchingInfoCallback);

                GamebaseLaunchingImplementation.Instance.RequestLaunchingStatus(handle);

                return;
            }

            callback();
        }

        public GamebaseResponse.Auth.BanInfo GetBanInfo()
        {
            var vo = DataContainer.GetData<AuthResponse.LoginInfo.Ban>(VOKey.Auth.BAN_INFO);
            var launchingVO = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
            var banVO = new GamebaseResponse.Auth.BanInfo();

            banVO.userId = vo.userId;
            banVO.banType = vo.banType;
            banVO.beginDate = vo.beginDate;
            banVO.endDate = vo.endDate;
            banVO.message = vo.message;
            banVO.csInfo = launchingVO.launching.app.accessInfo.csInfo;
            banVO.csUrl = launchingVO.launching.app.relatedUrls.csUrl;
            return banVO;
        }
    }
}
#endif