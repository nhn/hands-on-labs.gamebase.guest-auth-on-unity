#if UNITY_EDITOR || UNITY_ANDROID

namespace Toast.Gamebase.Mobile.Android
{
    public class AOSGamebasePurchase : NativeGamebasePurchase
    {
        override protected void Init()
        {
            CLASS_NAME      = "com.toast.gamebase.gamebaseplugin.GamebasePurchasePlugin";
            messageSender   = AOSMessageSender.Instance;

            base.Init();
        }
    }
}
#endif