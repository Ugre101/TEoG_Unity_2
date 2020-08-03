using System;
using UnityEngine;

public enum GameState
{
    Intro,
    Free,
    Menu,
    PauseMenu,
    Battle,
    InBuilding,
    Event,
    Dorm,
    NonCombatSex,
}

public enum GlobalArea
{
    Map,
    Home
}

public static class GameManager
{
    public static GameState LastState { get; private set; }

    private static GlobalArea currentArea;

    public static GlobalArea CurrentArea
    {
        get => currentArea;
        private set
        {
            currentArea = value;
            GloablAreaChange?.Invoke(value);
        }
    }

    public static void SetCurrentArea(GlobalArea value) => CurrentArea = value;

    public static bool KeyBindsActive { get; set; } = true;
    
    public static GameState CurState { get; private set; } = GameState.Free;

    public static void SetCurState(GameState value)
    {
        LastState = CurState;
        switch (value)
        {
            case GameState.Event:
            case GameState.Intro:
            case GameState.Battle:
            case GameState.InBuilding:
            case GameState.Dorm:
            case GameState.NonCombatSex:
                Time.timeScale = 0f;
                KeyBindsActive = false;
                break;

            case GameState.PauseMenu:
            case GameState.Menu:
                KeyBindsActive = true;
                Time.timeScale = 0f;
                break;

            case GameState.Free:
                KeyBindsActive = true;
                Time.timeScale = 1f;
                break;

            default:
                Debug.LogError($"{value} isn't properly handled in SetcurState'a switch and got handled by default case.");
                SetCurState(GameState.Free);
                break;
        }
        CurState = value;
        GameStateChangeEvent?.Invoke(value);
    }

    public static void ReturnToLastState() => SetCurState(LastState != CurState ? LastState : GameState.Free);

    public static GameManagerSaveState Save() => new GameManagerSaveState(CurState, CurrentArea, KeyBindsActive);

    public static void Load(GameManagerSaveState load)
    {
        SetCurState(load.CurState);
        SetCurrentArea(load.CurrentArea);
        KeyBindsActive = load.KeyBindsActive;
    }

    public static Action<GameState> GameStateChangeEvent;
    public static Action<GlobalArea> GloablAreaChange;
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