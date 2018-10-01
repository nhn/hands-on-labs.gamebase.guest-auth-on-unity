#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
using System.Collections.Generic;

namespace Toast.Gamebase.Mobile
{
    public class NativeRequest
    {
        public class SDK
        {
            public class Initialize
            {
                public string gameObjectName;
                public List<string> pluginList;
            }

            public class IsDebugMode
            {
                public bool isDebugMode;
            }
        }

        public class Launching
        {

        }

        public class Auth
        {
            public class Login
            {
                public string providerName;
            }

            public class LoginWithAdditionalInfo
            {
                public string providerName;
                public Dictionary<string, object> additionalInfo;
            }            
            
            public class AddMapping
            {
                public string providerName;
            }

            public class AddMappingWithAdditionalInfo
            {
                public string providerName;
                public Dictionary<string, object> additionalInfo;
            }

            public class RemoveMapping
            {
                public string providerName;
            }

            public class IssueTransferKey
            {
                public long expiresIn;
            }

            public class RequestTransfer
            {
                public string transferKey;
            }
        }

        public class Purchase
        {
            public class PurchaseItemSeq
            {
                public long itemSeq;
            }
        }

        public class Push
        {
            public class RegisterPush
            {
                public Dictionary<string, object> options;
            }

            public class Enable
            {
                public bool enable;
            }

            public class IsSandboxMode
            {
                public bool isSandbox;
            }
        }

        public class Webview
        {
            public class WebviewConfiguration
            {
                public string url;
                public GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration;                
            }
        }

        public class Util
        {
            public class AlertDialog
            {
                public string title;
                public string message;
                public int duration;
            }
        }
    }
}
#endif