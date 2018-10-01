namespace Toast.Gamebase
{
    public class GamebaseCallback
	{
        public delegate void VoidDelegate();
		public delegate void ErrorDelegate(GamebaseError error);
        public delegate void DataDelegate<T>(T data);
        public delegate void GamebaseDelegate<T>(T data, GamebaseError error);
    }
}