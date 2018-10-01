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
using System;

namespace Toast.Gamebase
{
    public sealed class GamebaseWebviewImplementation
    {
        private static readonly GamebaseWebviewImplementation instance = new GamebaseWebviewImplementation();
        
        public static GamebaseWebviewImplementation Instance
        {
            get { return instance; }
        }

        IGamebaseWebview webview;

        private GamebaseWebviewImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            webview = new AOSGamebaseWebview();
#elif !UNITY_EDITOR && UNITY_IOS
            webview = new IOSGamebaseWebview();
#elif !UNITY_EDITOR && UNITY_WEBGL
            webview = new WebGLGamebaseWebview();
#else
            webview = new StandaloneGamebaseWebview();
#endif
        }

        [Obsolete("ShowWebBrowser is deprecated.")]
        public void ShowWebBrowser(string url)
        {
            webview.ShowWebBrowser(url);
        }

        [Obsolete("ShowWebView is deprecated.")]
        public void ShowWebView(GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration)
        {
            webview.ShowWebView(configuration);
        }

        public void OpenWebBrowser(string url)
        {
            webview.OpenWebBrowser(url);
        }

        public void ShowWebView(string url, GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = null, GamebaseCallback.ErrorDelegate closeCallback = null, List<string> schemeList = null, GamebaseCallback.GamebaseDelegate<string> schemeEvent = null)
        {

            int closeCallbackHandle = -1;
            int schemeEventHandle = -1;

            if (null != closeCallback)
            {
                closeCallbackHandle =  GamebaseCallbackHandler.RegisterCallback(closeCallback);
            }

            if (null != schemeEvent)
            {
                schemeEventHandle = GamebaseCallbackHandler.RegisterCallback(schemeEvent);
            }

            webview.ShowWebView(url, configuration, closeCallbackHandle, schemeList, schemeEventHandle);
        }

        public void CloseWebView()
        {
            webview.CloseWebView();
        }
    }
}