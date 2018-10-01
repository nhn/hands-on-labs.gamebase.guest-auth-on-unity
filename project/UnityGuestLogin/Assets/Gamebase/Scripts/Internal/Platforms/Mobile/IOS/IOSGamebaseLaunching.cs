#if UNITY_EDITOR || UNITY_IOS

namespace Toast.Gamebase.Mobile.IOS
{
    public class IOSGamebaseLaunching : NativeGamebaseLaunching
    {
        protected override void Init()
        {
            CLASS_NAME      = "TCGBLaunchingPlugin";
            messageSender   = IOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif