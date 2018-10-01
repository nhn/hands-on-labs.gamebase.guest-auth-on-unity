#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL)

namespace Toast.Gamebase.Single.Communicator
{
    public class CommunicatorConfiguration
    {
        public const int connectionTimeout  = 10;
        public const int timeout            = 5;
        public const int heartbeatInterval  = 120;
        public const int launchingInterval  = 120;
        public const int launchingExpire    = 30;
    }
}
#endif