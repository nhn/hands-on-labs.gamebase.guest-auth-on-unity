#if UNITY_EDITOR || UNITY_WEBGL

using System;
using System.Runtime.InteropServices;

namespace Toast.Gamebase.Single.WebGL
{
    public class WebGLGamebaseWebview : CommonGamebaseWebview
    {
        [DllImport("__Internal")]
        private extern static void OpenBrowser(string url);

        public WebGLGamebaseWebview()
        {
            Domain = typeof(WebGLGamebaseWebview).Name;
        }

        [Obsolete("ShowWebBrowser is deprecated.")]
        public override void ShowWebBrowser(string url)
        {
            OpenBrowser(url);
        }

        public override void OpenWebBrowser(string url)
        {
            OpenBrowser(url);
        }
    }
}
#endif