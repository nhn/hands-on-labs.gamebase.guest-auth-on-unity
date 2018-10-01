using System.Collections.Generic;

namespace Toast.Gamebase
{
    public class GamebaseResponse
    {
        public class SDK
        {
            public class ObserverMessage
            {
                public string type;
                public Dictionary<string, object> data;
            }

            public class ServerPushMessage
            {
                public string type;
                public string data;
            }
        }

        public class Launching
        {
            public class LaunchingInfo
            {
                public GamebaseLaunching launching;
                public TCProductInfo tcProduct;
                public List<TCIAPInfo> tcIap;                

                public class GamebaseLaunching
                {
                    public LaunchingStatus status;
                    public APP app;
                    public Maintenance maintenance;
                    public LaunchingNotice notice;
                    //public StandaloneInfo standalone;

                    public class APP
                    {
                        public AccessInfo accessInfo;
                        public RelatedURLs relatedUrls;
                        public Install install;
                        public Dictionary<string, LaunchingIDPInfo> idP;
                        public string typeCode;
                        public LoginUrls loginUrls;

                        public class AccessInfo
                        {
                            public string serverAddress;
                            public string csInfo;
                        }

                        public class RelatedURLs
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

                        public class LoginUrls
                        {
                            public string gamebaseUrl;
                        }

                        public class LaunchingIDPInfo
                        {
                            public string clientId;
                            public string clientSecret;
                            public string additional;
                        }                        
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

                    public class LaunchingNotice
                    {
                        public string message;
                        public string title;
                        public string url;
                    }
                }

                public class TCProductInfo
                {
                    public TCProductAppKeyInfo gamebase;
                    public TCProductAppKeyInfo tcLaunching;
                    public TCProductAppKeyInfo iap;
                    public TCProductAppKeyInfo push;

                    public class TCProductAppKeyInfo
                    {
                        public string appKey;
                    }
                }

                public class TCIAPInfo
                {
                    public string id;
                    public string name;
                    public string storeCode;
                }                
            } 

            public class LaunchingStatus
            {
                public int code;
                public string message;
            }
        }

        public class Auth
        {
            public class AuthToken
            {
                public Token token;                
                public Common.Member member;

                public class Token
                {
                    public string accessToken;
                    public string accessTokenSecret;
                }
            }

            public class AuthProviderProfile
            {
                public Dictionary<string, object> information;
            }

            public class BanInfo
            {
                public string userId;
                public string banType;
                public long beginDate;
                public long endDate;
                public string message;
                public string csInfo;
                public string csUrl;
            }

            public class TransferKeyInfo
            {
                public string transferKey;
                public long regDate;
                public long expireDate;
            }
        }

        public class Purchase
        {
            public class PurchasableReceipt
            {
                public long itemSeq;
                public float price;
                public string currency;
                public string paymentSeq;
                public string purchaseToken;
            }

            public class PurchasableRetryTransactionResult
            {
                public List<PurchasableReceipt> successList;
                public List<PurchasableReceipt> failList;
            }

            public class PurchasableItem
            {
                public long itemSeq;
                public float price;
                public string currency;
                public string itemName;
                public string marketItemId;
            }
        }

        public class Push
        {
            public class PushConfiguration
            {
                public bool pushEnabled;
                public bool adAgreement;
                public bool adAgreementNight;
                public string displayLanguageCode;
            }
        }

        public class Util
        {

        }

        public class Common
        {
            public class Member
            {
                public class AuthMappingInfo
                {
                    public string authKey;
                    public string auauthSystemthKey;
                    public string idPCode;
                    public string oauthProviderCode;
                    public long regDate;
                    public string userId;
                }

                public string appId;
                public List<AuthMappingInfo> authList;
                public long lastLoginDate;
                public long regDate;
                public string userId;
                public string valid;
            }
        }
    }
}