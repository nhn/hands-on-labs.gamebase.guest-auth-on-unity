using System;
using System.Collections.Generic;

namespace Toast.Gamebase
{
    public class GamebaseRequest
    {
        public class GamebaseConfiguration
        {
            public string   appID;
            public string   appVersion;
            public string   zoneType;
            public string   displayLanguageCode;
            public bool     enablePopup;
            public bool     enableLaunchingStatusPopup;
            public bool     enableBanPopup;
            public bool     enableKickoutPopup;
            public string   storeCode;
            public string   fcmSenderId;
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

        public class Webview
        {
            public class GamebaseWebViewConfiguration
            {
                public string title;
                public int orientation;
                public int colorR;
                public int colorG;
                public int colorB;
                public int colorA;
                public int barHeight;
                public bool isBackButtonVisible;
                public string backButtonImageResource;
                public string closeButtonImageResource;
                [Obsolete]
                public string url;
            }

            public class SchemeConfiguration
            {
                public List<string> schemeList;
                public int schemeEvent;

                public SchemeConfiguration()
                {

                }

                public SchemeConfiguration(List<string> schemeList, int schemeEvent)
                {
                    this.schemeList = schemeList;
                    this.schemeEvent = schemeEvent;
                }
            }
        }
    }    
}