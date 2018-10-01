#if UNITY_EDITOR || UNITY_IOS
using System.Runtime.InteropServices;
using System;
namespace Toast.Gamebase.Mobile.IOS
{
	public sealed class IOSMessageSender : INativeMessageSender
	{
        [DllImport("__Internal")]
		private static extern void initialize(string className);
		[DllImport("__Internal")]
		private static extern IntPtr getSync (string jsonString);
		[DllImport("__Internal")]
		private static extern void getAsync (string jsonString);

		private static readonly IOSMessageSender instance = new IOSMessageSender();

		public static IOSMessageSender Instance
		{
			get { return instance; }
		}

		private IOSMessageSender()
		{
            if(GamebaseUnitySDKSettings.GetComponent<NativeMessageReceiver>() == null )
                GamebaseUnitySDKSettings.AddComponent<NativeMessageReceiver>();
        }

        public string GetSync(string jsonString)
		{
			GamebaseLog.Debug (string.Format("jsonString : {0}" , jsonString), this, "GetSync");
            string retValue = string.Empty;
			IntPtr result = getSync(jsonString);
			if (result != IntPtr.Zero)
				retValue = Marshal.PtrToStringAnsi(result);
			return retValue;
		}

		public void GetAsync(string jsonString)
		{
            GamebaseLog.Debug(string.Format("jsonString : {0}", jsonString), this, "GetAsync");
            getAsync(jsonString);
		}
        
        public void Initialize(string className)
        {
            GamebaseLog.Debug(string.Format("className : {0}", className), this, "Initialize");
            initialize(className);
        }
    }
}
#endif