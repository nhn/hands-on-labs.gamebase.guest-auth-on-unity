#if !UNITY_EDITOR && UNITY_ANDROID
using Toast.Gamebase.Mobile.Android;
#elif !UNITY_EDITOR && UNITY_IOS
using Toast.Gamebase.Mobile.IOS;
#elif !UNITY_EDITOR && UNITY_WEBGL
using Toast.Gamebase.Single.WebGL;
#else
using Toast.Gamebase.Single.Standalone;
#endif

using System;
using Toast.Gamebase.LitJson;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Toast.Gamebase
{
    public sealed class GamebaseImplementation
    {
        private static readonly GamebaseImplementation instance = new GamebaseImplementation();

        public static GamebaseImplementation Instance
        {
            get { return instance; }
        }

        IGamebase sdk;

        private GamebaseImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            sdk = new AOSGamebase();
#elif !UNITY_EDITOR && UNITY_IOS
            sdk = new IOSGamebase();
#elif !UNITY_EDITOR && UNITY_WEBGL
            sdk = new WebGLGamebase();
#else
            sdk = new StandaloneGamebase();
#endif
        }

        public void SetDebugMode(bool isDebugMode)
        {
            sdk.SetDebugMode(isDebugMode);
        }

        public void Initialize(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> callback)
        {
            Initialize(GetGamebaseConfiguration(), callback);
        }

        public void Initialize(GamebaseRequest.GamebaseConfiguration configuration, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> callback)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Gamebase VERSION:{0}", GamebaseUnitySDK.SDKVersion));
            sb.AppendLine(string.Format("Gamebase Configuration:{0}", JsonMapper.ToJson(configuration)));
            GamebaseLog.Debug(string.Format("{0}", sb), this, "Initialize");
            
            if (true == string.IsNullOrEmpty(GamebaseUnitySDK.ObjectName))
            {
                GamebaseLog.Error(GamebaseStrings.NOT_FOUND_GAMEOBJECT, this, "Initialize");
                return;
            }

            InitializeUnitySDK();
            SetGamebaseConfiguration(configuration);

            GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> initializeCallback = (launchingInfo, error) =>
            {
                GamebaseWaterMark.ShowWaterMark();                

                IndicatorReport.Instance.SendIndicator(
                    IndicatorReport.Level.DEBUG,
                    IndicatorReport.FieldName.UNITY_EDITOR_VERSION,
                    new Dictionary<string, string> { { IndicatorReport.SEND_DATA_KEY_UNITY_EDITOR_VERSION, Application.unityVersion } }
                    );

                callback(launchingInfo, error);
            };

            int handle = GamebaseCallbackHandler.RegisterCallback(initializeCallback);            
            sdk.Initialize(configuration, handle);
        }
        
        public string GetSDKVersion()
        {
            return sdk.GetSDKVersion();
        }

        public string GetUserID()
        {
            return sdk.GetUserID();
        }

        public string GetAccessToken()
        {
            return sdk.GetAccessToken();
        }

        public string GetLastLoggedInProvider()
        {
            return sdk.GetLastLoggedInProvider();
        }

        public string GetLanguageCode()
        {
            return sdk.GetLanguageCode();
        }

        public string GetCarrierCode()
        {
            return sdk.GetCarrierCode();
        }

        public string GetCarrierName()
        {
            return sdk.GetCarrierName();
        }

        public string GetCountryCode()
        {
            return sdk.GetCountryCode();
        }

        public string GetCountryCodeOfUSIM()
        {
            return sdk.GetCountryCodeOfUSIM();
        }

        public string GetCountryCodeOfDevice()
        {
            return sdk.GetCountryCodeOfDevice();
        }

        public bool IsSandbox()
        {
            return sdk.IsSandbox();
        }

        public void SetDisplayLanguageCode(string languageCode)
        {
            sdk.SetDisplayLanguageCode(languageCode);
        }
        
        public string GetDisplayLanguageCode()
        {
            return sdk.GetDisplayLanguageCode();
        }
        
        private void InitializeUnitySDK()
        {
            InitializeLitJson();
        }

        private void InitializeLitJson()
        {
            JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(Convert.ToDouble(obj)));
            JsonMapper.RegisterImporter<float, int>((float input) => { return (int)input; });
            JsonMapper.RegisterImporter<float, long>((float input) => { return (long)input; });
            JsonMapper.RegisterImporter<int, long>((int input) => { return (long)input; });
            JsonMapper.RegisterImporter<int, double>((int input) => { return (double)input; });
            JsonMapper.RegisterImporter<int, float>((int input) => { return (float)input; });
            JsonMapper.RegisterImporter<double, int>((double input) => { return (int)input; });
            JsonMapper.RegisterImporter<double, long>((double input) => { return (long)input; });
            JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
        }

        private GamebaseRequest.GamebaseConfiguration GetGamebaseConfiguration()
        {
            var configuration                               = new GamebaseRequest.GamebaseConfiguration();
            configuration.appID                             = GamebaseUnitySDK.AppID;
            configuration.appVersion                        = GamebaseUnitySDK.AppVersion;
            configuration.zoneType                          = GamebaseUnitySDK.ZoneType;
            configuration.displayLanguageCode               = GamebaseUnitySDK.DisplayLanguageCode;
            configuration.enablePopup                       = GamebaseUnitySDK.EnablePopup;
            configuration.enableLaunchingStatusPopup        = GamebaseUnitySDK.EnableLaunchingStatusPopup;
            configuration.enableBanPopup                    = GamebaseUnitySDK.EnableBanPopup;
            configuration.enableKickoutPopup                = GamebaseUnitySDK.EnableKickoutPopup;
            configuration.fcmSenderId                       = GamebaseUnitySDK.FcmSenderId;
            configuration.storeCode                         = GamebaseUnitySDK.StoreCode;

            return configuration;
        }

        private void SetGamebaseConfiguration(GamebaseRequest.GamebaseConfiguration configuration)
        {
            GamebaseUnitySDK.AppID                          = configuration.appID;
            GamebaseUnitySDK.AppVersion                     = configuration.appVersion;
            GamebaseUnitySDK.ZoneType                       = configuration.zoneType;
            GamebaseUnitySDK.DisplayLanguageCode            = configuration.displayLanguageCode;
            GamebaseUnitySDK.EnablePopup                    = configuration.enablePopup;
            GamebaseUnitySDK.EnableLaunchingStatusPopup     = configuration.enableLaunchingStatusPopup;
            GamebaseUnitySDK.EnableBanPopup                 = configuration.enableBanPopup;
            GamebaseUnitySDK.EnableKickoutPopup             = configuration.enableKickoutPopup;
            GamebaseUnitySDK.FcmSenderId                    = configuration.fcmSenderId;
            GamebaseUnitySDK.StoreCode                      = configuration.storeCode;
        }

        public void AddObserver(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer)
        {
            GamebaseObserverManager.Instance.AddObserver(observer);
            sdk.AddObserver(GamebaseObserverManager.Instance.Handle);
        }

        public void RemoveObserver(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer)
        {
            GamebaseObserverManager.Instance.RemoveObserver(observer);
            sdk.RemoveObserver();
        }

        public void RemoveAllObserver()
        {
            GamebaseObserverManager.Instance.RemoveAllObserver();
            sdk.RemoveAllObserver();
        }

        public void AddServerPushEvent(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent)
        {
            GamebaseServerPushEventManager.Instance.AddServerPushEvent(serverPushEvent);
            sdk.AddServerPushEvent(GamebaseServerPushEventManager.Instance.Handle);
        }
        
        public void RemoveServerPushEvent(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent)
        {
            GamebaseServerPushEventManager.Instance.RemoveServerPushEvent(serverPushEvent);
            sdk.RemoveServerPushEvent();
        }
        
        public void RemoveAllServerPushEvent()
        {
            GamebaseServerPushEventManager.Instance.RemoveAllServerPushEvent();
            sdk.RemoveAllServerPushEvent();
        }
    }
}