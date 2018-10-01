#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

using System;

namespace Toast.Gamebase.Single
{
    public class CommonGamebaseNetwork : IGamebaseNetwork
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                    return typeof(CommonGamebaseNetwork).Name;

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public GamebaseNetworkType GetNetworkType()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "FireNotSupportedAPI");
            return GamebaseNetworkType.TYPE_NOT;
        }

        public string GetNetworkTypeName()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "GetTypeName");
            return "";
        }
        
        public virtual bool IsConnected()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "IsConnected");
            return true;
        }

        public virtual void IsConnected(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "IsConnected", GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<bool>>(handle));
        }

        [Obsolete("AddOnChangedStatusListener is deprecated.")]
        public bool AddOnChangedStatusListener(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "AddOnChangedStatusListener");
            return false;
        }

        [Obsolete("RemoveOnChangedStatusListener is deprecated.")]
        public bool RemoveOnChangedStatusListener()
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "RemoveOnChangedStatusListener");
            return false;
        }
    }
}
#endif