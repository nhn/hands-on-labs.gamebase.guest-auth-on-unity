#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS

using System.Collections.Generic;
using Toast.Gamebase.LitJson;

namespace Toast.Gamebase.Mobile
{
    public class NativeGamebasePurchase : IGamebasePurchase
    {
        protected class GamebasePurchase
        {
            public const string PURCHASE_API_REQUEST_PURCHASE                    = "gamebase://requestPurchase";
            public const string PURCHASE_API_REQUEST_ITEM_LIST_OF_NOT_CONSUMED   = "gamebase://requestItemListOfNotConsumed";
            public const string PURCHASE_API_REQUEST_RETYR_TRANSACTION           = "gamebase://requestRetryTransaction";
            public const string PURCHASE_API_REQUEST_ITEM_LIST_PURCHASABLE       = "gamebase://requestItemListPurchasable";
            public const string PURCHASE_API_REQUEST_ITEM_LIST_AT_AP_CONSOLE     = "gamebase://requestItemListAtIAPConsole";
            public const string PURCHASE_API_SET_STORE_CODE                      = "gamebase://setStoreCode";
            public const string PURCHASE_API_GET_STORE_CODE                      = "gamebase://getStoreCode";
        }

        protected INativeMessageSender  messageSender   = null;
        protected string                CLASS_NAME      = string.Empty;

        public NativeGamebasePurchase()
        {
            Init();
        }

        virtual protected void Init()
        {
            messageSender.Initialize(CLASS_NAME);

            DelegateManager.AddDelegate(GamebasePurchase.PURCHASE_API_REQUEST_PURCHASE,                     DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Purchase.PurchasableReceipt>);
            DelegateManager.AddDelegate(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_OF_NOT_CONSUMED,    DelegateManager.SendGamebaseDelegateOnce<List<GamebaseResponse.Purchase.PurchasableReceipt>>);
            DelegateManager.AddDelegate(GamebasePurchase.PURCHASE_API_REQUEST_RETYR_TRANSACTION,            DelegateManager.SendGamebaseDelegateOnce<GamebaseResponse.Purchase.PurchasableRetryTransactionResult>);
            DelegateManager.AddDelegate(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_PURCHASABLE,        DelegateManager.SendGamebaseDelegateOnce<List<GamebaseResponse.Purchase.PurchasableItem>>);
            DelegateManager.AddDelegate(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_AT_AP_CONSOLE,      DelegateManager.SendGamebaseDelegateOnce<List<GamebaseResponse.Purchase.PurchasableItem>>);
        }

        virtual public void RequestPurchase(long itemSeq, int handle)
        {
            NativeRequest.Purchase.PurchaseItemSeq vo = new NativeRequest.Purchase.PurchaseItemSeq();
            vo.itemSeq = itemSeq;

            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_REQUEST_PURCHASE, jsonData: JsonMapper.ToJson(vo), handle:handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
        }

        virtual public void RequestItemListOfNotConsumed(int handle)
		{
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_OF_NOT_CONSUMED, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
		}

        virtual public void RequestRetryTransaction(int handle)
		{
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_REQUEST_RETYR_TRANSACTION, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
		}

        virtual public void RequestItemListPurchasable(int handle)
		{
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_PURCHASABLE, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
		}

        virtual public void RequestItemListAtIAPConsole(int handle)
		{
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_REQUEST_ITEM_LIST_AT_AP_CONSOLE, handle: handle, gameObjectName: GamebaseUnitySDK.ObjectName, requestMethodName: "OnAsyncEvent"));
            messageSender.GetAsync(jsonData);
		}

        virtual public void SetStoreCode(string storeCode)
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_SET_STORE_CODE, jsonData: storeCode));
            messageSender.GetSync(jsonData);
        }

        virtual public string GetStoreCode()
        {
            string jsonData = JsonMapper.ToJson(new UnityMessage(GamebasePurchase.PURCHASE_API_GET_STORE_CODE));
            return messageSender.GetSync(jsonData);
        }
    }
}

#endif
