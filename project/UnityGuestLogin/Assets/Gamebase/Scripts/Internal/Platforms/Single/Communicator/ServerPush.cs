#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System.Collections.Generic;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Single.Communicator
{
    public class ServerPush
    {
        public class ServerPushMessage
        {
            public class ServerPushPopup
            {
                public class Message
                {
                    public string title;
                    public string message;
                }

                public string defaultLanguage;
                public Dictionary<string, Message> messages;
            }

            public CommonResponse.Header header;
            public string result;
            public ServerPushPopup popup;
        }

        private static readonly ServerPush instance = new ServerPush();

        public static ServerPush Instance
        {
            get { return instance; }
        }

        private HashSet<string> stampSet = new HashSet<string>();

        public ServerPush()
        {
           
        }

        public void OnServerPush(string response)
        {
            GamebaseLog.Debug(response, this, "OnServerPush");

            ServerPushMessage message = JsonMapper.ToObject<ServerPushMessage>(response);
            CommonResponse.Header.ServerPush serverPush = message.header.serverPush;

            if (true == serverPush.type.Equals(GamebaseServerPushType.APP_KICKOUT) && true == string.IsNullOrEmpty(GamebaseImplementation.Instance.GetUserID()))
            {
                return;
            }

            if (false == stampSet.Add(serverPush.stamp))
            {
                return;
            }            

            if (true == serverPush.disconnect)
            {
                WebSocket.Instance.Disconnect();
            }

            if (true == serverPush.stopHeartbeat)
            {
                Heartbeat.Instance.StopHeartbeat();
            }

            GamebaseSystemPopup.Instance.ShowServerPushPopup(message.popup);            
            
            SendServerPushMessage(serverPush.type, message.result);
        }
        
        private void SendServerPushMessage(string type, string data)
        {
            GamebaseLog.Debug(string.Format("type : {0}, data : {1}", type, data), this, "SendServerPushMessage");
            GamebaseResponse.SDK.ServerPushMessage serverPushMessage = new GamebaseResponse.SDK.ServerPushMessage();
            serverPushMessage.type = type;
            serverPushMessage.data = data;

            var pushCallback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage>>(GamebaseServerPushEventManager.Instance.Handle);
            if (null != pushCallback)
            {
                pushCallback(serverPushMessage);
            }
        }
    }   
}
#endif