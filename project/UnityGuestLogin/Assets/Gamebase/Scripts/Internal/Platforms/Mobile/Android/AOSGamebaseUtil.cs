#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebaseUtil : NativeGamebaseUtil
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebaseUtilPlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif