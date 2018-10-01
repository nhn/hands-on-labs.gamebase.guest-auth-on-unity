
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

using System;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{

    public class NativeGamebaseNetwork : IGamebaseNetwork
    {
        protected class GamebaseNetwork
        {            
            public const string NETWORK_API_GET_TYPE                             = "gamebase://getType";
            public const string NETWORK_API_GET_TYPE_NAME                        = "gamebase://getTypeName";
            public const string NETWORK_API_IS_CONNECTED                         = "gamebase://isConnected";
            public const string NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER       = "gamebase://addOnChangedStatusListener";
            public const string NETWORK_API_REMOVE_ON_CHANGED_STATUS_LISTENER    = "gamebase://removeOnChangedStatusListener";
        }

        protected INativeMessageSender  messageSender   = null;
        protected string                CLASS_NAME      = string.Empty;

        public NativeGamebaseNetwork()
        {
            Init();
        }

        virtual protected void Init()
        {
            messageSender.Initialize(CLASS_NAME);

            DelegateManager.AddDelegate(GamebaseNetwork.NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER, OnAddOnChangedStatusListener);
        }

        virtual public GamebaseNetworkType GetNetworkType()
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseNetwork.NETWORK_API_GET_TYPE));
            string type     = messageSender.GetSync(jsonData);

            if (true == string.IsNullOrEmpty(type))
            {
                return GamebaseNetworkType.TYPE_NOT;
            }
            else
            {
                return (GamebaseNetworkType)Convert.ToInt32(type);
            }
        }

        virtual public string GetNetworkTypeName()
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebaseNetwork.NETWORK_API_GET_TYPE_NAME));

            return messageSender.GetSync(jsonData);
        }

        virtual public bool IsConnected()
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseNetwork.NETWORK_API_IS_CONNECTED));
            string jsonString   = messageSender.GetSync(jsonData);

            if (true == string.IsNullOrEmpty(jsonString))
            {
                return false;
            }

            JsonData requestData = JsonMapper.ToObject(jsonString);

            if (null != requestData["isConnected"])
            {
                return (bool)requestData["isConnected"];
            }
            else
            {
                return false;
            }
        }

        virtual public void IsConnected(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "IsConnected", GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<bool>>(handle));
        }

        [Obsolete("AddOnChangedStatusListener is deprecated.")]
        virtual public bool AddOnChangedStatusListener(int handle)
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseNetwork.NETWORK_API_ADD_ON_CHANGED_STATUS_LISTENER, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
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

        [Obsolete("RemoveOnChangedStatusListener is deprecated.")]
        virtual public bool RemoveOnChangedStatusListener()
        {
            string jsonData     = JsonMapper.ToJson(new UnityMessage(GamebaseNetwork.NETWORK_API_REMOVE_ON_CHANGED_STATUS_LISTENER));
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

        virtual protected void OnAddOnChangedStatusListener(NativeMessage message)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<GamebaseNetworkType>>(message.handle);
            if (null == callback)
            {
                return;
            }

            callback((GamebaseNetworkType)Convert.ToInt32(message.jsonData));
        }
    }
}

#endif
