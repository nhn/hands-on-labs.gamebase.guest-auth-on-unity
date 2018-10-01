#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toast.Gamebase.Single
{
    public class CommonGamebaseWebview : IGamebaseWebview
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                    return typeof(CommonGamebaseWebview).Name;

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        [Obsolete("ShowWebBrowser is deprecated.")]
        public virtual void ShowWebBrowser(string url)
        {
            Application.OpenURL(url);
        }

        [Obsolete("ShowWebView is deprecated.")]
        public virtual void ShowWebView(GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "ShowWebView");
        }

        public virtual void OpenWebBrowser(string url)
        {
            Application.OpenURL(url);
        }

        public virtual void ShowWebView(string url, GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = null, int closeCallback = -1, List<string> schemeList = null, int schemeEvent = -1)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "ShowWebView");
        }

        public virtual void CloseWebView()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "CloseWebView");
        }
    }
}
#endif