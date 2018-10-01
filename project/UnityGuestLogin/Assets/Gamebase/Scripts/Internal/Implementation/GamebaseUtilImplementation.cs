#if !UNITY_EDITOR && UNITY_ANDROID
using Toast.Gamebase.Mobile.Android;
#elif !UNITY_EDITOR && UNITY_IOS
using Toast.Gamebase.Mobile.IOS;
#elif !UNITY_EDITOR && UNITY_WEBGL
using Toast.Gamebase.Single.WebGL;
#else
using Toast.Gamebase.Single.Standalone;
#endif
using System.Collections.Generic;

namespace Toast.Gamebase
{
    public sealed class GamebaseUtilImplementation
    {
        private static readonly GamebaseUtilImplementation instance = new GamebaseUtilImplementation();
        
        public static GamebaseUtilImplementation Instance
        {
            get { return instance; }
        }

        IGamebaseUtil util;

        private GamebaseUtilImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            util = new AOSGamebaseUtil();
#elif !UNITY_EDITOR && UNITY_IOS
            util = new IOSGamebaseUtil();
#elif !UNITY_EDITOR && UNITY_WEBGL
            util = new WebGLGamebaseUtil();
#else
            util = new StandaloneGamebaseUtil();
#endif
        }

        public void ShowAlert(string title, string message)
        {
            util.ShowAlert(title, message);
        }

        public void ShowAlert(string title, string message, GamebaseCallback.VoidDelegate buttonCallback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(buttonCallback);

            util.ShowAlert(title, message, handle);
        }

        public void ShowToast(string message, int duration)
        {
            util.ShowToast(message, duration);
        }

        public void ShowToast(string message, GamebaseUIToastType type)
        {
            util.ShowToast(message, type);
        }
        
        public void ShowAlert(Dictionary<string, string> parameters, GamebaseUtilAlertType alertType, GamebaseCallback.DataDelegate<GamebaseUtilAlertButtonID> buttonCallback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(buttonCallback);
            util.ShowAlert(parameters, alertType, handle);
        }
    }
}