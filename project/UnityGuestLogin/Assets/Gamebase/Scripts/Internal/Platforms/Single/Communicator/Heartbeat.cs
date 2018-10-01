#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL)

using System;
using System.Collections;
using Toast.Gamebase.LitJson;
using UnityEngine;

namespace Toast.Gamebase.Single.Communicator
{
    public sealed class Heartbeat
    {
        private static readonly Heartbeat instance = new Heartbeat();
        
        private enum HeartbeatStatus
        {
            Start   = 0,
            Stop    = 1
        }

        public static Heartbeat Instance
        {
            get { return instance; }
        }

        private HeartbeatStatus status;
        private DateTime lastSentTime;
        private const float waitTime = 0.5f;

        public Heartbeat()
        {
            status          = HeartbeatStatus.Stop;
            lastSentTime    = DateTime.Now;
        }

        public void StartHeartbeat()
        {
            if (false == IsSendable())
            {
                return;
            }

            GamebaseLog.Debug("Start Heartbeat", this, "StartHeartbeat");

            status = HeartbeatStatus.Start;
            GamebaseUnitySDKSettings.Instance.StartCoroutine(ExecuteHeartbeat());
        }

        public void StopHeartbeat()
        {
            if (HeartbeatStatus.Stop == status)
            {
                return;
            }

            GamebaseLog.Debug("Stop Heartbeat", this, "StopHeartbeat");

            status = HeartbeatStatus.Stop;
        }

        private IEnumerator ExecuteHeartbeat()
        {
            if (HeartbeatStatus.Stop == status)
            {
                yield break;
            }

            var timestamp = (int)(DateTime.Now - lastSentTime).TotalSeconds;
            if (CommunicatorConfiguration.heartbeatInterval <= timestamp)
            {
                SendHeartbeat();
            }
            
            yield return new WaitForSecondsRealtime(waitTime);
            GamebaseUnitySDKSettings.Instance.StartCoroutine(ExecuteHeartbeat());
        }

        private void SendHeartbeat()
        {
            var requestVO = HeartbeatMessage.GetHeartbeatMessage();
            WebSocket.Instance.Request(requestVO, (response, error) =>
            {
                GamebaseError heartbeatError = error;

                if (null == heartbeatError)
                {
                    var vo = JsonMapper.ToObject<LaunchingResponse.HeartbeatInfo>(response);
                    if (true == vo.header.isSuccessful)
                    {
                        GamebaseLog.Debug("Send heartbeat succeeded", this, "SendHeartbeat");
                    }
                    else
                    {
                        heartbeatError = GamebaseErrorUtil.CreateGamebaseErrorByServerErrorCode(requestVO.transactionId, requestVO.apiId, vo.header, string.Empty);
                    }
                }

                if(null != heartbeatError)
                {
                    if (GamebaseErrorCode.BANNED_MEMBER == heartbeatError.code || GamebaseErrorCode.INVALID_MEMBER == heartbeatError.code)
                    {
                        GamebaseSystemPopup.Instance.ShowHeartbeatErrorPopup(heartbeatError);

                        var observerCallback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.DataDelegate<GamebaseResponse.SDK.ObserverMessage>>(GamebaseObserverManager.Instance.Handle);
                        if (null != observerCallback)
                        {
                            var vo = new GamebaseResponse.SDK.ObserverMessage();
                            vo.type = GamebaseObserverType.HEARTBEAT;
                            vo.data = new System.Collections.Generic.Dictionary<string, object>();
                            vo.data.Add("code", heartbeatError.code);
                            observerCallback(vo);
                        }
                    }
                    GamebaseLog.Debug(string.Format("Send heartbeat failed. error:{0}", heartbeatError), this, "SendHeartbeat");
                }

                lastSentTime = DateTime.Now;
            });
        }
        
        private bool IsSendable()
        {
            if (HeartbeatStatus.Start == status)
            {
                return false;
            }

            if (false == IsLoggedin())
            {
                return false;
            }

            return true;
        }

        private bool IsLoggedin()
        {
            if (true == string.IsNullOrEmpty(Gamebase.GetUserID()))
            {
                return false;
            }

            return true;
        }
    }
}
#endif