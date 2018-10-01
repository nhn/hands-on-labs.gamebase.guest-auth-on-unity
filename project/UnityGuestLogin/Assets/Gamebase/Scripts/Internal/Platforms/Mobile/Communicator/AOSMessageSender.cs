#if UNITY_EDITOR || UNITY_ANDROID
using UnityEngine;

namespace Toast.Gamebase.Mobile.Android
{
    public sealed class AOSMessageSender : INativeMessageSender
    {
        private static readonly AOSMessageSender    instance                        = new AOSMessageSender();
        private const string                        GAMEBASE_ANDROID_PLUGIN_CLASS   = "com.toast.gamebase.communicator.UnityMessageReceiver";
        private AndroidJavaClass                    jc                              = null;
        
        public static AOSMessageSender Instance
        {
            get { return instance; }
        }
        
        private AOSMessageSender()
        {
            if (null == jc)
            {
                jc = new AndroidJavaClass(GAMEBASE_ANDROID_PLUGIN_CLASS);                
            }

            if (null == GamebaseUnitySDKSettings.GetComponent<NativeMessageReceiver>())
            {
                GamebaseUnitySDKSettings.AddComponent<NativeMessageReceiver>();
            }
        }

        public string GetSync(string jsonString)
        {
            GamebaseLog.Debug(string.Format("jsonString : {0}", jsonString), this, "GetSync");
            return jc.CallStatic<string>("getSync", jsonString);
        }

        public void GetAsync(string jsonString)
        {
            GamebaseLog.Debug(string.Format("jsonString : {0}", jsonString), this, "GetAsync");
            jc.CallStatic("getAsync", jsonString);
        }

        public void Initialize(string className)
        {
            GamebaseLog.Debug(string.Format("className : {0}", className), this, "Initialize");
            jc.CallStatic("initialize", className);
        }
    }
}
#endif