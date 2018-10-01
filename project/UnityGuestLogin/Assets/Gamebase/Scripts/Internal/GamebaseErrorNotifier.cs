namespace Toast.Gamebase
{
    public class GamebaseErrorNotifier
    {
        public static void FireNotSupportedAPI(object classObj, string methodName, GamebaseCallback.ErrorDelegate callback)
        {
            if (callback == null)
                return;

            GamebaseLog.Warn(string.Format("{0}", CreateNotSupportedMessage()), classObj, methodName);
            string message = string.Format("{0} - {1}.", methodName, CreateNotSupportedMessage());
            callback(CreateNotSupportedError(classObj.GetType().Name, message));
        }

        public static void FireNotSupportedAPI<T>(object classObj, string methodName, GamebaseCallback.DataDelegate<T> callback)
        {
            if (callback == null)
                return;
            
            GamebaseLog.Warn(string.Format("{0}", CreateNotSupportedMessage()), classObj, methodName);
            callback(default(T));
        }

        public static void FireNotSupportedAPI<T>(object classObj, string methodName, GamebaseCallback.GamebaseDelegate<T> callback)
        {
            if (callback == null)
                return;

            GamebaseLog.Warn(string.Format("{0}", CreateNotSupportedMessage()), classObj, methodName);
            string message = string.Format("{0} - {1}.", methodName, CreateNotSupportedMessage());
            callback(default(T), CreateNotSupportedError(classObj.GetType().Name, message));
        }

        public static void FireNotSupportedAPI(object classObj, string methodName, GamebaseCallback.VoidDelegate callback)
        {
            if (callback == null)
                return;

            GamebaseLog.Warn(string.Format("{0}", CreateNotSupportedMessage()), classObj, methodName);
            callback();
        }

        public static void FireNotSupportedAPI(object classObj, string methodName)
        {
            GamebaseLog.Warn(string.Format("{0}", CreateNotSupportedMessage()), classObj, methodName);
        }
        
        private static GamebaseError CreateNotSupportedError(string domain, string message)
        {
#if UNITY_EDITOR
			return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED_UNITY_EDITOR, domain, message:message);
#elif UNITY_ANDROID
            return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED_ANDROID, domain, message:message);
#elif UNITY_IOS
            return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED_IOS, domain, message:message);
#elif UNITY_STANDALONE
			return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED_UNITY_STANDALONE, domain, message:message);
#elif UNITY_WEBGL
            return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED_UNITY_WEBGL, domain, message:message);
#else
            return new GamebaseError(GamebaseErrorCode.NOT_SUPPORTED, domain, message:message);
#endif
        }

        private static string CreateNotSupportedMessage()
        {
#if UNITY_EDITOR
    #if UNITY_EDITOR_WIN
			return GamebaseStrings.NOT_SUPPORTED_UNITY_EDITOR_WIN;
    #elif UNITY_EDITOR_OSX
			return GamebaseStrings.NOT_SUPPORTED_UNITY_EDITOR_OSX;
    #else
            return GamebaseStrings.NOT_SUPPORTED_UNITY_EDITOR;
    #endif
#elif UNITY_ANDROID
            return GamebaseStrings.NOT_SUPPORTED_ANDROID;
#elif UNITY_IOS
            return GamebaseStrings.NOT_SUPPORTED_IOS;
#elif UNITY_STANDALONE
    #if UNITY_STANDALONE_WIN
			return GamebaseStrings.NOT_SUPPORTED_UNITY_STANDALONE_WIN;
    #elif UNITY_STANDALONE_OSX
			return GamebaseStrings.NOT_SUPPORTED_UNITY_STANDALONE_OSX;
    #else
			return GamebaseStrings.NOT_SUPPORTED_UNITY_STANDALONE;
    #endif
#elif UNITY_WEBGL
            return GamebaseStrings.NOT_SUPPORTED_UNITY_WEBGL;
#else
            return GamebaseStrings.NOT_SUPPORTED;
#endif
        }
    }
}