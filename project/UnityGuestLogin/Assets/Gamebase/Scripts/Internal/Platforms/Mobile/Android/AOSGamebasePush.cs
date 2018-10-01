#if UNITY_EDITOR || UNITY_ANDROID
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebasePush : NativeGamebasePush
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebasePushPlugin";
            messageSender   = AOSMessageSender.Instance;
            
            base.Init();
        }

        override public void SetSandboxMode(bool isSandbox)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(this, "SetSandboxMode");
        }
    }
}
#endif