#if UNITY_EDITOR || UNITY_STANDALONE

namespace Toast.Gamebase.Single.Standalone
{
    public class StandaloneGamebasePurchase : CommonGamebasePurchase
    {
        public StandaloneGamebasePurchase()
        {
            Domain = typeof(StandaloneGamebasePurchase).Name;
        }
    }
}
#endif