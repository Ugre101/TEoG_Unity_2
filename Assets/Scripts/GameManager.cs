using UnityEngine;

public enum GameState
{
    Intro,
    Free,
    Menu,
    PauseMenu,
    Battle,
    InBuilding
}

public enum GlobalArea
{
    Map,
    Home
}

public static class GameManager
{
    private static GameState curState = GameState.Free;
    public static GameState LastState { get; private set; }
    public static GlobalArea CurrentArea { get; set; }
    public static bool KeyBindsActive { get; set; } = true;

    public static GameState CurState
    {
        get => curState;
        set
        {
            LastState = curState;
            switch (value)
            {
                case GameState.Intro:
                case GameState.Battle:
                case GameState.InBuilding:
                    Time.timeScale = 0f;
                    KeyBindsActive = false;
                    break;

                case GameState.PauseMenu:
                case GameState.Menu:
                    KeyBindsActive = true;
                    Time.timeScale = 0f;
                    break;

                case GameState.Free:
                default:
                    KeyBindsActive = true;
                    Time.timeScale = 1f;
                    break;
            }
            curState = value;
        }
    }

    public static GameManagerSaveState Save() => new GameManagerSaveState(CurState, CurrentArea, KeyBindsActive);

    public static void Load(GameManagerSaveState load)
    {
        CurState = load.CurState;
        CurrentArea = load.CurrentArea;
        KeyBindsActive = load.KeyBindsActive;
    }
}

[System.Serializable]
public struct GameManagerSaveState
{
    [SerializeField] private GameState curState;
    [SerializeField] private GlobalArea currentArea;
    [SerializeField] private bool keyBindsActive;

    public GameManagerSaveState(GameState curState, GlobalArea currentArea, bool keyBindsActive)
    {
        this.curState = curState;
        this.currentArea = currentArea;
        this.keyBindsActive = keyBindsActive;
    }

    public GameState CurState => curState;
    public GlobalArea CurrentArea => currentArea;
    public bool KeyBindsActive => keyBindsActive;
}