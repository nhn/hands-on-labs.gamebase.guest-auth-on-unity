using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Toast.Gamebase.LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace Toast.Gamebase
{
    public sealed class IndicatorReport
    {
        public class Level
        {
            public const string DEBUG = "DEBUG";
            public const string ERROR = "ERROR";
        }
        public class FieldName
        {
            public const string UNITY_EDITOR_VERSION = "UNITY_EDITOR_VERSION";
        }
        
        private static readonly IndicatorReport instance = new IndicatorReport();

        public static IndicatorReport Instance
        {
            get { return instance; }
        }

        private const string url                                    = "http://api-logncrash.cloud.toast.com/v2/log";
        private const string appKey                                 = "XJ4GrOPcU3xVvwOS";
        private const string projectVersion                         = "1.0.0";
        private const string logVersion                             = "v2";
        

        #region key for basic data
        private const string BASIC_DATA_KEY_LAUNCHING_ZONE          = "LaunchingZone";
        private const string BASIC_DATA_KEY_APP_ID                  = "AppID";
        private const string BASIC_DATA_KEY_APP_VERSION             = "ClientVersion";
        private const string BASIC_DATA_KEY_PLATFORM                = "Platform";
        private const string BASIC_DATA_KEY_PROJECT_VERSION         = "projectVersion";
        private const string BASIC_DATA_KEY_LOG_VERSION             = "logVersion";
        #endregion

        #region key for send data
        private const string SEND_DATA_KEY_PROJECT_NAME             = "projectName";
        private const string SEND_DATA_KEY_LOG_LEVEL                = "logLevel";
        private const string SEND_DATA_KEY_BODY                     = "body";
        private const string SEND_DATA_KEY_CLIENT_LOG_TIME          = "ClientLogTime";
        #endregion

        #region key for custom data
        public const string SEND_DATA_KEY_UNITY_EDITOR_VERSION     = "UnityEditorVersion";
        #endregion

        private Dictionary<string, string> basicDataDic;

        private IndicatorReport()
        {
            CreateBasicData();
        }

        public void SendIndicator(string level, string fieldName, Dictionary<string, string> data)
        {
            GamebaseUnitySDKSettings.Instance.StartCoroutine(SendHTTPPost(level, fieldName, data));
        }

        private IEnumerator SendHTTPPost(string level, string fieldName, Dictionary<string, string> data)
        {
            if(true == string.IsNullOrEmpty(url) || true == string.IsNullOrEmpty(appKey))
            {
                GamebaseLog.Debug("Failed Message : Url or appKey is null or empty!", this, "SendHTTPPost");
                yield break;
            }

            Dictionary<string, string> sendDataDic = CreateSendData(level, fieldName, data);

            var jsonString      = JsonMapper.ToJson(sendDataDic);
            var encoding        = new UTF8Encoding().GetBytes(jsonString);            

            UnityWebRequest www = UnityWebRequest.Put(url, encoding);
            www.timeout         = 10;
            www.method          = "POST";
            www.SetRequestHeader("Content-Type", "application/json");            

            yield return UnityCompatibility.UnityWebRequest.Send(www);

            if (true == www.isDone)
            {
                if (200 == www.responseCode)
                {
                    if (true == UnityCompatibility.UnityWebRequest.IsError(www))
                    {
                        GamebaseLog.Debug(string.Format("Failed Error : {0}", www.error), this, "SendHTTPPost");
                    }
                    else
                    {
                        GamebaseLog.Debug("Success", this, "SendHTTPPost");
                    }
                }
                else
                {
                    GamebaseLog.Debug(string.Format("Failed ResponseCode : {0}", www.responseCode), this, "SendHTTPPost");
                }
            }
        }

        private void CreateBasicData()
        {
            basicDataDic = new Dictionary<string, string>();

            basicDataDic.Add(BASIC_DATA_KEY_LAUNCHING_ZONE,     GamebaseUnitySDK.ZoneType);
            basicDataDic.Add(BASIC_DATA_KEY_APP_ID,             GamebaseUnitySDK.AppID);
            basicDataDic.Add(BASIC_DATA_KEY_APP_VERSION,        GamebaseUnitySDK.AppVersion);
            basicDataDic.Add(BASIC_DATA_KEY_PLATFORM,           Application.platform.ToString());
            basicDataDic.Add(BASIC_DATA_KEY_PROJECT_VERSION,    projectVersion);
            basicDataDic.Add(BASIC_DATA_KEY_LOG_VERSION,        logVersion);

        }

        private Dictionary<string, string> CreateSendData(string level, string fieldName, Dictionary<string, string> customData)
        {
            Dictionary<string, string> sendDataDic = new Dictionary<string, string>(basicDataDic);
            sendDataDic.Add(SEND_DATA_KEY_BODY,             fieldName);
            sendDataDic.Add(SEND_DATA_KEY_LOG_LEVEL,        level);
            sendDataDic.Add(SEND_DATA_KEY_PROJECT_NAME,     appKey);
            sendDataDic.Add(SEND_DATA_KEY_CLIENT_LOG_TIME,  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff 'GMT'zzz"));

            SetCustomData(ref sendDataDic, customData);            
            return sendDataDic;
        }

        private void SetCustomData(ref Dictionary<string, string> sendDataDic, Dictionary<string, string> customData)
        {
            if (null == customData)
            {
                return;
            }

            foreach (string key in customData.Keys)
            {
                sendDataDic.Add(key, customData[key]);
            }
        }
    }
}