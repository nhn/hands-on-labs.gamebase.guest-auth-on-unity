﻿#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

namespace Toast.Gamebase.Single
{
    public class CommonGamebasePush : IGamebasePush
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                    return typeof(CommonGamebasePush).Name;

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public void RegisterPush(GamebaseRequest.Push.PushConfiguration pushConfiguration, int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RegisterPush",
                GamebaseCallbackHandler.GetCallback<GamebaseCallback.ErrorDelegate>(handle));
        }
        
        public void QueryPush(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "QueryPush",
                GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Push.PushConfiguration>>(handle));
        }

        public void SetSandboxMode(bool isSandbox)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "SetSandboxMode");
        }
    }
}
#endif