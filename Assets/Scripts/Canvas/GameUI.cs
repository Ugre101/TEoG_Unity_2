using System.Collections.Generic;
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

public class GameUI : MonoBehaviour
{
    private GameState curState = GameState.Free;
    private GameState lastState;

    public GameState CurState
    {
        get => curState;
        set
        {
            lastState = curState;
            Debug.Log("Last state: " + lastState);
            switch (value)
            {
                case GameState.Intro:
                case GameState.PauseMenu:
                case GameState.Home:
                case GameState.Menu:
                case GameState.Battle:
                    Time.timeScale = 0f;
                    break;

                case GameState.Free:
                default:
                    Time.timeScale = 1f;
                    break;
            }
            curState = value;
        }
    }

    private bool KeyBindsActive => CurState.Equals(GameState.Free) || CurState.Equals(GameState.Menu) || CurState.Equals(GameState.PauseMenu);

    [field: SerializeField] public KeyBindings Keys { get; private set; }

    [field: SerializeField] public GameObject Gameui { get; private set; }

    [field: SerializeField] public GameObject Battle { get; private set; }
    [field: SerializeField] public GameObject Menus { get; private set; }
    [field: SerializeField] public GameObject Buildings { get; private set; }
    [field: SerializeField] public GameObject PauseMenu { get; private set; }

    [field: SerializeField] public GameObject Home { get; private set; }

    [SerializeField]
    private MenuPanels menuPanels = new MenuPanels();

    [field: SerializeField] public CombatMain combatButtons { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(Keys.escKey.Key))
        {
            EscapePause();
        }
        else if (Input.GetKeyDown(Keys.hideAllKey.Key))
        {
            if (CurState == GameState.Free)
            {
                Gameui.SetActive(!Gameui.activeSelf);
            }
        }
        // if in menus or main game(not combat)
        if (KeyBindsActive)
        {
            if (Input.GetKeyDown(Keys.saveKey.Key))
            {
                ResumePause(menuPanels.Savemenu);
            }
            else if (Input.GetKeyDown(Keys.optionsKey.Key))
            {
                ResumePause(menuPanels.Options);
            }
            else if (Input.GetKeyDown(Keys.questKey.Key))
            {
                ResumePause(menuPanels.QuestMenu);
            }
            else if (Input.GetKeyDown(Keys.inventoryKey.Key))
            {
                ResumePause(menuPanels.Inventory);
            }
            else if (Input.GetKeyDown(Keys.voreKey.Key))
            {
                ResumePause(menuPanels.Vore);
            }
            else if (Input.GetKeyDown(Keys.essenceKey.Key))
            {
                ResumePause(menuPanels.Essence);
            }
            else if (Input.GetKeyDown(Keys.lvlKey.Key))
            {
                ResumePause(menuPanels.LevelUp);
            }
            else if (Input.GetKeyDown(Keys.lookKey.Key))
            {
                ResumePause(menuPanels.Looks);
            }
            if (Input.GetKeyUp(Keys.eventKey.Key))
            {
                BigEventLog();
            }
        }
    }

    public void Intro()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        CurState = GameState.Intro;
    }

    public void Resume()
    {
        foreach (Transform child in Menus.transform)
        {
            child.gameObject.SetActive(false);
        }
        ToggleBigPanel(Gameui);
        CurState = GameState.Free;
    }

    public void Pause()
    {
        ToggleBigPanel(Menus);
        foreach (Transform child in Menus.transform)
        {
            child.gameObject.SetActive(false);
        }
        CurState = GameState.Menu;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool BigEventLog()
    {
        if (CurState.Equals(GameState.Free))
        {
            Pause();
            menuPanels.Bigeventlog.SetActive(true);
            return true;
        }
        Resume();
        return false;
    }

    public void StartCombat(EnemyPrefab enemy)
    {
        CurState = GameState.Battle;
        foreach (Transform p in Battle.transform)
        {
            p.gameObject.SetActive(false);
        }
        ToggleBigPanel(Battle);
        List<EnemyPrefab> toAdd = new List<EnemyPrefab> { enemy };
        combatButtons.SetUpCombat(toAdd);
    }

    private void ToggleBigPanel(GameObject toActivate)
    {
        foreach (Transform bigPanel in transform)
        {
            bigPanel.gameObject.SetActive(false);
        }
        toActivate.SetActive(true);
    }

    public void ResumePause(GameObject toBeActivated)
    {
        if (CurState.Equals(GameState.Menu))
        {
            Resume();
        }
        else
        {
            Pause();
            toBeActivated.SetActive(true);
        }
    }

    public void EnterHome()
    {
        CurState = GameState.Home;
        ToggleBigPanel(Home);
        Home.transform.SleepChildren(Home.transform.GetChild(0));
    }

    public void EscapePause()
    {
        if (CurState.Equals(GameState.Menu))
        {
            Resume();
        }
        else if (CurState.Equals(GameState.PauseMenu))
        {
            if (lastState.Equals(GameState.Free) || lastState.Equals(GameState.Menu) || lastState.Equals(GameState.PauseMenu ))
            {
                Resume();
            }
            else
            {
                PauseMenu.SetActive(false);
                CurState = lastState;
            }
        }
        else
        {
            CurState = GameState.PauseMenu;
            PauseMenu.SetActive(true);
        }
    }

    public void EnterBuilding(GameObject buildingToEnter)
    {
        CurState = GameState.Home;
        ToggleBigPanel(Buildings);
        // Disable all buildings
        foreach (Transform building in Buildings.transform)
        {
            building.gameObject.SetActive(false);
        }
        buildingToEnter.SetActive(true);
    }
}