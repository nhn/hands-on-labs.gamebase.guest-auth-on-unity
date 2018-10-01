#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
using UnityEngine;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class NativeMessageReceiver : MonoBehaviour
    {
        public void OnAsyncEvent(string jsonString)
        {
            GamebaseLog.Debug(string.Format("jsonString : {0}", jsonString), this, "OnAsyncEvent");

            NativeMessage                   message         = JsonMapper.ToObject<NativeMessage>(jsonString);
            DelegateManager.DelegateData    delegateData    = DelegateManager.GetDelegate(message.scheme);

            if (null != delegateData)
            {
                if (null != delegateData.pluginEventDelegate)
                {
                    delegateData.pluginEventDelegate(message);
                }
                if (null != delegateData.eventDelegate)
                {
                    delegateData.eventDelegate(message);
                }
            }            
        }
    }
}
#endif