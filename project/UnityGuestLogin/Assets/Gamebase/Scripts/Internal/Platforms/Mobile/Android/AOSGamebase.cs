#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebase : NativeGamebase
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebasePlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif