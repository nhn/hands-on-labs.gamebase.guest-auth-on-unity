#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System;
using System.Collections;
using Toast.Gamebase.LitJson;
using Toast.Gamebase.Single.Communicator;
using UnityEngine;

namespace Toast.Gamebase.Single
{
    public class CommonGamebaseLaunching : IGamebaseLaunching
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (true == string.IsNullOrEmpty(domain))
                {
                    return typeof(CommonGamebaseLaunching).Name;
                }

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        private WebSocketRequest.RequestVO requestVO;
        private int scheduleHandle                  = -1;
        private bool isPlaySchedule                 = false;
        private float launchingStatusInterval       = 0;
        private float launchingStatusExpire         = 0;

        public CommonGamebaseLaunching()
        {
            requestVO = LaunchingMessage.GetLaunchingInfoMessage();
        }
        
        [Obsolete("AddUpdateStatusListener is deprecated.")]
        public bool AddUpdateStatusListener(int handle)
        {
            scheduleHandle = handle;
            return true;
        }

        [Obsolete("RemoveUpdateStatusListener is deprecated.")]
        public bool RemoveUpdateStatusListener(int handle)
        {
            scheduleHandle = -1;
            return true;
        }
        
        public GamebaseResponse.Launching.LaunchingInfo GetLaunchingInformations()
        {
            if (false == GamebaseUnitySDK.IsInitialized)
            {
                GamebaseLog.Warn(GamebaseStrings.NOT_INITIALIZED, this, "GetLaunchingInformations");
                return null;
            }

            var vo = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
            return JsonMapper.ToObject<GamebaseResponse.Launching.LaunchingInfo>(JsonMapper.ToJson(vo));
        }

        public int GetLaunchingStatus()
        {
            if (false == GamebaseUnitySDK.IsInitialized)
            {
                GamebaseLog.Warn(GamebaseStrings.NOT_INITIALIZED, this, "GetLaunchingStatus");
                return 0;
            }

            var vo = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
            return vo.launching.status.code;
        }

        public void GetLaunchingInfo(int handle)
        {
            requestVO.apiId = Lighthouse.API.Launching.ID.GET_LAUNCHING;
            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                if (null == error)
                {
                    var vo = JsonMapper.ToObject<LaunchingResponse.LaunchingInfo>(response);                    

                    if (true == vo.header.isSuccessful)
                    {
                        DataContainer.SetData(VOKey.Launching.LAUNCHING_INFO, vo);
                        Gamebase.SetDisplayLanguageCode(vo.request.displayLanguage);

                        GamebaseSystemPopup.Instance.ShowLaunchingPopup(vo);
                    }
                    else
                    {
                        error = GamebaseErrorUtil.CreateGamebaseErrorByServerErrorCode(requestVO.transactionId, requestVO.apiId, vo.header, Domain);

                        GamebaseSystemPopup.Instance.ShowErrorPopup(error);
                    }
                }
                else
                {
                    GamebaseSystemPopup.Instance.ShowErrorPopup(error);
                }

                var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo>>(handle);
                if (null == callback)
                {
                    return;
                }

                if (null == error)
                {
                    GamebaseUnitySDK.IsInitialized = true;
                    callback(JsonMapper.ToObject<GamebaseResponse.Launching.LaunchingInfo>(response), error);
                    ExecuteSchedule();
                }   
                else
                {
                    callback(null, error);
                }
            });
        }
        
        #region Schedule
        private void ExecuteSchedule()
        {
            GamebaseLog.Debug("Start Launching Status Schedule", this, "ExecuteSchedule");

            if (true == isPlaySchedule)
            {
                return;
            }

            isPlaySchedule = true;
            GamebaseUnitySDKSettings.Instance.StartCoroutine(ScheduleLaunchingStatus());            
        }

        private IEnumerator ScheduleLaunchingStatus()
        {
            GamebaseLog.Debug("Start Launching Status Schedule", this, "ScheduleLaunchingStatus");

            launchingStatusInterval     = 0f;
            launchingStatusExpire       = 0f;

            while (true == isPlaySchedule)
            {
                launchingStatusInterval     += Time.unscaledDeltaTime;
                launchingStatusExpire       += Time.unscaledDeltaTime;

                if( CommunicatorConfiguration.launchingInterval <= launchingStatusInterval)
                {
                    launchingStatusInterval = 0;
                    launchingStatusExpire = 0;

                    RequestLaunchingStatus();
                }
                
                yield return null;
            }            
        }

        public void RequestLaunchingStatus(int handle = -1)
        {
            GamebaseLog.Debug("Check Launching Status", this, "RequestLaunchingStatus");
            requestVO.apiId = Lighthouse.API.Launching.ID.GET_LAUNCHING_STATUS;
            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                if (null != error)
                {
                    return;
                }

                var launchingInfo = DataContainer.GetData<LaunchingResponse.LaunchingInfo>(VOKey.Launching.LAUNCHING_INFO);
                var launchingStatus = JsonMapper.ToObject<LaunchingResponse.LaunchingStatus>(response);
                if (launchingInfo.launching.status.code == launchingStatus.launching.status.code)
                {
                    OnLaunchingInfoCallback(handle);
                    return;
                }

                GamebaseLog.Debug("Check Launching Info", this, "RequestLaunchingStatus");

                GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo> launchingInfoCallback = (launchingInfoTemp, errorTemp) =>
                {
                    var launchingStatusCallback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<GamebaseResponse.Launching.LaunchingStatus>>(scheduleHandle);
                    if (null != launchingStatusCallback)
                    {
                        var vo = new GamebaseResponse.Launching.LaunchingStatus();
                        vo.code = launchingStatus.launching.status.code;
                        vo.message = launchingStatus.launching.status.message;

                        launchingStatusCallback(vo);
                    }
                    
                    var observerCallback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage>>(GamebaseObserverManager.Instance.Handle);
                    if (null != observerCallback)
                    {
                        var vo = new GamebaseResponse.SDK.ObserverMessage();
                        vo.type = GamebaseObserverType.LAUNCHING;
                        vo.data = new System.Collections.Generic.Dictionary<string, object>();
                        vo.data.Add("code", launchingStatus.launching.status.code);
                        vo.data.Add("message", launchingStatus.launching.status.message);
                        observerCallback(vo);
                    }

                    OnLaunchingInfoCallback(handle);
                };

                int handleTemp = GamebaseCallbackHandler.RegisterCallback(launchingInfoCallback);

                GetLaunchingInfo(handleTemp);
            });
        }
        #endregion
        
        private void OnLaunchingInfoCallback(int handle)
        {
            if(-1 == handle)
            {
                return;
            }

            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Launching.LaunchingInfo>>(handle);
            if (null == callback)
            {
                return;
            }
            callback(null, null);
        }

        public static bool IsPlayable()
        {
            var status = Gamebase.Launching.GetLaunchingStatus();
            // 200 ~ 299 playable
            if (200 <= status && 300 > status)
            {
                return true;
            }

            return false;
        }

        public float GetLaunchingStatusExpire()
        {
            return launchingStatusExpire;
        }

        public void ResetLaunchingStatusExpire()
        {
            launchingStatusExpire = CommunicatorConfiguration.launchingExpire;
        }
    }
}
#endif