#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL)
using System.Collections.Generic;

namespace Toast.Gamebase.Single.Communicator
{
    public class LaunchingResponse
    {
        #region common data
        public class Request
        {
            public string clientVersion;
            public string deviceCountryCode;
            public string deviceKey;
            public string deviceModel;
            public string displayLanguage;
            public string deviceLanguage;
            public long lastCheckedNoticeTime;
            public string osCode;
            public string osVersion;
            public string sdkVersion;
            public string userId;
            public string usimCountryCode;
            public string uuid;
        }

        public class Status
        {
            public int code;
            public string message;
        }
        #endregion
        
        public class LaunchingInfo : BaseVO
        {
            public class Launching
            {
                public class App
                {
                    public class AccessInfo
                    {
                        public string serverAddress;
                        public string csInfo;
                    }
                    
                    public class RelatedUrls
                    {
                        public string termsUrl;
                        public string csUrl;
                        public string punishRuleUrl;
                        public string personalInfoCollectionUrl;
                    }

                    public class Install
                    {
                        public string url;
                    }

                    public class IDP
                    {
                        public string clientId;
                        public string clientSecret;
                        public string additional;
                    }

                    public class LoginUrls
                    {
                        public string gamebaseUrl;
                    }

                    public class Language
                    {
                        public string deviceLanguage;
                    }

                    public AccessInfo accessInfo;
                    public Dictionary<string, IDP> idP;
                    public Install install;
                    public RelatedUrls relatedUrls;
                    public Language language;
                    public string storeCode;
                    public string typeCode;
                    public LoginUrls loginUrls;
                }

                public class Maintenance
                {
                    public string url;
                    public string typeCode;
                    public string pageTypeCode;
                    public string reason;
                    public string message;
                    public string timezone;
                    public string beginDate;
                    public string endDate;
                    public long localBeginDate;
                    public long localEndDate;
                }

                public class Notice
                {
                    public string message;
                    public string title;
                    public string url;
                }

                public class TCGBClient
                {
                    public class Stability
                    {
                        public string appKey;
                        public string customKey;
                        public string logLevel;
                        public string url;
                        public bool useFlag;
                    }
                }

                public class LoginURL
                {
                    public string loginUrl;
                }

                public App app;
                public Maintenance maintenance;
                public Notice notice;
                public Status status;
                public TCGBClient tcgbClient;
                public LoginURL standalone;
            }

            public class TCIap
            {
                public string id;
                public string name;
                public string storeCode;
            }
            
            public string date;
            public CommonResponse.Header header;
            public Request request;
            public Launching launching;
            public List<TCIap> tcIap;
            public Dictionary<string, object> tcLaunching;
            public Dictionary<string, object> tcProduct;
        }

        public class LaunchingStatus : BaseVO
        {
            public class Launching
            {
                public Status status;
            }
            
            public Launching launching;
            public Request request;
            public string date;
        }

        public class HeartbeatInfo : BaseVO
        {
            public CommonResponse.Header header;
        }
    }
}
#endif