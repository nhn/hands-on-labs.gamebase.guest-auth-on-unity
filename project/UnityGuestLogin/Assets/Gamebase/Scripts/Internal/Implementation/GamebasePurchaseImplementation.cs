#if !UNITY_EDITOR && UNITY_ANDROID
using Toast.Gamebase.Mobile.Android;
#elif !UNITY_EDITOR && UNITY_IOS
using Toast.Gamebase.Mobile.IOS;
#elif !UNITY_EDITOR && UNITY_WEBGL
using Toast.Gamebase.Single.WebGL;
#else
using Toast.Gamebase.Single.Standalone;
#endif
using System.Collections.Generic;

namespace Toast.Gamebase
{
    public sealed class GamebasePurchaseImplementation
    {
        private static readonly GamebasePurchaseImplementation instance = new GamebasePurchaseImplementation();

        public static GamebasePurchaseImplementation Instance
        {
            get { return instance; }
        }

        IGamebasePurchase purchase;

        private GamebasePurchaseImplementation()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            purchase = new AOSGamebasePurchase();
#elif !UNITY_EDITOR && UNITY_IOS
            purchase = new IOSGamebasePurchase();
#elif !UNITY_EDITOR && UNITY_WEBGL
            purchase = new WebGLGamebasePurchase();
#else
            purchase = new StandaloneGamebasePurchase();
#endif
        }

        public void RequestPurchase(long itemSeq, GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableReceipt> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            purchase.RequestPurchase(itemSeq, handle);
        }

        public void RequestItemListOfNotConsumed(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableReceipt>> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            purchase.RequestItemListOfNotConsumed(handle);
        }

        public void RequestRetryTransaction(GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableRetryTransactionResult> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            purchase.RequestRetryTransaction(handle);
        }

        public void RequestItemListPurchasable(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            purchase.RequestItemListPurchasable(handle);
        }

        public void RequestItemListAtIAPConsole(GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>> callback)
        {
            int handle = GamebaseCallbackHandler.RegisterCallback(callback);
            purchase.RequestItemListAtIAPConsole(handle);
        }

        public void SetStoreCode(string storeCode)
        {
            purchase.SetStoreCode(storeCode);
        }

        public string GetStoreCode()
        {
            return purchase.GetStoreCode();
        }
    }
}