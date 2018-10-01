using UnityEngine;

namespace Toast.Gamebase
{
    public class GamebaseUnitySDKSettings : MonoBehaviour
    {
        public string   appID                       = string.Empty;
        public string   appVersion                  = string.Empty;
        public bool     isDebugMode                 = false;
        
        public string   displayLanguageCode         = string.Empty;
        public string   zoneType                    = string.Empty;

        public bool     enablePopup                 = false;
        public bool     enableLaunchingStatusPopup  = true;
        public bool     enableBanPopup              = true;
        public bool     enableKickoutPopup          = true;

        public string   storeCodeIOS                = string.Empty;        
        public string   storeCodeAOS                = string.Empty;
        public string   storeCodeWebGL              = string.Empty;
        public string   storeCodeStandalone         = string.Empty;

        public string   fcmSenderId                 = string.Empty;
        
        public bool     useWebview                  = false;

        private bool    isQuit                      = false;

        public static GamebaseUnitySDKSettings Instance { get; private set; }

		public static T AddComponent<T>() where T : Component
		{
			return Instance.gameObject.AddComponent<T>();
		}

#pragma warning disable 0108
		public static T GetComponent<T>()
        {
            return Instance.gameObject.GetComponent<T>();
		}

		void Awake()
		{
			Instance = this;

			DontDestroyOnLoad(gameObject);

            GamebaseUnitySDK.ObjectName                     = this.name;
            GamebaseUnitySDK.AppID                          = appID;
            GamebaseUnitySDK.AppVersion                     = appVersion;
            GamebaseUnitySDK.DisplayLanguageCode            = displayLanguageCode;
            GamebaseUnitySDK.ZoneType                       = (string.IsNullOrEmpty(zoneType)) ? "real" : zoneType.ToLower();
            GamebaseUnitySDK.EnablePopup                    = enablePopup;
            GamebaseUnitySDK.EnableLaunchingStatusPopup     = enableLaunchingStatusPopup;
            GamebaseUnitySDK.EnableBanPopup                 = enableBanPopup;
            GamebaseUnitySDK.EnableKickoutPopup             = enableKickoutPopup;
            GamebaseUnitySDK.FcmSenderId                    = fcmSenderId;

#if !UNITY_EDITOR && UNITY_ANDROID
            GamebaseUnitySDK.StoreCode                      = storeCodeAOS.ToUpper();
#elif !UNITY_EDITOR && UNITY_IOS
            GamebaseUnitySDK.StoreCode                      = storeCodeIOS.ToUpper();
#elif !UNITY_EDITOR && UNITY_WEBGL
            GamebaseUnitySDK.StoreCode                      = storeCodeWebGL.ToUpper();
#elif UNITY_STANDALONE
            GamebaseUnitySDK.StoreCode                      = storeCodeStandalone.ToUpper();
#else
            GamebaseUnitySDK.StoreCode                      = string.Empty;
#endif
            GamebaseUnitySDK.UseWebview                     = useWebview;

            GamebaseImplementation.Instance.SetDebugMode(isDebugMode);
        }

        void OnApplicationQuit()
        {
            isQuit = true;
        }

        void OnDestroy()
        {
            if (true == isQuit)
            {
                return;
            }

            GamebaseLog.Error("Do not destroy this gameObject in order to receive callback.", this, "OnDestroy");
        }
    }
}