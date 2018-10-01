using System.Collections.Generic;

namespace Toast.Gamebase
{
    public interface IGamebaseAuth
    {
        void Login(string providerName, int handle);
        void Login(string providerName, Dictionary<string, object> additionalInfo, int handle);
        void Login(Dictionary<string, object> credentialInfo, int handle);
        void LoginForLastLoggedInProvider(int handle);
        void AddMapping(string providerName, int handle);
        void AddMapping(string providerName, Dictionary<string, object> additionalInfo, int handle);
        void AddMapping(Dictionary<string, object> credentialInfo, int handle);
        void RemoveMapping(string providerName, int handle);
        void Logout(int handle);
        void Withdraw(int handle);
        void IssueTransferKey(long expiresIn, int handle);
        void RequestTransfer(string transferKey, int handle);

        List<string> GetAuthMappingList();
        string GetAuthProviderUserID(string providerName);
        string GetAuthProviderAccessToken(string providerName);
        GamebaseResponse.Auth.AuthProviderProfile GetAuthProviderProfile(string providerName);
        GamebaseResponse.Auth.BanInfo GetBanInfo();
    }
}