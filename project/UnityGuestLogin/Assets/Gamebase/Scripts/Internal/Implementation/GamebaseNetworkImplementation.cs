#if !UNITY_EDITOR && UNITY_ANDROID
using Toast.Gamebase.Mobile.Android;
#elif !UNITY_EDITOR && UNITY_IOS
using Toast.Gamebase.Mobile.IOS;
#elif !UNITY_EDITOR && UNITY_WEBGL
using Toast.Gamebase.Single.WebGL;
#else
using Toast.Gamebase.Single.Standalone;
#endif

using System;

namespace Toast.Gamebase
{
    public sealed class GamebaseNetworkImplementation
    {
        private GamebaseCallback.DataDelegate<GamebaseNetworkType> networkStateDelegate = null;
        private int networkStateHandle;

        private static readonly GamebaseNetworkImplementation instance = new GamebaseNetworkImplementation();
        
        public static GamebaseNetworkImplementation Instance
        {
            get { return instance; }
        }

        IGamebaseNetwork network;

        private GamebaseNetworkImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            network = new AOSGamebaseNetwork();
#elif !UNITY_EDITOR && UNITY_IOS
            network = new IOSGamebaseNetwork();
#elif !UNITY_EDITOR && UNITY_WEBGL
            network = new WebGLGamebaseNetwork();
#else
            network = new StandaloneGamebaseNetwork();
#endif
        }

        public GamebaseNetworkType GetNetworkType()
        {
            return network.GetNetworkType();
        }

        public string GetNetworkTypeName()
        {
            return network.GetNetworkTypeName();
        }

        public bool IsConnected()
        {
            return network.IsConnected();
        }
        
        public void IsConnected(GamebaseCallback.DataDelegate<bool> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            network.IsConnected(handle);
        }

        [Obsolete("AddOnChangedStatusListener is deprecated.")]
        public bool AddOnChangedStatusListener(GamebaseCallback.DataDelegate<GamebaseNetworkType> stateDelegate)
        {
            if (networkStateDelegate == null)
            {
                networkStateHandle = GamebaseCallbackHandler.RegisterCallback(new GamebaseCallback.DataDelegate<GamebaseNetworkType>(OnChangedStatus));
				if (!network.AddOnChangedStatusListener (networkStateHandle)) 
				{
                    GamebaseCallbackHandler.UnregisterCallback(networkStateHandle);
					return false;
				}
            }
            networkStateDelegate += stateDelegate;
            return true;
        }

        [Obsolete("RemoveOnChangedStatusListener is deprecated.")]
        public bool RemoveOnChangedStatusListener(GamebaseCallback.DataDelegate<GamebaseNetworkType> stateDelegate)
        {
            networkStateDelegate -= stateDelegate;
            if (networkStateDelegate == null)
            {
                network.RemoveOnChangedStatusListener();
                GamebaseCallbackHandler.UnregisterCallback(networkStateHandle);
                networkStateHandle = 0;
            }
            return true;
        }

        public void OnChangedStatus(GamebaseNetworkType type)
        {
            networkStateDelegate(type);
        }
    }
}