using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class UnityMessage
    {
        public string scheme;
        public int handle;
        public string jsonData;
        public string extraData;
        public string gameObjectName;
        public string requestMethodName;

        public UnityMessage(string scheme, int handle = -1, string jsonData = "", string extraData = "", string gameObjectName = "", string requestMethodName = "")
        {
            this.scheme = scheme;
            this.handle = handle;
            if (jsonData != null)
                this.jsonData = jsonData;
            if (extraData != null)
                this.extraData = extraData;
            this.requestMethodName = requestMethodName;
            this.gameObjectName = gameObjectName;
        }
    }
}