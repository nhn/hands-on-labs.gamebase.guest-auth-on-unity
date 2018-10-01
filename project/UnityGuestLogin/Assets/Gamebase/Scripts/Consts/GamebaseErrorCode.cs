namespace Toast.Gamebase
{
    public class GamebaseErrorCode
    {
        //----------------------------------------
        //  Success
        //----------------------------------------
        public const int SUCCESS                                            = 0;

        //----------------------------------------
        //  Common
        //----------------------------------------
        public const int NOT_INITIALIZED                                    = 1;
        public const int NOT_LOGGED_IN                                      = 2;
        public const int INVALID_PARAMETER                                  = 3;
        public const int INVALID_JSON_FORMAT                                = 4;
        public const int USER_PERMISSION                                    = 5;
        public const int INVALID_MEMBER                                     = 6;
        public const int BANNED_MEMBER                                      = 7;
        public const int SAME_REQUESTOR                                     = 8;
        public const int NOT_GUEST_OR_HAS_OTHERS                            = 9;

        public const int NOT_SUPPORTED                                      = 10;
        public const int NOT_SUPPORTED_ANDROID                              = 11; // <UNITY ONLY>
        public const int NOT_SUPPORTED_IOS                                  = 12; // <UNITY ONLY>
        public const int NOT_SUPPORTED_UNITY_EDITOR                         = 13; // <UNITY ONLY>
        public const int NOT_SUPPORTED_UNITY_STANDALONE                     = 14; // <UNITY ONLY>
        public const int NOT_SUPPORTED_UNITY_WEBGL                          = 15; // <UNITY ONLY>
        public const int ANDROID_ACTIVITY_DESTROYED                         = 31; // <ANDROID ONLY>
        public const int ANDROID_ACTIVEAPP_NOT_CALLED                       = 32; // <ANDROID ONLY>
        public const int IOS_GAMECENTER_DENIED                              = 51; // <IOS ONLY>
        public const int UNKNOWN_ERROR                                      = 999;

        //----------------------------------------
        //  Network (Socket)
        //----------------------------------------
        public const int SOCKET_RESPONSE_TIMEOUT                            = 101;
        public const int SOCKET_ERROR                                       = 110;

        //----------------------------------------
        //  Launching
        //----------------------------------------
        public const int LAUNCHING_SERVER_ERROR                             = 2001;
        public const int LAUNCHING_NOT_EXIST_CLIENT_ID                      = 2002;
        public const int LAUNCHING_UNREGISTERED_APP                         = 2003;
        public const int LAUNCHING_UNREGISTERED_CLIENT                      = 2004;

        //----------------------------------------
        //  Auth
        //----------------------------------------
        public const int AUTH_USER_CANCELED                                 = 3001;
        public const int AUTH_NOT_SUPPORTED_PROVIDER                        = 3002;
        public const int AUTH_NOT_EXIST_MEMBER                              = 3003;
        public const int AUTH_INVALID_MEMBER                                = 3004;
        public const int AUTH_BANNED_MEMBER                                 = 3005;

        public const int AUTH_EXTERNAL_LIBRARY_ERROR                        = 3009;
        public const int AUTH_ALREADY_IN_PROGRESS_ERROR                     = 3010;
        public const int AUTH_TRANSFERKEY_EXPIRED                           = 3031;
        public const int AUTH_TRANSFERKEY_CONSUMED                          = 3032;
        public const int AUTH_TRANSFERKEY_NOT_EXIST                         = 3033;
        // tokenLogin
        public const int AUTH_TOKEN_LOGIN_FAILED                            = 3101;
        public const int AUTH_TOKEN_LOGIN_INVALID_TOKEN_INFO                = 3102;
        public const int AUTH_TOKEN_LOGIN_INVALID_LAST_LOGGED_IN_IDP        = 3103;
        // idPLogin
        public const int AUTH_IDP_LOGIN_FAILED                              = 3201;
        public const int AUTH_IDP_LOGIN_INVALID_IDP_INFO                    = 3202;
        // addMapping
        public const int AUTH_ADD_MAPPING_FAILED = 3301;
        public const int AUTH_ADD_MAPPING_ALREADY_MAPPED_TO_OTHER_MEMBER    = 3302;
        public const int AUTH_ADD_MAPPING_ALREADY_HAS_SAME_IDP              = 3303;
        public const int AUTH_ADD_MAPPING_INVALID_IDP_INFO                  = 3304;
        public const int AUTH_ADD_MAPPING_CANNOT_ADD_GUEST_IDP              = 3305;
        // removeMapping
        public const int AUTH_REMOVE_MAPPING_FAILED                         = 3401;
        public const int AUTH_REMOVE_MAPPING_LAST_MAPPED_IDP                = 3402;
        public const int AUTH_REMOVE_MAPPING_LOGGED_IN_IDP                  = 3403;
        // logout
        public const int AUTH_LOGOUT_FAILED                                 = 3501;
        // withdraw
        public const int AUTH_WITHDRAW_FAILED                               = 3601;
        // status not playable
        public const int AUTH_NOT_PLAYABLE                                  = 3701;
        // unknown
        public const int AUTH_UNKNOWN_ERROR                                 = 3999;

        //----------------------------------------
        //  Purchase
        //----------------------------------------
        public const int PURCHASE_NOT_INITIALIZED                           = 4001;
        public const int PURCHASE_USER_CANCELED                             = 4002;
        public const int PURCHASE_NOT_FINISHED_PREVIOUS_PURCHASING          = 4003;
        public const int PURCHASE_NOT_ENOUGH_CASH                           = 4004;

        public const int PURCHASE_NOT_SUPPORTED_MARKET                      = 4010;

        public const int PURCHASE_EXTERNAL_LIBRARY_ERROR                    = 4201;

        public const int PURCHASE_UNKNOWN_ERROR                             = 4999;

        //----------------------------------------
        //  Push
        //----------------------------------------
        public const int PUSH_NOT_REGISTERED                                = 5001;
        public const int PUSH_EXTERNAL_LIBRARY_ERROR                        = 5101;
        public const int PUSH_ALREADY_IN_PROGRESS_ERROR                     = 5102;

        public const int PUSH_UNKNOWN_ERROR                                 = 5999;

        //----------------------------------------
        //  UI
        //----------------------------------------
        public const int UI_UNKNOWN_ERROR                                   = 6999;

        //----------------------------------------
        //  Webview
        //----------------------------------------
        public const int WEBVIEW_UNKNOWN_ERROR                              = 7999;

        //----------------------------------------
        //  Server
        //----------------------------------------
        public const int SERVER_INTERNAL_ERROR                              = 8001;
        public const int SERVER_REMOTE_SYSTEM_ERROR                         = 8002;

        public const int SERVER_UNKNOWN_ERROR                               = 8999;

        //----------------------------------------
        //  Platform specified errors (iOS)
        //----------------------------------------
        public const int INVALID_INTERNAL_STATE                             = 11001;
        public const int NOT_CALLABLE_STATE                                 = 11002;

        //----------------------------------------
        //  Platform specified errors (Unity)
        //----------------------------------------
        public const int NATIVE_METHOD_CALL_EXCEPTION_IN_PLUGIN             = 12001;

        //Kickout
    }
}