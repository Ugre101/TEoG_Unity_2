using System.Collections.Generic;
using UnityEngine;

public class CanvasMain : MonoBehaviour
{
    #region Properties

    [SerializeField] private KeyBindings Keys = null;
    [SerializeField] private BigPanel Gameui = null;
    [SerializeField] private BigPanel Battle = null;
    [SerializeField] private BigPanel Menus = null;
    [SerializeField] private BigPanel Buildings = null;
    [SerializeField] private GameObject PauseMenu = null;
    [SerializeField] private HomeMain Home = null;

    [SerializeField] private MenuPanels menuPanels = new MenuPanels();

    [SerializeField] private CombatMain combatMain = null;

    #endregion Properties

    private void Update()
    {
        if (Keys.escKey.GetKeyDown())
        {
            EscapePause();
        }
        else if (Keys.hideAllKey.GetKeyDown())
        {
            if (GameManager.CurState.Equals(GameState.Free))
            {
                Gameui.gameObject.SetActive(!Gameui.gameObject.activeSelf);
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
        transform.SleepChildren(transform.GetChild(0).transform);
        GameManager.CurState = GameState.Intro;
    }

    public void Resume()
    {
        Menus.transform.SleepChildren();
        ToggleBigPanel(Gameui.gameObject);
        GameManager.CurState = GameState.Free;
    }

    public void Pause()
    {
        ToggleBigPanel(Menus.gameObject);
        Menus.transform.SleepChildren();
        GameManager.CurState = GameState.Menu;
    }

    public void QuitGame() => Application.Quit();

    public bool BigEventLog()
    {
        if (GameManager.CurState.Equals(GameState.Free))
        {
            Pause();
            menuPanels.Bigeventlog.SetActive(true);
            return true;
        }
        Resume();
        return false;
    }

    public void StartCombat(BasicChar enemy)
    {
        GameManager.CurState = GameState.Battle;
        Battle.transform.SleepChildren();
        ToggleBigPanel(Battle.gameObject);
        List<BasicChar> toAdd = new List<BasicChar> { enemy };
        combatMain.SetUpCombat(toAdd);
    }

    private void ToggleBigPanel(GameObject toActivate) => transform.SleepChildren(toActivate.transform);

    public void ResumePause(GameObject toBeActivated)
    {
        if (GameManager.CurState.Equals(GameState.Menu))
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
        GameManager.CurState = GameState.Home;
        ToggleBigPanel(Home.gameObject);
        Home.transform.SleepChildren(Home.transform.GetChild(0));
    }

    public void EscapePause()
    {
        if (GameManager.CurState.Equals(GameState.Menu))
        {
            Resume();
        }
        else if (GameManager.CurState.Equals(GameState.PauseMenu))
        {
            if (GameManager.LastState.Equals(GameState.Free) || GameManager.LastState.Equals(GameState.Menu) || GameManager.LastState.Equals(GameState.PauseMenu))
            {
                Resume();
            }
            else
            {
                PauseMenu.SetActive(false);
                GameManager.CurState = GameManager.LastState;
            }
        }
        else
        {
            GameManager.CurState = GameState.PauseMenu;
            PauseMenu.SetActive(true);
            if (Gameui.gameObject.activeSelf)
            {
                Gameui.gameObject.SetActive(false);
            }
        }
    }

    public void EnterBuilding(GameObject buildingToEnter)
    {
        GameManager.CurState = GameState.Home;
        ToggleBigPanel(Buildings.gameObject);
        // Disable all buildings
        foreach (Transform building in Buildings.transform)
        {
            building.gameObject.SetActive(false);
        }
        buildingToEnter.SetActive(true);
    }

    private void Awake()
    {
        if (thisCanvasMain == null)
        {
            thisCanvasMain = this;
        }
        else if (thisCanvasMain != this)
        {
            Destroy(gameObject);
        }
    }

    private static CanvasMain thisCanvasMain;

    public static CanvasMain GetCanvasMain
    {
        get
        {
            if (thisCanvasMain == null)
            {
                thisCanvasMain = GameObject.FindGameObjectWithTag("GameUI").GetComponent<CanvasMain>();
            }
            // might seem over kill but it's good to know if something calls getcanvas hundreds of times.
            if (Debug.isDebugBuild)
            {
                Debug.Log(new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType + " missed canvasMain");
            }
            return thisCanvasMain;
        }
    }
}