namespace Toast.Gamebase
{
    public interface IGamebaseNetwork
    {
        GamebaseNetworkType GetNetworkType();
        string GetNetworkTypeName();
        bool IsConnected();
        void IsConnected(int handle);
        bool AddOnChangedStatusListener(int handle);
        bool RemoveOnChangedStatusListener();
    }
}