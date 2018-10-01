#if UNITY_EDITOR || UNITY_WEBGL

using System.Collections.Generic;

namespace Toast.Gamebase.Single.WebGL
{
    public class WebGLGamebasePurchase : CommonGamebasePurchase
    {
        public WebGLGamebasePurchase()
        {
            Domain = typeof(WebGLGamebasePurchase).Name;
        }
    }
}
#endif