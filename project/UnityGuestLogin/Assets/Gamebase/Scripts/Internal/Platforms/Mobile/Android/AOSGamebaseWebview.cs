#if UNITY_EDITOR || UNITY_ANDROID
using System.Collections.Generic;
using Toast.Gamebase.LitJson;
using UnityEngine;

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebaseWebview : NativeGamebaseWebview
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebaseWebviewPlugin";
            messageSender   = AOSMessageSender.Instance;
            
            base.Init();
        }
    }
}
#endif