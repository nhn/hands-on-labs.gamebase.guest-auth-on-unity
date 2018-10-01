namespace Toast.Gamebase
{
    internal interface IGamebasePurchase
    {
        void RequestPurchase(long itemSeq, int handle);
        void RequestItemListOfNotConsumed(int handle);
        void RequestRetryTransaction(int handle);
        void RequestItemListPurchasable(int handle);
        void RequestItemListAtIAPConsole(int handle);

        void SetStoreCode(string storeCode);
        string GetStoreCode();
    }
}