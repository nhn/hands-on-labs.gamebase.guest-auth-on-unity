using System;
using System.Collections.Generic;

namespace Toast.Gamebase
{
    /// <summary>
    /// The Gamebase class is core of Gamebase service.
    /// </summary>
    public sealed class Gamebase
    {
        /// <summary>
        /// Gamebase shows warning and error logs only.
        /// To turn on system logs for the reference of development, call Gamebase.setDebugMode(true).
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="isDebugMode">Debug features (like logging) are enabled if true, disabled if false.</param>
        /// <example>
        /// Example Usage : 
        /// <code>
        /// public void SetDebugMode(bool isDebugMode)
        /// {
        ///     Gamebase.SetDebugMode(isDebugMode);
        /// }
        /// </code>
        /// </example>
        public static void SetDebugMode(bool isDebugMode)
        {
            GamebaseImplementation.Instance.SetDebugMode(isDebugMode);
        }

        /// <summary>
        /// Return true if the GamebaseException object is null or the error code is zero. 
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="error">The exception that occurred.</param>
        /// <returns>True if success, false if not.</returns>
        public static bool IsSuccess(GamebaseError error)
        {
            return (error == null || error.code == GamebaseErrorCode.SUCCESS);
        }

        /// <summary>
        /// This function initialize the Gamebase SDK with inspector settings.
        /// If this function is not called, the Gamebase SDK function will not work.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="callback">The callback provided will return a LaunchingInfo object that contains the infomations of Gamebase.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Initialize()
        /// {
        ///     Gamebase.Initialize((launchingInfo, error) =>
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("Gamebase initialization succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Gamebase initialization failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Initialize(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> callback)
        {
            GamebaseImplementation.Instance.Initialize(callback);
        }

        /// <summary>
        /// This function initialize the Gamebase SDK with Gamebase configuration. 
        /// If this function is not called, the Gamebase SDK function will not work.
        /// @since Added 1.6.0
        /// </summary>
        /// <param name="configuration">Settings for the Gamebase SDK.</param>
        /// <param name="callback">The callback provided will return a LaunchingInfo object that contains the infomations of Gamebase.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Initialize(string appID, string appVersion, string displayLanguage, bool enablePopup, bool enableLaunchingStatusPopup, bool enableBanPopup, string storeCode, string fcmSenderId)
        /// {
        ///     var configuration = new GamebaseRequest.GamebaseConfiguration();
        ///     configuration.appID = appID;
        ///     configuration.appVersion = appVersion;
        ///     configuration.zoneType = "real";
        ///     configuration.displayLanguage = displayLanguage;
        ///     configuration.enablePopup = enablePopup;
        ///     configuration.enableLaunchingStatusPopup = enableLaunchingStatusPopup;
        ///     configuration.enableBanPopup = enableBanPopup;
        ///     configuration.storeCode = storeCode;
        ///     configuration.fcmSenderId = fcmSenderId;
        ///     
        ///     Gamebase.Initialize(configuration, (launchingInfo, error) =>
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("Gamebase initialization succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Gamebase initialization failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Initialize(GamebaseRequest.GamebaseConfiguration configuration, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> callback)
        {
            GamebaseImplementation.Instance.Initialize(configuration, callback);
        }

        /// <summary>
        /// Add a observer to be called when network status, launching status or user status is changed.
        /// @since Added 1.8.0
        /// </summary>
        /// <param name="observer">The callback that will run.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void AddObserver()
        /// {
        ///     GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer = (data) =>
        ///     {
        ///         GamebaseResponse.SDK.ObserverMessage observerMessage = data;
        ///     };
        ///     
        ///     Gamebase.AddObserver(observer);
        /// }
        /// </code>
        /// </example>
        public static void AddObserver(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer)
        {
            GamebaseImplementation.Instance.AddObserver(observer);
        }

        /// <summary>
        /// Remove a observer listener.
        /// @since Added 1.8.0
        /// </summary>
        /// <param name="observer">The callback that will run.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RemoveObserver(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer)
        /// {
        ///     Gamebase.RemoveObserver(observer);
        /// }
        /// </code>
        /// </example>
        public static void RemoveObserver(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage> observer)
        {
            GamebaseImplementation.Instance.RemoveObserver(observer);
        }

        /// <summary>
        /// Remove all observer listener.
        /// @since Added 1.8.0
        /// </summary>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RemoveAllObserver()
        /// {
        ///     Gamebase.RemoveAllObserver();
        /// }
        /// </code>
        /// </example>
        public static void RemoveAllObserver()
        {
            GamebaseImplementation.Instance.RemoveAllObserver();
        }

        /// <summary>
        /// Add a server push event listener to be called when server push message is arrived.
        /// @since Added 1.8.0
        /// </summary>
        /// <param name="serverPushEvent">The callback that will run.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void AddServerPushEvent()
        /// {
        ///     GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent = (data) =>
        ///     {
        ///         GamebaseResponse.SDK.ServerPushMessage serverPushMessage = data;
        ///     };
        ///     
        ///     Gamebase.AddServerPushEvent(serverPushEvent);
        /// }
        /// </code>
        /// </example>
        public static void AddServerPushEvent(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent)
        {
            GamebaseImplementation.Instance.AddServerPushEvent(serverPushEvent);
        }

        /// <summary>
        /// Remove a server push event listener.
        /// @since Added 1.8.0
        /// </summary>
        /// <param name="serverPushEvent">The callback that will run.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RemoveServerPushEvent(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent)
        /// {
        ///     Gamebase.RemoveServerPushEvent(serverPushEvent);
        /// }        
        /// </code>
        /// </example>
        public static void RemoveServerPushEvent(GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ServerPushMessage> serverPushEvent)
        {
            GamebaseImplementation.Instance.RemoveServerPushEvent(serverPushEvent);
        }

        /// <summary>
        /// Remove all server push event listeners.
        /// @since Added 1.8.0
        /// </summary>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RemoveAllServerPushEvent()
        /// {
        ///     Gamebase.RemoveAllServerPushEvent();
        /// }  
        /// </code>
        /// </example>
        public static void RemoveAllServerPushEvent()
        {
            GamebaseImplementation.Instance.RemoveAllServerPushEvent();
        }

        /// <summary>
        /// Try to authenticate by specifying an IdP type.
        /// Types that can be authenticated are declared in the GamebaseAuthProvider class.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The provider name witch is authentication provider. </param>
        /// <param name="callback">Login result callback, returns the authentication token as a result of login.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Login(string providerName)
        /// {
        ///     Gamebase.Login(providerName, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             string userId = authToken.member.userId;
        ///             Debug.Log(string.Format("Login succeeded. Gamebase userId is {0}", userId));
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Login failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Login(string providerName, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.Login(providerName, callback);
        }

        /// <summary>
        /// There is information which must be included for login with some IdPs.
        /// In order to set such necessary information, this API is provided.
        /// You can enter those information to additionalInfo in the dictionary type. 
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <param name="additionalInfo">The additionalInfo which is additional information using for login.</param>
        /// <param name="callback">Login result callback, returns the authentication token as a result of login.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Login(string providerName, Dictionary<string, object> additionalInfo)
        /// {
        ///     Gamebase.Login(providerName, additionalInfo, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             string userId = authToken.member.userId;
        ///             Debug.Log(string.Format("Login succeeded. Gamebase userId is {0}", userId));
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Login failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Login(string providerName, Dictionary<string, object> additionalInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.Login(providerName, additionalInfo, callback);
        }

        /// <summary>
        /// This game interface allows authentication to be made with SDK provided by IdP, before login to Gamebase with provided access token.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <param name="credentialInfo">The credentialInfo which is credential of authentication provider.</param>
        /// <param name="callback">Login result callback, returns the authentication token as a result of login.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Login(Dictionary<string, object> credentialInfo)
        /// {
        ///     Gamebase.Login(credentialInfo, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             string userId = authToken.member.userId;
        ///             Debug.Log(string.Format("Login succeeded. Gamebase userId is {0}", userId));
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Login failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Login(Dictionary<string, object> credentialInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.Login(credentialInfo, callback);
        }

        /// <summary>
        /// Try login with the most recently logged-in IdP.
        /// If a token is expired or its authentication fails, return failure.
        /// Note that a login for the IdP should be implemented.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="callback">Login result callback, returns the authentication token as a result of login.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void LoginForLastLoggedInProvider()
        /// {
        ///     Gamebase.LoginForLastLoggedInProvider((authToken, error) => 
        ///     { 
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("LoginForLastLoggedInProvider is succeeded.");
        ///         }
        ///         else
        ///         {
        ///             if (error.code == GamebaseErrorCode.SOCKET_ERROR || error.code == GamebaseErrorCode.SOCKET_RESPONSE_TIMEOUT)
        ///             {
        ///                 Debug.Log(string.Format("Retry LoginForLastLoggedInProvider or notify an error message to the user. : {0}", error.message));
        ///             }
        ///             else
        ///             {
        ///                 Debug.Log("Try to login using a specifec IdP");
        ///             }
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void LoginForLastLoggedInProvider(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.LoginForLastLoggedInProvider(callback);
        }

        /// <summary>
        /// Try to log out from logged-in IdP. In many cases, the logout button is located on the game configuration screen. 
        /// Even if a logout is successful, a game user's data remain. 
        /// When it is successful, as authentication records with a corresponding IdP are removed, ID and passwords will be required for the next log-in process.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="callback">Callbacks the result of logout.(GamebaseError error)</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Logout()
        /// {
        ///     Gamebase.Logout((error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("Logout succeeded.");
        ///         }
        ///         else
        ///         {
        ///         	Debug.Log(string.Format("Logout failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Logout(GamebaseCallback.ErrorDelegate callback)
        {
            GamebaseAuthImplementation.Instance.Logout(callback);
        }

        /// <summary>
        /// Below shows an example of how a game user withdraws while logged-in.
        /// When a user is successfully withdrawn, the user's data interfaced with a login IdP will be deleted.
        /// The user can log in with the IdP again, and a new user's data will be created.
        /// It means user's withdrawal from Gamebase, not from IdP account.
        /// After a successful withdrawal, a log-out from IdP will be tried.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="callback">Callbacks the result of withdraw. (GamebaseError error)</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void Withdraw()
        /// {
        ///     Gamebase.Withdraw((error) =>
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("Withdraw succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("Withdraw failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void Withdraw(GamebaseCallback.ErrorDelegate callback)
        {
            GamebaseAuthImplementation.Instance.Withdraw(callback);
        }

        /// <summary>
        /// Try mapping to another IdP while logged-in to a specific IdP. 
        /// If an IdP account to map has already been integrated to another account, return the AUTH_ADD_MAPPING_ALREADY_MAPPED_TO_OTHER_MEMBER (3302) error.
        /// Even if a mapping is successful, 'currently logged-in IdP' does not change.
        /// Mapping simply adds IdP integration.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <param name="callback">Mapping result callback, returns the authentication token as a result of mapping.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void AddMapping(string providerName)
        /// {
        ///     Gamebase.AddMapping(providerName, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("AddMapping succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("AddMapping failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void AddMapping(string providerName, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.AddMapping(providerName, callback);
        }

        /// <summary>
        /// There is information which must be included for mapping with some IdPs.
        /// In order to set such necessary information, this API is provided.
        /// You can enter those information to additionalInfo in the dictionary type.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <param name="additionalInfo">The additionalInfo which is additional information using for mapping. </param>
        /// <param name="callback">Callbacks the results of mappings, returns the authentication token as a result of mappings.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void AddMapping(string providerName, Dictionary<string, object> additionalInfo)
        /// {
        ///     Gamebase.AddMapping(providerName, additionalInfo, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("AddMapping succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("AddMapping failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void AddMapping(string providerName, Dictionary<string, object> additionalInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.AddMapping(providerName, additionalInfo, callback);
        }

        /// <summary>
        /// This game interface allows authentication to be made with SDK provided by IdP, before applying Gamebase AddMapping with provided access token.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="credentialInfo">The credentialInfo which is credential of authentication provider.</param>                
        /// <param name="callback">Mapping result callback, returns the authentication token as a result of mapping.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void AddMapping(Dictionary<string, object> credentialInfo)
        /// {
        ///     Gamebase.AddMapping(credentialInfo, (authToken, error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("AddMapping succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("AddMapping failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void AddMapping(Dictionary<string, object> credentialInfo, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.AddMapping(credentialInfo, callback);
        }

