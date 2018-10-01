using System.Collections.Generic;

namespace Toast.Gamebase
{
    public interface IGamebaseWebview
    {
        void ShowWebBrowser(string url);
        void ShowWebView(GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration);

        void OpenWebBrowser(string url);
        void ShowWebView(string url, GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = null, int closeCallback = -1, List<string> schemeList = null, int schemeEvent = -1);        
        void CloseWebView();
    }
}