#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System.Collections.Generic;

namespace Toast.Gamebase.Single
{
    public class CommonGamebasePurchase : IGamebasePurchase
    {
        private string domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                    return typeof(CommonGamebasePurchase).Name;

                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public void RequestPurchase(long itemSeq, int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableReceipt>>(handle);

            if (IsLoggedIn() == false)
            {
                callback(null, new GamebaseError(GamebaseErrorCode.NOT_LOGGED_IN, Domain));
                return;
            }

            bool hasAdapter = PurchaseAdapterManager.Instance.CreateIDPAdapter("iapadapter");

            if (hasAdapter == false)
            {
                GamebaseErrorNotifier.FireNotSupportedAPI(
                    this,
                    "RequestPurchase",
                    GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableReceipt>>(handle));

                return;
            }

            PurchaseAdapterManager.Instance.RequestPurchase(itemSeq, (error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                if (callback == null)
                    return;

                if (Gamebase.IsSuccess(error))
                {
                    callback(PurchaseAdapterManager.Instance.GetPurchasableReceipt(), null);
                    return;
                }

                error.domain = Domain;
                callback(null, error);
            });
        }
        
        public void RequestItemListOfNotConsumed(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RequestItemListOfNotConsumed",
                GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableReceipt>>>(handle));
        }

        public void RequestRetryTransaction(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RequestRetryTransaction",
                GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<GamebaseResponse.Purchase.PurchasableRetryTransactionResult>>(handle));
        }

        public void RequestItemListPurchasable(int handle)
        {
            var callback = GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>>>(handle);

            if (IsLoggedIn() == false)
            {
                callback(null, new GamebaseError(GamebaseErrorCode.NOT_LOGGED_IN, Domain));
                return;
            }

            bool hasAdapter = PurchaseAdapterManager.Instance.CreateIDPAdapter("iapadapter");

            if (hasAdapter == false)
            {
                GamebaseErrorNotifier.FireNotSupportedAPI(
                    this,
                    "RequestItemListPurchasable",
                    GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>>>(handle));

                return;
            }

            PurchaseAdapterManager.Instance.RequestItemListPurchasable((error) =>
            {
                GamebaseSystemPopup.Instance.ShowErrorPopup(error);

                if (callback == null)
                    return;

                if (Gamebase.IsSuccess(error))
                {
                    callback(PurchaseAdapterManager.Instance.GetPurchasableReceiptList(), null);
                    return;
                }

                error.domain = Domain;
                callback(null, error);
            });
        }
        
        public void RequestItemListAtIAPConsole(int handle)
        {
            GamebaseErrorNotifier.FireNotSupportedAPI(
                this,
                "RequestItemListAtIAPConsole",
                GamebaseCallbackHandler.GetCallback<GamebaseCallback.GamebaseDelegate<List<GamebaseResponse.Purchase.PurchasableItem>>>(handle));
        }

        public void SetStoreCode(string storeCode)
        {
            GamebaseUnitySDK.StoreCode = storeCode;
        }

        public string GetStoreCode()
        {
            return GamebaseUnitySDK.StoreCode;
        }

        private bool IsLoggedIn()
        {
            if (string.IsNullOrEmpty(Gamebase.GetUserID()))
                return false;

            return true;
        }
    }
}
#endif