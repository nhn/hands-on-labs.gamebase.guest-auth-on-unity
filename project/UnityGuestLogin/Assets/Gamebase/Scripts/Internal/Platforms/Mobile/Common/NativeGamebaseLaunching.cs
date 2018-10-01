#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

using System;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class NativeGamebaseLaunching : IGamebaseLaunching
    {
        protected class GamebaseLaunching
        {
            public const string LAUNCHING_API_ADD_UPDATE_STATUSLISTENER      = "gamebase://addOnUpdateStatusListener";
            public const string LAUNCHING_API_REMOVE_UPDATE_STATUSLISTENER   = "gamebase://removeOnUpdateStatusListener";
            public const string LAUNCHING_API_GET_LAUNCHING_INFORMATIONS     = "gamebase://getLaunchingInformations";
            public const string LAUNCHING_API_GET_LAUNCHING_STATUS           = "gamebase://getLaunchingStatus";
        }

        protected INativeMessageSender  messageSender   = null;
        protected string                CLASS_NAME      = string.Empty;

        public NativeGamebaseLaunching()
        {
            Init();
        }

        virtual protected void Init()
        {
            messageSender.Initialize(CLASS_NAME);

            DelegateManager.AddDelegate(GamebaseLaunching.LAUNCHING_API_ADD_UPDATE_STATUSLISTENER, DelegateManager.SendDataDelegate<GamebaseResponse.Launching.LaunchingStatus>);
        }

        [Obsolete("AddUpdateStatusListener is deprecated.")]
        virtual public bool AddUpdateStatusListener(int handle)
		{
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseLaunching.LAUNCHING_API_ADD_UPDATE_STATUSLISTENER, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            string jsonString   = messageSender.GetSync(jsonData);

            if (true == string.IsNullOrEmpty(jsonString))
            {
                return false;
            }

            JsonData requestData = JsonMapper.ToObject(jsonString);

            if (null != requestData["isSuccess"])
            {
                return (bool)requestData["isSuccess"];
            }
            else
            {
                return false;
            }
        }

        [Obsolete("RemoveUpdateStatusListener is deprecated.")]
        virtual public bool RemoveUpdateStatusListener(int handle)
		{
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseLaunching.LAUNCHING_API_REMOVE_UPDATE_STATUSLISTENER, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            string jsonString   = messageSender.GetSync(jsonData);

            if (string.IsNullOrEmpty(jsonString))
                return false;

            JsonData requestData = JsonMapper.ToObject(jsonString);

            if (null != requestData["isSuccess"])
            {
                return (bool)requestData["isSuccess"];
            }
            else
            {
                return false;
            }
        }

		virtual public GamebaseResponse.Launching.LaunchingInfo GetLaunchingInformations()
		{
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseLaunching.LAUNCHING_API_GET_LAUNCHING_INFORMATIONS));
            string jsonString   = messageSender.GetSync(jsonData);

			return JsonMapper.ToObject<GamebaseResponse.Launching.LaunchingInfo>(jsonString);
		}

		virtual public int GetLaunchingStatus()
		{
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseLaunching.LAUNCHING_API_GET_LAUNCHING_STATUS));

            return Convert.ToInt32(messageSender.GetSync(jsonData));
		}
    }
}

#endif
