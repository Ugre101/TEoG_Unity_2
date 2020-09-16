namespace StartMenuStuff
{
    public static class StartMenuMananger
    {
        public delegate void ManangerEvents();

        public static event ManangerEvents NewGame, LoadGame;

        public static void StartNewGame() => NewGame?.Invoke();

        public static void StartLoadGame() => LoadGame?.Invoke();
    }
}