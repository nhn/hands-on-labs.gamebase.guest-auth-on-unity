#if UNITY_EDITOR || UNITY_STANDALONE

namespace Toast.Gamebase.Single.Standalone
{
    public class StandaloneGamebasePush : CommonGamebasePush
    {
        public StandaloneGamebasePush()
        {
            Domain = typeof(StandaloneGamebasePush).Name;
        }
    }
}
#endif