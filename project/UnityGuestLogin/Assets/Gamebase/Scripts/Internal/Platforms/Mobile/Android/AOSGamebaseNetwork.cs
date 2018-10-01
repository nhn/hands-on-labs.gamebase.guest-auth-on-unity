#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebaseNetwork : NativeGamebaseNetwork
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebaseNetworkPlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif