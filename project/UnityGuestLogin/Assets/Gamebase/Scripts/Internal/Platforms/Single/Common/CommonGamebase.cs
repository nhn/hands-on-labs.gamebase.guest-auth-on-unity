#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System.Collections;
using Toast.Gamebase.Single.Communicator;

namespace Toast.Gamebase.Single
{
    public class CommonGamebase : IGamebase
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                    return typeof(CommonGamebase).Name;

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        private int initializeHandle = -1;

        public void SetDebugMode(bool isDebugMode)
        {
            GamebaseLog.SetDebugMode(isDebugMode);
        }
        
        public void Initialize(GamebaseRequest.GamebaseConfiguration configuration, int handle)
        {
            initializeHandle = handle;
            GamebaseUnitySDKSettings.Instance.StartCoroutine(Init());
        }

        public string GetSDKVersion()
        {
            return GamebaseUnitySDK.SDKVersion;
        }

        public string GetUserID()
        {
            var vo = DataContainer.GetData<AuthResponse.LoginInfo>(VOKey.Auth.LOGIN_INFO);
            if (vo == null)
                return string.Empty;

            return vo.member.userId;
        }

        public string GetAccessToken()
        {
            var vo = DataContainer.GetData<AuthResponse.LoginInfo>(VOKey.Auth.LOGIN_INFO);
            if (vo == null)
                return string.Empty;

            return vo.token.accessToken;
        }

        public string GetLastLoggedInProvider()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetLastLoggedInProvider");
            return string.Empty;
        }

        public string GetLanguageCode()
        {
            return GamebaseUnitySDK.Language;
        }

        public string GetCarrierCode()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetCarrierCode");
            return string.Empty;
        }

        public string GetCarrierName()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetCarrierName");
            return string.Empty;
        }

        public string GetCountryCode()
        {
            return GamebaseUnitySDK.CountryCode;
        }

        public string GetCountryCodeOfUSIM()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetCountryCodeOfUSIM");
            return string.Empty;
        }

        public string GetCountryCodeOfDevice()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetCountryCodeOfDevice");
            return string.Empty;
        }

        public bool IsSandbox()
        {
            if (GamebaseUnitySDK.IsInitialized == false)
            {
                GamebaseLog.Warn(GamebaseStrings.NOT_INITIALIZED, this, "IsSandbox");
                return false;
            }

            var vo = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
            return vo.launching.app.typeCode.Equals("SANDBOX", System.StringComparison.Ordinal);
        }

        public void SetDisplayLanguageCode(string languageCode)
        {
            if (true == DisplayLanguage.Instance.HasLocalizedStringVO(languageCode))
            {
                GamebaseUnitySDK.DisplayLanguageCode = languageCode;
            }
            else
            {
                var launchingVO = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
                var launchingDeviceLanguageCode = launchingVO.launching.app.language.deviceLanguage;

                if (true == DisplayLanguage.Instance.HasLocalizedStringVO(launchingDeviceLanguageCode))
                {
                    GamebaseUnitySDK.DisplayLanguageCode = launchingDeviceLanguageCode;
                    GamebaseLog.Warn(GamebaseStrings.DISPLAY_LANGUAGE_CODE_NOT_FOUND, this, "SetDisplayLanguageCode");
                }
                else
                {
                    GamebaseUnitySDK.DisplayLanguageCode = GamebaseDisplayLanguageCode.English;
                    GamebaseLog.Warn(GamebaseStrings.SET_DEFAULT_DISPLAY_LANGUAGE_CODE, this, "SetDisplayLanguageCode");
                }
            }
        }

        public string GetDisplayLanguageCode()
        {
            return GamebaseUnitySDK.DisplayLanguageCode;
        }

        private IEnumerator Init()
        {
            yield return DisplayLanguage.Instance.DisplayLanguageInitialize();

            WebSocket.Instance.Initialize();
            yield return GamebaseUnitySDKSettings.Instance.StartCoroutine(WebSocket.Instance.Connect((error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                if (error == null)
                {
                    GamebaseLaunchingImplementation.Instance.RequestLaunchingInfo(initializeHandle);
                    return;
                }

                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo>>(initializeHandle);
                if (callback == null)
                    return;

                error.domain = Domain;
                callback(null, error);
            }));
        }

        public string GetDisplayLanguage()
        {
            return string.Empty;
        }

        public void AddObserver(int handle)
        {

        }

        public void RemoveObserver()
        {

        }

        public void RemoveAllObserver()
        {

        }

        public void AddServerPushEvent(int handle)
        {

        }

        public void RemoveServerPushEvent()
        {

        }

        public void RemoveAllServerPushEvent()
        {

        }    
    }
}
#endif