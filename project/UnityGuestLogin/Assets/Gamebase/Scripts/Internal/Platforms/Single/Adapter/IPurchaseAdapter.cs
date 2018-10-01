#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

using System;
using System.Collections.Generic;

namespace Toast.Gamebase.Single
{
    public interface IPurchaseAdapter
    {
        void RequestPurchase(long itemSeq, Action<GamebaseError> callback);
        void RequestItemListPurchasable(Action<GamebaseError> callback);

        GamebaseResponse.Purchase.PurchasableReceipt GetPurchasableReceipt();
        List<GamebaseResponse.Purchase.PurchasableItem> GetPurchasableReceiptList();

        void Destroy();
    }
}
#endif