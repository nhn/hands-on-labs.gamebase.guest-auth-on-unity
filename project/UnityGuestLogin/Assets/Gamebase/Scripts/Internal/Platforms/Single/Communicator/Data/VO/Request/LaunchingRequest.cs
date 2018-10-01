#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL)
namespace Toast.Gamebase.Single.Communicator
{
    public class LaunchingRequest
    {
        public class ReqLaunchingInfoVO : BaseVO
        {
            public class Parameter
            {
                public string appId;
                public string userId;
                public string clientVersion;
                public string sdkVersion;
                public string uuid;
                public string deviceKey;
                public string osCode;
                public string osVersion;
                public string deviceModel;
                public string deviceLanguage;
                public string displayLanguage;
                public string deviceCountryCode;
                public string usimCountryCode;
                public long lcnt;
            }

            public Parameter parameter;

            public ReqLaunchingInfoVO()
            {
                parameter = new Parameter();
            }
        }

        public class HeartBeatVO : BaseVO
        {
            public class Parameter
            {
                public string appId;
            }

            public class Payload
            {
                public string appId;
                public string clientVersion;
                public string deviceCountryCode;
                public string osCode;
                public string userId;
                public string usimCountryCode;
            }

            public Parameter parameter;
            public Payload payload;

            public HeartBeatVO()
            {
                parameter = new Parameter();
                payload = new Payload();
            }
        }
    }
}
#endif