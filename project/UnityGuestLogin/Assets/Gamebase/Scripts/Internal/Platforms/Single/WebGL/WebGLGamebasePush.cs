#if UNITY_EDITOR || UNITY_WEBGL

namespace Toast.Gamebase.Single.WebGL
{
    public class WebGLGamebasePush : CommonGamebasePush
    {
        public WebGLGamebasePush()
        {
            Domain = typeof(WebGLGamebasePush).Name;
        }
    }
}
#endif