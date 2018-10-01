#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

using System;
using System.Collections.Generic;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class NativeGamebaseWebview : IGamebaseWebview
    {
        protected class GamebaseWebview
        {
            public const string WEBVIEW_API_SHOW_WEBBROWSER_DEPRECATED   = "gamebase://showWebBrowser?deprecated";
            public const string WEBVIEW_API_SHOW_WEBVIEW_DEPRECATED      = "gamebase://showWebView?deprecated";

            public const string WEBVIEW_API_OPEN_WEBBROWSER              = "gamebase://openWebBrowser";
            public const string WEBVIEW_API_SHOW_WEBVIEW                 = "gamebase://showWebView";            
            public const string WEBVIEW_API_CLOSE_WEBVIEW                = "gamebase://closeWebView";
            public const string WEBVIEW_API_SCHEME_EVENT                 = "gamebase://schemeEvent";
        }

        protected INativeMessageSender  messageSender   = null;
        protected string                CLASS_NAME      = string.Empty;

        public NativeGamebaseWebview()
        {
            Init();
        }

        virtual protected void Init()
        {
            messageSender.Initialize(CLASS_NAME);

            DelegateManager.AddDelegate(GamebaseWebview.WEBVIEW_API_SHOW_WEBVIEW, DelegateManager.SendErrorDelegateOnce, OnCloseCallback);
            DelegateManager.AddDelegate(GamebaseWebview.WEBVIEW_API_SCHEME_EVENT, DelegateManager.SendGamebaseDelegate<string>);
        }

        [Obsolete("ShowWebBrowser is deprecated.")]
        virtual public void ShowWebBrowser(string url)
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseWebview.WEBVIEW_API_SHOW_WEBBROWSER_DEPRECATED, jsonData: url, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        [Obsolete("ShowWebView is deprecated.")]
        virtual public void ShowWebView(GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration)
        {
            string jsonString = "";
            if(null != configuration)
            {
                jsonString  = JsonMapper.ToJson(configuration);
            }

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseWebview.WEBVIEW_API_SHOW_WEBVIEW_DEPRECATED, jsonData: jsonString, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void OpenWebBrowser(string url)
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseWebview.WEBVIEW_API_OPEN_WEBBROWSER, jsonData: url, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void ShowWebView(string url, GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = null, int closeCallback = -1, List<string> schemeList = null, int schemeEvent = -1)
        {
            NativeRequest.Webview.WebviewConfiguration webviewConfiguration = new NativeRequest.Webview.WebviewConfiguration();
            webviewConfiguration.url                                        = url;
            webviewConfiguration.configuration                              = configuration;

            string extraData    = JsonMapper.ToJson(new GamebaseRequest.Webview.SchemeConfiguration(schemeList, schemeEvent));
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseWebview.WEBVIEW_API_SHOW_WEBVIEW, handle: closeCallback, jsonData: JsonMapper.ToJson(webviewConfiguration), extraData: extraData, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void CloseWebView()
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseWebview.WEBVIEW_API_CLOSE_WEBVIEW));
            messageSender.GetAsync(jsonData);
        }

        virtual protected void OnCloseCallback(NativeMessage message)
        {
            if (false == string.IsNullOrEmpty(message.extraData))
            {
                int schemeEventHandle = int.Parse(message.extraData);
                GamebaseCallbackHandler.UnregisterCallback(schemeEventHandle);
            }
        }
    }
}

#endif
