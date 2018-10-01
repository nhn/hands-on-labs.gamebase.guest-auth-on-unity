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
    public sealed class GamebaseLaunchingImplementation
    {
        private GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus> launchingStatusDelegate = null;
        private int launchingStatusHandle;

        private static readonly GamebaseLaunchingImplementation instance = new GamebaseLaunchingImplementation();
        
        public static GamebaseLaunchingImplementation Instance
        {
            get { return instance; }
        }

        IGamebaseLaunching launching;

        private GamebaseLaunchingImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            launching = new AOSGamebaseLaunching();
#elif !UNITY_EDITOR && UNITY_IOS
            launching = new IOSGamebaseLaunching();
#elif !UNITY_EDITOR && UNITY_WEBGL
            launching = new WebGLGamebaseLaunching();
#else
            launching = new StandaloneGamebaseLaunching();
#endif
        }
        
        [Obsolete("AddUpdateStatusListener is deprecated.")]
        public bool AddUpdateStatusListener(GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus> statusDelegate)
        {
            if (launchingStatusDelegate == null)
            {
                launchingStatusHandle = GamebaseCallbackHandler.RegisterCallback(new GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus>(OnLaunchingStatus));
                
                if (!launching.AddUpdateStatusListener(launchingStatusHandle))
                {
                    GamebaseCallbackHandler.UnregisterCallback(launchingStatusHandle);
                    return false;
                }
            }
            launchingStatusDelegate += statusDelegate;
            return true;
        }

        [Obsolete("RemoveUpdateStatusListener is deprecated.")]
        public bool RemoveUpdateStatusListener(GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus> statusDelegate)
        {
            launchingStatusDelegate -= statusDelegate;
            if (launchingStatusDelegate == null)
            {
                launching.RemoveUpdateStatusListener(launchingStatusHandle);
                GamebaseCallbackHandler.UnregisterCallback(launchingStatusHandle);
                launchingStatusHandle = 0;
            }
            return true;
        }

        public GamebaseResponse.Launching.LaunchingInfo GetLaunchingInformations()
        {
            return launching.GetLaunchingInformations();
        }

        public int GetLaunchingStatus()
        {
            return launching.GetLaunchingStatus();
        }

        public void OnLaunchingStatus(GamebaseResponse.Launching.LaunchingStatus launchingStatus)
        {
            launchingStatusDelegate(launchingStatus);
        }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        public void RequestLaunchingInfo(int handle)
        {
            ((Single.CommonGamebaseLaunching)launching).GetLaunchingInfo(handle);
        }

        public void RequestLaunchingStatus(int handle)
        {
            ((Single.CommonGamebaseLaunching)launching).RequestLaunchingStatus(handle);
        }

        public float GetLaunchingStatusExpire()
        {
            return ((Single.CommonGamebaseLaunching)launching).GetLaunchingStatusExpire();
        }

        public void ResetLaunchingStatusExpire()
        {
            ((Single.CommonGamebaseLaunching)launching).ResetLaunchingStatusExpire();
        }
#endif
    }
}