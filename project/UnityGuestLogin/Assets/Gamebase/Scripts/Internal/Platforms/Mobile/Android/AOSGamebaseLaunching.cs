#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebaseLaunching : NativeGamebaseLaunching
    {
         protected override void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebaseLaunchingPlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif