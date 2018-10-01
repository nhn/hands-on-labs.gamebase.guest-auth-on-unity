#if !UNITY_EDITOR && UNITY_ANDROID
using Toast.Gamebase.Mobile.Android;
#elif !UNITY_EDITOR && UNITY_IOS
using Toast.Gamebase.Mobile.IOS;
#elif !UNITY_EDITOR && UNITY_WEBGL
using Toast.Gamebase.Single.WebGL;
#else
using Toast.Gamebase.Single.Standalone;
#endif

namespace Toast.Gamebase
{
    public sealed class GamebasePushImplementation
    {
        private static readonly GamebasePushImplementation instance = new GamebasePushImplementation();
        
        public static GamebasePushImplementation Instance
        {
            get { return instance; }
        }

        IGamebasePush push;

        private GamebasePushImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            push = new AOSGamebasePush();
#elif !UNITY_EDITOR && UNITY_IOS
            push = new IOSGamebasePush();
#elif !UNITY_EDITOR && UNITY_WEBGL
            push = new WebGLGamebasePush();
#else
            push = new StandaloneGamebasePush();
#endif
        }

        public void RegisterPush(GamebaseRequest.Push.PushConfiguration pushConfiguration, GamebaseCallback.ErrorDelegate callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            push.RegisterPush(pushConfiguration, handle);
        }
        
        public void QueryPush(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Push.PushConfiguration> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            push.QueryPush(handle);
        }

        public void SetSandboxMode(bool isSandbox)
        {
            push.SetSandboxMode(isSandbox);
        }
    }
}