#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebaseAuth : NativeGamebaseAuth
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebaseAuthPlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif