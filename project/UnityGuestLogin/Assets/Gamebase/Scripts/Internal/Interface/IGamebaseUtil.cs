using System.Collections.Generic;

namespace Toast.Gamebase
{
    public interface IGamebaseUtil
    {
        void ShowAlert(string title, string message);
        void ShowAlert(string title, string message, int handle);        
        void ShowToast(string message, int duration);
        void ShowToast(string message, GamebaseUIToastType type);
        void ShowAlert(Dictionary<string, string> parameters, GamebaseUtilAlertType alertType, int handle);
    }
}