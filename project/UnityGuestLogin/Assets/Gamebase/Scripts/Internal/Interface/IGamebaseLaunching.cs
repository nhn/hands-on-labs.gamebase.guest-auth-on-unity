namespace Toast.Gamebase
{
    public interface IGamebaseLaunching
    {
        bool AddUpdateStatusListener(int handle);
        bool RemoveUpdateStatusListener(int handle);
        GamebaseResponse.Launching.LaunchingInfo GetLaunchingInformations();
        int GetLaunchingStatus();
    }
}