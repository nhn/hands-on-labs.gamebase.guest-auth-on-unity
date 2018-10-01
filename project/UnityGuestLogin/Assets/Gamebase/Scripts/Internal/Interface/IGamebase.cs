﻿namespace Toast.Gamebase
{
    internal interface IGamebase
    {
        void SetDebugMode(bool isDebugMode);
        void Initialize(GamebaseRequest.GamebaseConfiguration configuration, int handle);

        string GetSDKVersion();
        string GetUserID();
        string GetAccessToken();
        string GetLastLoggedInProvider();
        string GetLanguageCode();
        string GetCarrierCode();
        string GetCarrierName();
        string GetCountryCode();
        string GetCountryCodeOfUSIM();
        string GetCountryCodeOfDevice();
        bool IsSandbox();
        void SetDisplayLanguageCode(string languageCode);
        string GetDisplayLanguageCode();

        void AddObserver(int handle);
        void RemoveObserver();
        void RemoveAllObserver();
        void AddServerPushEvent(int handle);
        void RemoveServerPushEvent();
        void RemoveAllServerPushEvent();
    }
}
