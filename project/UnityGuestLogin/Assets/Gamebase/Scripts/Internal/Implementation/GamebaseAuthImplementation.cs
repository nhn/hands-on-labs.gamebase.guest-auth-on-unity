using System.Collections.Generic;
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
    public sealed class GamebaseAuthImplementation
    {
        private static readonly GamebaseAuthImplementation instance = new GamebaseAuthImplementation();
        
        public static GamebaseAuthImplementation Instance
        {
            get { return instance; }
        }

        IGamebaseAuth auth;

        private GamebaseAuthImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            auth = new AOSGamebaseAuth();
#elif !UNITY_EDITOR && UNITY_IOS
            auth = new IOSGamebaseAuth();
#elif !UNITY_EDITOR && UNITY_WEBGL
            auth = new WebGLGamebaseAuth();
#else
            auth = new StandaloneGamebaseAuth();
#endif
        }

        public void Login(string providerName, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.Login(providerName, handle);
        }

        public void Login(Dictionary<string, object> credentialInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.Login(credentialInfo, handle);
        }

        public void Login(string providerName, Dictionary<string, object> additionalInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.Login(providerName, additionalInfo, handle);
        }

        public void LoginForLastLoggedInProvider(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.LoginForLastLoggedInProvider(handle);
        }

        public void AddMapping(string providerName, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.AddMapping(providerName, handle);
        }

        public void AddMapping(string providerName, Dictionary<string, object> additionalInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.AddMapping(providerName, additionalInfo, handle);
        }

        public void AddMapping(Dictionary<string, object> credentialInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.AddMapping(credentialInfo, handle);
        }

        public void RemoveMapping(string providerName, GamebaseCallback.ErrorDelegate callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.RemoveMapping(providerName, handle);
        }

        public void Logout(GamebaseCallback.ErrorDelegate callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.Logout(handle);
        }

        public void Withdraw(GamebaseCallback.ErrorDelegate callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.Withdraw(handle);
        }

        public void IssueTransferKey(long expiresIn, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.TransferKeyInfo> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.IssueTransferKey(expiresIn, handle);
        }

        public void RequestTransfer(string transferKey, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            auth.RequestTransfer(transferKey, handle);
        }

        public List<string> GetAuthMappingList()
        {
            return auth.GetAuthMappingList();
        }

        public string GetAuthProviderUserID(string providerName)
        {
            return auth.GetAuthProviderUserID(providerName);
        }

        public string GetAuthProviderAccessToken(string providerName)
        {
            return auth.GetAuthProviderAccessToken(providerName);
        }

        public GamebaseResponse.Auth.AuthProviderProfile GetAuthProviderProfile(string providerName)
        {
            return auth.GetAuthProviderProfile(providerName);
        }

        public GamebaseResponse.Auth.BanInfo GetBanInfo()
        {
            return auth.GetBanInfo();
        }
    }
}