        /// <summary>
        /// 
        /// Remove mapping with a specific IdP. 
        /// If IdP mapping is not removed, error will occur.  
        /// After mapping is removed, Gamebase processes logout of the IdP.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <param name="callback">Callbacks the result of removing the mapping.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RemoveMapping(string providerName)
        /// {
        ///     Gamebase.RemoveMapping(providerName, (error) => 
        ///     {
        ///         if (Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log("RemoveMapping succeeded.");
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("RemoveMapping failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void RemoveMapping(string providerName, GamebaseCallback.ErrorDelegate callback)
        {
            GamebaseAuthImplementation.Instance.RemoveMapping(providerName, callback);
        }

        /// <summary>
        /// If you want to transfer an account that is guest to other device. You can use this method to publish an key that called TransferKey.
        /// To publish TransferKey, an account must be logged in by Guest.
        /// After transfering guest account to other device, guest account on this device will be logged out and will not be able to authenticate with same guest account.
        /// @since Added 1.9.0.
        /// </summary>
        /// <param name="expiresIn">TransferKey is valid for expiresIn value in seconds.</param>
        /// <param name="callback">Handler to receive transferKey, regDate, expireDate and errors.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void IssueTransferKey(long expiresIn)
        /// {
        ///     Gamebase.IssueTransferKey(expiresIn, (transferKeyInfo, error) => 
        ///     {
        ///         if (true == Gamebase.IsSuccess(error))
        ///         {
        ///             Debug.Log(string.Format("transferKey:{0}", transferKeyInfo.transferKey));
        ///             Debug.Log(string.Format("regDate:{0}", transferKeyInfo.regDate));
        ///             Debug.Log(string.Format("expireDate:{0}", transferKeyInfo.expireDate));
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("IssueTransferKey failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void IssueTransferKey(long expiresIn, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.TransferKeyInfo> callback)
        {
            GamebaseAuthImplementation.Instance.IssueTransferKey(expiresIn, callback);
        }

        /// <summary>
        /// This method transfer the guest account to other device.
        /// If result is success, the guest account can be logged in to new device and logged out from old device.
        /// @since Added 1.9.0.
        /// </summary>
        /// <param name="transferKey">TransferKey received from old device.</param>
        /// <param name="callback">completion Handler that include guest login informations and errors.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void RequestTransfer(string transferKey)
        /// {
        ///     Gamebase.RequestTransfer(transferKey, (authToken, error) =>
        ///     {   
        ///         if (true == Gamebase.IsSuccess(error))
        ///         {
        ///             string userId = authToken.member.userId;
        ///             Debug.Log(string.Format("RequestTransfer succeeded. Gamebase userId is {0}", userId));
        ///         }
        ///         else
        ///         {
        ///             Debug.Log(string.Format("RequestTransfer failed. error is {0}", error));
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        public static void RequestTransfer(string transferKey, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Auth.AuthToken> callback)
        {
            GamebaseAuthImplementation.Instance.RequestTransfer(transferKey, callback);
        }

        /// <summary>
        /// Return the list of IdPs mapped to user IDs.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The list of external authentication providers mapped to the current user identifier.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetAuthMappingList()
        /// {
        ///     List<string> mappingList = Gamebase.GetAuthMappingList();
        /// }
        /// </code>
        /// </example>
        public static List<string> GetAuthMappingList()
        {
            return GamebaseAuthImplementation.Instance.GetAuthMappingList();
        }

        /// <summary>
        /// Get User ID from externally authenticated SDK.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <returns>The user ID from the authentication provider.(</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetAuthProviderUserID(string providerName)
        /// {
        ///     string authProviderUserID = Gamebase.GetAuthProviderUserID(providerName);
        /// }
        /// </code>
        /// </example>
        public static string GetAuthProviderUserID(string providerName)
        {
            return GamebaseAuthImplementation.Instance.GetAuthProviderUserID(providerName);
        }

        /// <summary>
        /// Get Access Token from externally authentication SDK.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <returns>The access token from the authentication provider.(</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetAuthProviderAccessToken(string providerName)
        /// {
        ///     string authProviderAccessToken = Gamebase.GetAuthProviderAccessToken(providerName);
        /// }
        /// </code>
        /// </example>
        public static string GetAuthProviderAccessToken(string providerName)
        {
            return GamebaseAuthImplementation.Instance.GetAuthProviderAccessToken(providerName);
        }

        /// <summary>
        /// Get Profile from externally authenticated SDK.
        /// @since Added 1.4.0.
        /// </summary>
        /// <param name="providerName">The providerName which is authentication provider.</param>
        /// <returns>The profile from the authentication provider.(</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetAuthProviderProfile(string providerName)
        /// {
        ///     GamebaseRequest.AuthProviderProfile profile = Gamebase.GetAuthProviderProfile(providerName);
        /// }
        /// </code>
        /// </example>
        public static GamebaseResponse.Auth.AuthProviderProfile GetAuthProviderProfile(string providerName)
        {
            return GamebaseAuthImplementation.Instance.GetAuthProviderProfile(providerName);
        }

        /// <summary>
        /// For a banned user registered at Gamebase Console,restricted use of information code (AUTH_BANNED_MEMBER(3005)) can be displayed as below, when trying login. The ban information can be found by using the API as below.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The ban information of the suspended user.(</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetBanInfo()
        /// {
        ///     GamebaseResponse.Auth.BanInfo banInfo = Gamebase.GetBanInfo();
        /// }
        /// </code>
        public static GamebaseResponse.Auth.BanInfo GetBanInfo()
        {
            return GamebaseAuthImplementation.Instance.GetBanInfo();
        }

        /// <summary>
        /// Get the current version of the Gamebase SDK for Unity as a string.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The current version of the Gamebase SDK.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetSDKVersion()
        /// {
        ///     string sdkVersion = Gamebase.GetSDKVersion();
        /// }
        /// </code>
        /// </example>
        public static string GetSDKVersion()
        {
            return GamebaseImplementation.Instance.GetSDKVersion();
        }

        /// <summary>
        /// Get User ID issued by Gamebase.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The user id that is currently logged in.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetUserID()
        /// {
        ///     string userID = Gamebase.GetUserID();
        /// }
        /// </code>
        /// </example>
        public static string GetUserID()
        {
            return GamebaseImplementation.Instance.GetUserID();
        }

        /// <summary>
        /// Get AccessToken issued by Gamebase.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The access token for Gamebase platform.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetAccessToken()
        /// {
        ///     string accessToken = Gamebase.GetAccessToken();
        /// }
        /// </code>
        /// </example>
        public static string GetAccessToken()
        {
            return GamebaseImplementation.Instance.GetAccessToken();
        }

        /// <summary>
        /// Get the last logged-in Provider Name in Gamebase.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The name that is last logged-in authentication provider.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetLastLoggedInProvider()
        /// {
        ///     string lastLoggedInProvider = Gamebase.GetLastLoggedInProvider();
        /// } 
        /// </code>
        /// </example>
        public static string GetLastLoggedInProvider()
        {
            return GamebaseImplementation.Instance.GetLastLoggedInProvider();
        }

        /// <summary>
        /// Gets the language code set for the current device.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The language code that is currently set.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetLanguageCode()
        /// {
        ///     string languageCode = Gamebase.GetLanguageCode();
        /// } 
        /// </code>
        /// </example>
        public static string GetLanguageCode()
        {
            return GamebaseImplementation.Instance.GetLanguageCode();
        }

        /// <summary>
        /// Gets the carrier code set for the current device.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The carrier code set for the current device.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetCarrierCode()
        /// {
        ///     string carrierCode = Gamebase.GetCarrierCode();
        /// } 
        /// </code>
        /// </example>
        public static string GetCarrierCode()
        {
            return GamebaseImplementation.Instance.GetCarrierCode();
        }

        /// <summary>
        /// Gets the carrier name set for the current device.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The carrier name set for the current device.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetCarrierName()
        /// {
        ///     string carrierName = Gamebase.GetCarrierName();
        /// }  
        /// </code>
        /// </example>
        public static string GetCarrierName()
        {
            return GamebaseImplementation.Instance.GetCarrierName();
        }

        /// <summary>
        /// Gets the country code.
        /// First, get the country code set in USIM, 
        /// and if there is no USIM, get the country code set in the device.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The country code.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetCountryCode()
        /// {
        ///     string countryCode = Gamebase.GetCountryCode();
        /// }
        /// </code>
        /// </example>
        public static string GetCountryCode()
        {
            return GamebaseImplementation.Instance.GetCountryCode();
        }

        /// <summary>
        /// Gets the country code set in USIM. 
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The country code that is set in USIM.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetCountryCodeOfUSIM()
        /// {
        ///     string countryCodeOfUSIM = Gamebase.GetCountryCodeOfUSIM();
        /// }
        /// </code>
        /// </example>
        public static string GetCountryCodeOfUSIM()
        {
            return GamebaseImplementation.Instance.GetCountryCodeOfUSIM();
        }

        /// <summary>
        /// Gets the country code currently set on the device.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>The country code currently set on the device.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetCountryCodeOfDevice()
        /// {
        ///     string countryCodeOfDevice = Gamebase.GetCountryCodeOfDevice();
        /// }
        /// </code>
        /// </example>
        public static string GetCountryCodeOfDevice()
        {
            return GamebaseImplementation.Instance.GetCountryCodeOfDevice();
        }

        /// <summary>
        /// Returns whether this project is sandbox mode or not.
        /// @since Added 1.4.0.
        /// </summary>
        /// <returns>Bool value whether this project is sandbox mode.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void IsSandbox()
        /// {
        ///     bool isSandbox = Gamebase.IsSandbox();
        /// }
        /// </code>
        /// </example>
        public static bool IsSandbox()
        {
            return GamebaseImplementation.Instance.IsSandbox();
        }

        /// <summary>
        /// Sets the Gamebase displayLanguage.
        /// @since Added 1.7.0
        /// </summary>
        /// <param name="languageCode">The Gamebase displayLanguage. Please use the GamebaseDisplayLanguageCode class.</param>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void SetDisplayLanguageCode()
        /// {
        ///     Gamebase.SetDisplayLanguageCode(GamebaseDisplayLanguageCode.English);
        /// }
        /// </code>
        /// </example>
        public static void SetDisplayLanguageCode(string languageCode)
        {
            GamebaseImplementation.Instance.SetDisplayLanguageCode(languageCode);
        }

        /// <summary>
        /// Gets the Gamebase displayLanguage.
        /// @since Added 1.7.0
        /// </summary>
        /// <returns>The Gamebase displayLanguage.</returns>
        /// <example> 
        /// Example Usage : 
        /// <code>
        /// public void GetDisplayLanguageCode()
        /// {
        ///     string displayLanguage = Gamebase.GetDisplayLanguageCode();
        /// }
        /// </code>
        /// </example>
        public static string GetDisplayLanguageCode()
        {
            return GamebaseImplementation.Instance.GetDisplayLanguageCode();
        }

        /// <summary>
        /// The Launching class contains several informations that is received from the server after application is successfully launched.
        /// @since Added 1.4.0.
        /// </summary>
        public class Launching
        {
            /// <summary>
            /// Add a callback to be invoked when launching status is changed.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.8.0, use Gamebase.AddObserver(observer) method instead.
            /// </summary>
            /// <param name="callback">The callback that will run.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void AddUpdateStatusListener()
            /// {
            ///     Gamebase.Launching.AddUpdateStatusListener((launchingStatus) => 
            ///     {
            ///         Debug.Log(string.Format("AddUpdateStatusListener launchingStatus code : {0}", launchingStatus.code.ToString()));
            ///         Debug.Log(string.Format("AddUpdateStatusListener launchingStatus message : {0}", launchingStatus.message));
            ///     });
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static void AddUpdateStatusListener(GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus> callback)
            {
                GamebaseLaunchingImplementation.Instance.AddUpdateStatusListener(callback);
            }

            /// <summary>
            /// Remove a callback to be invoked when launching status is changed. 
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.8.0, use Gamebase.RemoveObserver(observer) method instead.
            /// </summary>
            /// <param name="callback">The callback that will removed. </param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RemoveUpdateStatusListener()
            /// {
            ///     Gamebase.Launching.RemoveUpdateStatusListener((launchingStatus) => 
            ///     {
            ///         Debug.Log(string.Format("RemoveUpdateStatusListener launchingStatus code : {0}", launchingStatus.code.ToString()));
            ///         Debug.Log(string.Format("RemoveUpdateStatusListener launchingStatus message : {0}", launchingStatus.message));
            ///     });
            /// } 
            /// </code>
            /// </example>
            [Obsolete]
            public static void RemoveUpdateStatusListener(GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus> callback)
            {
                GamebaseLaunchingImplementation.Instance.RemoveUpdateStatusListener(callback);
            }

            /// <summary>
            /// Get various information provided by the launching server.
            /// @since Added 1.4.0.
            /// </summary>
            /// <returns>The Launching information.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void GetLaunchingInformations()
            /// {            
            ///     Toast.Gamebase.GamebaseResponse.Launching.LaunchingInfo launchingInfo = Gamebase.Launching.GetLaunchingInformations();                
            /// }
            /// </code>
            /// </example>
            public static GamebaseResponse.Launching.LaunchingInfo GetLaunchingInformations()
            {
                return GamebaseLaunchingImplementation.Instance.GetLaunchingInformations();
            }

            /// <summary>
            /// Gets the launching state.
            /// @since Added 1.4.0.
            /// </summary>
            /// <returns>The launching status.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void GetLaunchingStatus()
            /// {
            ///     int launchingStatus = Gamebase.Launching.GetLaunchingStatus();
            /// }
            /// </code>
            /// </example>
            public static int GetLaunchingStatus()
            {
                return GamebaseLaunchingImplementation.Instance.GetLaunchingStatus();
            }
        }

        /// <summary>
        /// The Purchase class provides several APIs related to purchasing processes.
        /// Before using these APIs, You should be logged in. Because every Purchase API need informations that are obtained from GamebaseServer.
        /// If you do not have been authenticated, you will get NOT_LOGGED_IN error.
        /// @since Added 1.4.0.
        /// </summary>
        public class Purchase
        {
            /// <summary>
            /// Call following API of an item to purchase by using itemSeq to send a purchase request. 
            /// When a game user cancels purchasing, the PURCHASE_USER_CANCELED error will be returned.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="itemSeq">Represent to item ID.</param>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RequestPurchase(long itemSeq)
            /// {
            ///     Gamebase.Purchase.RequestPurchase(itemSeq, (purchasableReceipt, error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RequestPurchase succeeded");
            ///     
            ///             Debug.Log("itemSeq : " + purchasableReceipt.itemSeq);
            ///             Debug.Log("price : " + purchasableReceipt.price);
            ///             Debug.Log("currency : " + purchasableReceipt.currency);
            ///             Debug.Log("paymentSeq : " + purchasableReceipt.paymentSeq);
            ///             Debug.Log("purchaseToken : " + purchasableReceipt.purchaseToken);
            ///         }
            ///         else
            ///         {
            ///             if (error.code == GamebaseErrorCode.PURCHASE_USER_CANCELED)
            ///             {
            ///                 Debug.Log("User canceled purchase.");
            ///             }
            ///             else
            ///             {
            ///             	Debug.Log(string.Format("Purchase failed. error is {0}", error));
            ///             }
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RequestPurchase(long itemSeq, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableReceipt> callback)
            {
                GamebasePurchaseImplementation.Instance.RequestPurchase(itemSeq, callback);
            }

            /// <summary>
            /// Request for a list of non-consumed items, which have not been normally consumed (delivered, or provided) after purchase.
            /// In case of non-purchased items, ask the game server (item server) to proceed with item delivery (supply).
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example>
            /// Example Usage : 
            /// <code>
            /// public void RequestItemListOfNotConsumed()
            /// {
            ///     Gamebase.Purchase.RequestItemListOfNotConsumed((purchasableReceiptList, error) => 
            ///     {
            ///         if (gGamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RequestItemListOfNotConsumed is succeeded");
            ///
            ///             foreach (GamebaseResponse.Purchase.PurchasableReceipt purchasableReceipt in purchasableReceiptList)
            ///             {
            ///                 Debug.Log("itemSeq : " + purchasableReceipt.itemSeq);
            ///                 Debug.Log("price : " + purchasableReceipt.price);
            ///                 Debug.Log("currency : " + purchasableReceipt.currency);
            ///                 Debug.Log("paymentSeq : " + purchasableReceipt.paymentSeq);
            ///                 Debug.Log("purchaseToken : " + purchasableReceipt.purchaseToken);
            ///             }
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("RequestItemListOfNotConsumed is failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RequestItemListOfNotConsumed(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableReceipt>> callback)
            {
                GamebasePurchaseImplementation.Instance.RequestItemListOfNotConsumed(callback);
            }

            /// <summary>
            /// In case a purchase is not normally completed after a successful purchase at a store due to failure of authentication of TOAST IAP server, try to reprocess by using API.
            /// Based on the latest success of purchase, reprocessing is required by calling an API for item delivery(supply).
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RequestRetryTransaction()
            /// {
            ///     Gamebase.Purchase.RequestRetryTransaction((purchasableRetryTransactionResult, error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RequestRetryTransaction succeeded");
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("RequestRetryTransaction failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RequestRetryTransaction(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableRetryTransactionResult> callback)
            {
                GamebasePurchaseImplementation.Instance.RequestRetryTransaction(callback);
            }

            /// <summary>
            /// Request a item list which is purchasable. 
            /// This list has items which are registered in both Market(AppStore) and ToastCloud IAP Console.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RequestItemListPurchasable()
            /// {
            ///     Gamebase.Purchase.RequestItemListPurchasable((purchasableItemList, error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RequestItemListPurchasable succeeded");
            ///
            ///             foreach (GamebaseResponse.Purchase.PurchasableItem purchasableItem in purchasableItemList)
            ///             {
            ///                 Debug.Log("itemSeq : " + purchasableItem.itemSeq);
            ///                 Debug.Log("price : " + purchasableItem.price);
            ///                 Debug.Log("currency : " + purchasableItem.currency);
            ///                 Debug.Log("itemName : " + purchasableItem.itemName);
            ///                 Debug.Log("marketItemId : " + purchasableItem.marketItemId);
            ///             }
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("RequestItemListPurchasable failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RequestItemListPurchasable(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>> callback)
            {
                GamebasePurchaseImplementation.Instance.RequestItemListPurchasable(callback);
            }

            /// <summary>
            /// Request a item list which is purchasable. 
            /// This list has items which are only registered in ToastCloud IAP Console, not Market(Google, TStore)
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RequestItemListAtIAPConsole()
            /// {
            ///     Gamebase.Purchase.RequestItemListAtIAPConsole((purchasableItemList, error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RequestItemListAtIAPConsole succeeded");
            ///
            ///             foreach (GamebaseResponse.Purchase.PurchasableItem purchasableItem in purchasableItemList)
            ///             {
            ///                 Debug.Log("itemSeq : " + purchasableItem.itemSeq);
            ///                 Debug.Log("price : " + purchasableItem.price);
            ///                 Debug.Log("currency : " + purchasableItem.currency);
            ///                 Debug.Log("itemName : " + purchasableItem.itemName);
            ///                 Debug.Log("marketItemId : " + purchasableItem.marketItemId);
            ///             }
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("RequestItemListAtIAPConsole failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RequestItemListAtIAPConsole(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>> callback)
            {
                GamebasePurchaseImplementation.Instance.RequestItemListAtIAPConsole(callback);
            }

            /// <summary>
            /// Set the store code of the current app. 
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="storeCode">Input store code.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void SetStoreCode(string storeCode)
            /// {
            ///     Gamebase.Purchase.SetStoreCode(storeCode);
            /// }
            /// </code>
            /// </example>
            public static void SetStoreCode(string storeCode)
            {
                GamebasePurchaseImplementation.Instance.SetStoreCode(storeCode);
            }

            /// <summary>
            /// Gets the store code of the current app. 
            /// This should only be called after the SDK has been initialized by calling Gamebase.initialize().
            /// @since Added 1.4.0.
            /// </summary> 
            /// <returns>The store code.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void GetStoreCode()
            /// {
            ///     string storeCode = Gamebase.Purchase.GetStoreCode();
            ///     if (string.IsNullOrEmpty(storeCode))
            ///     {
            ///         Debug.Log("fail");
            ///     }
            ///     else
            ///     {
            ///         Debug.Log(string.Format("success storeCode : {0}", storeCode));
            ///     }
            /// }
            /// </code>
            /// </example>
            public static string GetStoreCode()
            {
                return GamebasePurchaseImplementation.Instance.GetStoreCode();
            }
        }

        /// <summary>
        /// The Push class provides registering push token API to ToastCloud Push Server and querying push token API.
        /// @since Added 1.4.0.
        /// </summary>
        public class Push
        {
            /// <summary>
            /// Register push information to the push server.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="pushConfiguration">Settings of the notification.</param>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RegisterPush(bool pushEnabled, bool adAgreement, bool adAgreementNight)
            /// {
            ///     GamebaseRequest.Push.PushConfiguration pushConfiguration = new GamebaseRequest.Push.PushConfiguration();
            ///     pushConfiguration.pushEnabled = pushEnabled;
            ///     pushConfiguration.adAgreement = adAgreement;
            ///     pushConfiguration.adAgreementNight = adAgreementNight;
            ///
            ///     Gamebase.Push.RegisterPush(pushConfiguration, (error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("RegisterPush succeeded");
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("RegisterPush failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void RegisterPush(GamebaseRequest.Push.PushConfiguration pushConfiguration, GamebaseCallback.ErrorDelegate callback)
            {
                GamebasePushImplementation.Instance.RegisterPush(pushConfiguration, callback);
            }

            /// <summary>
            /// Get push settings from the the push server.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">Callback pass to API result.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void QueryPush()
            /// {
            ///     Gamebase.Push.QueryPush((pushAdvertisements, error) => 
            ///     {
            ///         if (Gamebase.IsSuccess(error))
            ///         {
            ///             Debug.Log("QueryPush succeeded");
            ///
            ///             Debug.Log("pushEnabled : " + pushAdvertisements.pushEnabled);
            ///             Debug.Log("adAgreement : " + pushAdvertisements.adAgreement);
            ///             Debug.Log("adAgreementNight : " + pushAdvertisements.adAgreementNight);
            ///         }
            ///         else
            ///         {
            ///             Debug.Log(string.Format("QueryPush failed. error is {0}", error));
            ///         }
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void QueryPush(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Push.PushConfiguration> callback)
            {
                GamebasePushImplementation.Instance.QueryPush(callback);
            }

            /// <summary>
            /// Set SandboxMode.(iOS Only)
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="isSandbox">"true" if application is on the sandbox mode.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void SetSandboxMode(bool isSandbox)
            /// {
            ///     Gamebase.Push.SetSandboxMode(isSandbox);
            /// }
            /// </code>
            /// </example>
            public static void SetSandboxMode(bool isSandbox)
            {
                GamebasePushImplementation.Instance.SetSandboxMode(isSandbox);
            }
        }

        /// <summary>
        /// The WebView class represents the entry point for launching WebView.
        /// @since Added 1.4.0.
        /// </summary>
        public class Webview
        {
            /// <summary>
            /// This method to create the webview and display it on screen.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.5.0.
            /// </summary>
            /// <param name="url">The url of the resource to load.</param>            
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowWebBrowser(string url)
            /// {
            ///     Gamebase.Webview.ShowWebBrowser(url);
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static void ShowWebBrowser(string url)
            {
                GamebaseWebviewImplementation.Instance.ShowWebBrowser(url);
            }

            /// <summary>
            /// This method to create the webview and display it on screen.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.5.0, use Gamebase.ShowWebView(string, GamebaseRequest.Webview.GamebaseWebViewConfiguration, GamebaseCallback.ErrorDelegate, List<string> schemeList, GamebaseCallback.GamebaseDelegate<string>) method instead.
            /// </summary>
            /// <param name="configuration">The configuration of webview.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowWebView()
            /// {
            ///     GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = new GamebaseRequest.Webview.GamebaseWebViewConfiguration();
            ///     configuration.title = "Title";
            ///     configuration.orientation = -1;
            ///     configuration.colorR = 128;
            ///     configuration.colorG = 128;
            ///     configuration.colorB = 128;
            ///     configuration.colorA = 255;
            ///     configuration.barHeight = 40;
            ///     configuration.buttonVisible = true;
            ///     configuration.url = "http://www.naver.com";
            ///     
            ///     Gamebase.Webview.ShowWebView(configuration);
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static void ShowWebView(GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration)
            {
                GamebaseWebviewImplementation.Instance.ShowWebView(configuration);
            }

            /// <summary>
            /// This method to create the external web browser and display it on screen.
            /// @since Added 1.5.0.
            /// </summary>
            /// <param name="url">The url of the resource to load.</param>            
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void OpenWebBrowser(string url)
            /// {
            ///     Gamebase.Webview.OpenWebBrowser(url);
            /// }
            /// </code>
            /// </example>
            public static void OpenWebBrowser(string url)
            {
                GamebaseWebviewImplementation.Instance.OpenWebBrowser(url);
            }

            /// <summary>
            /// This method to create the webview and display it on screen.
            /// @since Added 1.5.0.
            /// </summary>
            /// <param name="url">The url of the resource to load.</param>
            /// <param name="configuration">The configuration of webview.</param>
            /// <param name="closeCallback">This callback would be called when webview is closed</param>
            /// <param name="schemeList">This schemeList would be filtered every web view request and call schemeEvent</param>
            /// <param name="schemeEvent">This schemeEvent would be called when web view request matches one of the schemeLlist</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowWebView(GamebaseCallback.ErrorDelegate closeCallback, List<string> schemeList, GamebaseCallback.GamebaseDelegate<string> schemeEvent)
            /// {
            ///     GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = new GamebaseRequest.Webview.GamebaseWebViewConfiguration();
            ///     configuration.title = "Title";
            ///     configuration.orientation = -1;
            ///     configuration.colorR = 128;
            ///     configuration.colorG = 128;
            ///     configuration.colorB = 128;
            ///     configuration.colorA = 255;
            ///     configuration.barHeight = 40;
            ///     configuration.buttonVisible = true;
            ///     
            ///     Gamebase.Webview.ShowWebView("http://www.naver.com", configuration, closeCallback, schemeList, schemeEvent);
            /// }
            /// </code>
            /// </example>
            public static void ShowWebView(string url, GamebaseRequest.Webview.GamebaseWebViewConfiguration configuration = null, GamebaseCallback.ErrorDelegate closeCallback = null, List<string> schemeList = null, GamebaseCallback.GamebaseDelegate<string> schemeEvent = null)
            {
                GamebaseWebviewImplementation.Instance.ShowWebView(url, configuration, closeCallback, schemeList, schemeEvent);
            }

            /// <summary>
            /// This method to close the webview in display on a screen.
            /// @since Added 1.5.0.
            /// </summary>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void CloseWebView()
            /// {
            ///     Gamebase.Webview.CloseWebView();
            /// }
            /// </code>
            /// </example>
            public static void CloseWebView()
            {
                GamebaseWebviewImplementation.Instance.CloseWebView();
            }
        }

        /// <summary>
        /// The Util class provides convenient and useful methods.
        /// @since Added 1.4.0.
        /// </summary>
        public class Util
        {
            /// <summary>
            /// This method to create the dialog and display it on screen.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="title">The title to be shown in the alert.</param>
            /// <param name="message">The message to be shown in the alert.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowAlert(string title, string message)
            /// {
            ///     Gamebase.Util.ShowAlert(
            ///         title,
            ///         message);
            /// }
            /// </code>
            /// </example>
            public static void ShowAlert(string title, string message)
            {
                GamebaseUtilImplementation.Instance.ShowAlert(title, message);
            }

            /// <summary>
            /// This method to create the dialog and display it on screen.
            /// Show Alert View with async completion callback.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="title">The title to be shown in the alert.</param>
            /// <param name="message">The message to be shown in the alert.</param>
            /// <param name="buttonCallback">The buttonCallback to be executed after click ok button.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowAlertCallback(string title, string message)
            /// {
            ///     Gamebase.Util.ShowAlert(
            ///         title,
            ///         message,
            ///         () =>
            ///         {
            ///             Debug.Log("Button Click");
            ///         });
            /// }
            /// </code>
            /// </example>            
            public static void ShowAlert(string title, string message, GamebaseCallback.VoidDelegate buttonCallback)
            {
                GamebaseUtilImplementation.Instance.ShowAlert(title, message, buttonCallback);
            }

            /// <summary>
            /// Show a standard toast that just contains a text view.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.5.0, use Gamebase.ShowToast(string, GamebaseUIToastType) method instead.
            /// </summary>
            /// <param name="message">The message to be shown in the alert.</param>
            /// <param name="duration">The time interval to be exposed.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowToast(string message, int duration)
            /// {
            ///     Gamebase.Util.ShowToast(
            ///         message,
            ///         duration
            ///         );
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static void ShowToast(string message, int duration)
            {
                GamebaseUtilImplementation.Instance.ShowToast(message, duration);
            }

            /// <summary>
            /// Show a standard toast that just contains a text view.
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="message">The message to be shown in the alert.</param>
            /// <param name="type">The time interval to be exposed. GamebaseUIToastType.TOAST_LENGTH_LONG (3.5 seconds), GamebaseUIToastType.TOAST_LENGTH_SHORT (2 seconds)</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void ShowToast(string message, GamebaseUIToastType type)
            /// {
            ///     Gamebase.Util.ShowToast(
            ///         message,
            ///         type
            ///         );
            /// }
            /// </code>
            /// </example>           
            public static void ShowToast(string message, GamebaseUIToastType type)
            {
                GamebaseUtilImplementation.Instance.ShowToast(message, type);
            }
        }

        /// <summary>
        /// The Network class indicates Network status.
        /// @since Added 1.4.0.
        /// </summary>
        public class Network
        {
            /// <summary>
            /// Reports the type of network to which the info in this NetworkInfo pertains.
            /// @since Added 1.4.0.
            /// </summary>
            /// <returns>one of GamebaseNetworkType.TYPE_NOT, GamebaseNetworkType.TYPE_MOBILE, GamebaseNetworkType.TYPE_WIFI, GamebaseNetworkType.TYPE_ANY, or other types defined by NetworkManager.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void GetType()
            /// {
            ///     GamebaseNetworkType type = Gamebase.Network.GetType();
            /// }
            /// </code>
            /// </example>
            public static GamebaseNetworkType GetNetworkType()
            {
                return GamebaseNetworkImplementation.Instance.GetNetworkType();
            }

            /// <summary>
            /// Return a human-readable name describe the type of the network, for example "WIFI" or "MOBILE".
            /// @since Added 1.4.0.
            /// </summary>
            /// <returns>The name of the network type.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void GetTypeName()
            /// {
            ///     string networkName = Gamebase.Network.GetTypeName();
            /// }
            /// </code>
            /// </example>
            public static string GetNetworkTypeName()
            {
                return GamebaseNetworkImplementation.Instance.GetNetworkTypeName();
            }

            /// <summary>
            /// Indicates whether network connectivity exists and it is possible to establish connections and pass data. (Platforms other than WebGL)
            /// @since Added 1.4.0.
            /// </summary>
            /// <returns> "true" if network connectivity exists, "false" otherwise.</returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void IsConnected()
            /// {
            ///     bool isConnected = Gamebase.Network.IsConnected();
            /// }
            /// </code>
            /// </example>
            public static bool IsConnected()
            {
                return GamebaseNetworkImplementation.Instance.IsConnected();
            }

            /// <summary>
            /// Indicates whether network connectivity exists and it is possible to establish connections and pass data. (Only WebGL platform)
            /// @since Added 1.4.0.
            /// </summary>
            /// <param name="callback">"true" if network connectivity exists, "false" otherwise.</param>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void IsConnected()
            /// {
            ///     Gamebase.Network.IsConnected((reachable) => 
            ///     {
            ///         Debug.Log(string.Format("Internet reachability is {0}", reachable));
            ///     });
            /// }
            /// </code>
            /// </example>
            public static void IsConnected(GamebaseCallback.DataDelegate<bool> callback)
            {
                GamebaseNetworkImplementation.Instance.IsConnected(callback);
            }

            /// <summary>
            /// Add the callback to be called when the network status is changed.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.8.0, use Gamebase.AddObserver(observer) method instead.
            /// </summary>
            /// <param name="callback">The callback to be invoked when the network status is changed.</param>
            /// <returns>"true" if callback registration succeeded, "false" otherwise </returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void AddOnChangedStatusListener()
            /// {
            ///     bool isSuccess = Gamebase.Network.AddOnChangedStatusListener(OnChangeNetworkMain);
            /// }
            /// 
            /// public void OnChangeNetwork(GamebaseNetworkType type)
            /// {
            ///     Debug.Log(string.Format("OnChangeNetwork GamebaseNetworkType : {0}", type.ToString()));
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static bool AddOnChangedStatusListener(GamebaseCallback.DataDelegate<GamebaseNetworkType> callback)
            {
                return GamebaseNetworkImplementation.Instance.AddOnChangedStatusListener(callback);
            }

            /// <summary>
            /// Remove callbacks added by AddOnChangedStatusListener method.
            /// @since Added 1.4.0.
            /// @deprecated As of release 1.8.0, use Gamebase.RemoveObserver(observer) method instead.
            /// </summary>
            /// <param name="callback">The callback to be removed.</param>
            /// <returns>"true "if callback unregistration succeeded, "false" otherwise </returns>
            /// <example> 
            /// Example Usage : 
            /// <code>
            /// public void RemoveOnChangedStatusListener()
            /// {
            ///     bool isSuccess = Gamebase.Network.RemoveOnChangedStatusListener(OnChangeNetwork);
            /// }
            /// 
            /// public void OnChangeNetwork(GamebaseNetworkType type)
            /// {
            ///     Debug.Log(string.Format("OnChangeNetwork GamebaseNetworkType : {0}", type.ToString()));
            /// }
            /// </code>
            /// </example>
            [Obsolete]
            public static bool RemoveOnChangedStatusListener(GamebaseCallback.DataDelegate<GamebaseNetworkType> callback)
            {
                return GamebaseNetworkImplementation.Instance.RemoveOnChangedStatusListener(callback);
            }
        }
    }
}