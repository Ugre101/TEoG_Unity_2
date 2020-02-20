using UnityEngine;

public enum GameState
{
    Intro,
    Free,
    Menu,
    PauseMenu,
    Battle,
    Home
}

public static class GameManager
{
    private static GameState curState = GameState.Free;
    public static GameState LastState { get; private set; }
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
                    Time.timeScale = 0f;
                    KeyBindsActive = false;
                    break;

                case GameState.PauseMenu:
                case GameState.Menu:
                    KeyBindsActive = true;
                    Time.timeScale = 0f;
                    break;

                case GameState.Home:
                case GameState.Free:
                default:
                    KeyBindsActive = true;
                    Time.timeScale = 1f;
                    break;
            }
            curState = value;
        }
    }
}