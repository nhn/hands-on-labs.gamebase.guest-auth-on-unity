#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

using System;
using System.Collections.Generic;

namespace Toast.Gamebase.Single
{
    public class PurchaseAdapterManager
    {
        private static readonly PurchaseAdapterManager instance = new PurchaseAdapterManager();

        public static PurchaseAdapterManager Instance
        {
            get { return instance; }
        }

        public IPurchaseAdapter adapter;

        public bool CreateIDPAdapter(string moduleName)
        {
            if (adapter != null)
                return true;

            adapter = AdapterFactory.CreateAdapter<IPurchaseAdapter>(moduleName);

            if (adapter == null)
                return false;
            else
                return true;
        }

        public void RequestPurchase(long itemSeq, Action<GamebaseError> callback)
        {
            if (adapter == null)
            {
                callback(new GamebaseError(GamebaseErrorCode.PURCHASE_UNKNOWN_ERROR, message: GamebaseStrings.PURCHASE_ADAPTER_NOT_FOUND));
                return;
            }

            adapter.RequestPurchase(itemSeq, callback);
        }

        public void RequestItemListPurchasable(Action<GamebaseError> callback)
        {
            if (adapter == null)
            {
                callback(new GamebaseError(GamebaseErrorCode.PURCHASE_UNKNOWN_ERROR, message: GamebaseStrings.PURCHASE_ADAPTER_NOT_FOUND));
                return;
            }

            adapter.RequestItemListPurchasable(callback);
        }

        public GamebaseResponse.Purchase.PurchasableReceipt GetPurchasableReceipt()
        {
            if (adapter == null)
            {
                GamebaseLog.Warn(GamebaseStrings.PURCHASE_ADAPTER_NOT_FOUND, this, "GetPurchasableReceipt");
                return null;
            }

            return adapter.GetPurchasableReceipt();
        }

        public List<GamebaseResponse.Purchase.PurchasableItem> GetPurchasableReceiptList()
        {
            if (adapter == null)
            {
                GamebaseLog.Warn(GamebaseStrings.PURCHASE_ADAPTER_NOT_FOUND, this, "GetPurchasableReceiptList");
                return null;
            }

            return adapter.GetPurchasableReceiptList();
        }

        public void Destroy()
        {
            if (adapter == null)
                return;

            adapter.Destroy();
        }
    }
}
#endif