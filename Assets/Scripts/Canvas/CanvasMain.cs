using System.Collections.Generic;
using UnityEngine;

public class CanvasMain : MonoBehaviour
{
    private GameState CurState { get => GameManager.CurState; set => GameManager.CurState = value; }
    private GameState lastState => GameManager.LastState;

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
        if (Keys.escKey.GetKeyDown())
        {
            EscapePause();
        }
        else if (Keys.hideAllKey.GetKeyDown())
        {
            if (CurState.Equals(GameState.Free))
            {
                Gameui.SetActive(!Gameui.activeSelf);
            }
        }
        // if in menus or main game(not combat)
        if (GameManager.KeyBindsActive)
        {
            if (Keys.saveKey.GetKeyDown())
            {
                ResumePause(menuPanels.Savemenu);
            }
            else if (Keys.optionsKey.GetKeyDown())
            {
                ResumePause(menuPanels.Options);
            }
            else if (Keys.questKey.GetKeyDown())
            {
                ResumePause(menuPanels.QuestMenu);
            }
            else if (Keys.inventoryKey.GetKeyDown())
            {
                ResumePause(menuPanels.Inventory);
            }
            else if (Keys.voreKey.GetKeyDown())
            {
                ResumePause(menuPanels.Vore);
            }
            else if (Keys.essenceKey.GetKeyDown())
            {
                ResumePause(menuPanels.Essence);
            }
            else if (Keys.lvlKey.GetKeyDown())
            {
                ResumePause(menuPanels.LevelUp);
            }
            else if (Keys.lookKey.GetKeyDown())
            {
                ResumePause(menuPanels.Looks);
            }
            if (Keys.eventKey.GetKeyDown())
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
            if (lastState.Equals(GameState.Free) || lastState.Equals(GameState.Menu) || lastState.Equals(GameState.PauseMenu))
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