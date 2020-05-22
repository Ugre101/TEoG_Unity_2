using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CanvasMain : MonoBehaviour
{
    private static CanvasMain thisCanvasMain;

    public static CanvasMain GetCanvasMain
    {
        get
        {
            if (thisCanvasMain == null)
            {
                thisCanvasMain = GameObject.FindGameObjectWithTag("GameUI").GetComponent<CanvasMain>();
                Debug.LogError("Something tried to call canvasmain before it could awake");
            }
            // might seem over kill but it's good to know if something calls getcanvas hundreds of times.
            if (Debug.isDebugBuild)
            {
                Debug.Log(new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType + " missed canvasMain");
            }
            return thisCanvasMain;
        }
    }

    #region Properties

    [SerializeField] private BigPanel Gameui = null;
    [SerializeField] private BigPanel Battle = null;
    [SerializeField] private BigPanel Menus = null;
    [SerializeField] private BigPanel Buildings = null;
    [SerializeField] private Buildings buildings = null;
    [SerializeField] private GameObject PauseMenu = null;
    [SerializeField] private HomeMain Home = null;

    [SerializeField] private MenuPanels menuPanels = new MenuPanels();

    [SerializeField] private CombatMain combatMain = null;

    #endregion Properties

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

    private void Start() => Movement.TriggerEnemy += StartCombat;

    private void Update()
    {
        if (KeyBindings.EscKey.KeyDown)
        {
            EscapePause();
        }
        else if (KeyBindings.HideAllKey.KeyDown)
        {
            if (GameManager.CurState.Equals(GameState.Free))
            {
                Gameui.gameObject.SetActive(!Gameui.gameObject.activeSelf);
            }
        }
        // if in menus or main game(not combat)
        if (GameManager.KeyBindsActive)
        {
            if (KeyDown(KeyBindings.SaveKey, menuPanels.Savemenu))
            {
            }
            else if (KeyDown(KeyBindings.OptionsKey, menuPanels.Options))
            {
            }
            else if (KeyDown(KeyBindings.QuestKey, menuPanels.QuestMenu))
            {
            }
            else if (KeyDown(KeyBindings.InventoryKey, menuPanels.Inventory))
            {
            }
            else if (KeyDown(KeyBindings.VoreKey, menuPanels.Vore))
            {
            }
            else if (KeyDown(KeyBindings.EssenceKey, menuPanels.Essence))
            {
            }
            else if (KeyDown(KeyBindings.LvlKey, menuPanels.LevelUp))
            {
            }
            else if (KeyDown(KeyBindings.LookKey, menuPanels.Looks))
            {
            }
            if (KeyBindings.EventKey.KeyDown)
            {
                BigEventLog();
            }
        }
    }

    // DRY
    private bool KeyDown(KeyBind key, GameObject panel)
    {
        bool keyDown = key.KeyDown;
        if (keyDown)
        {
            ResumePause(panel);
        }
        return keyDown;
    }

    public void Intro()
    {
        transform.SleepChildren(transform.GetChild(0));
        GameManager.CurState = GameState.Intro;
    }

    public void Resume()
    {
        if (GameManager.CurrentArea == GlobalArea.Home)
        {
            Menus.transform.SleepChildren();
            ToggleBigPanel(new List<Transform>() { Home.transform, Gameui.transform });
            GameManager.CurState = GameState.Free;
        }
        else
        {
            Menus.transform.SleepChildren();
            ToggleBigPanel(Gameui.gameObject);
            GameManager.CurState = GameState.Free;
        }
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

    private void ToggleBigPanel(List<Transform> toActivate) => transform.SleepChildren(toActivate);

    private GameObject activeGameObject = null;

    public void ResumePause(GameObject toBeActivated)
    {
        if (GameManager.CurState.Equals(GameState.Menu) && (activeGameObject != null ? activeGameObject.GetInstanceID() == toBeActivated.GetInstanceID() : true))
        {
            Resume();
        }
        else
        {
            Pause();
            toBeActivated.SetActive(true);
            activeGameObject = toBeActivated;
        }
    }

    [SerializeField] private MapEvents mapEvents = null;
    [SerializeField] private HomeMapHandler homeMapHandler = null;
    [SerializeField] private Tilemap homeLandPlatform = null;

    public void EnterHome()
    {
        GameManager.CurState = GameState.Free;
        GameManager.CurrentArea = GlobalArea.Home;

        ToggleBigPanel(new List<Transform>() { Home.transform, Gameui.transform });
        Home.transform.SleepChildren(Home.transform.GetChild(0));
        if (homeLandPlatform == null)
        {
            mapEvents.Teleport(WorldMaps.Home, homeMapHandler.GetActiveLawn);
        }
        else
        {
            mapEvents.Teleport(WorldMaps.Home, homeMapHandler.GetActiveLawn, homeLandPlatform);
        }
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
        GameManager.CurState = GameState.InBuilding;
        ToggleBigPanel(Buildings.gameObject);
        // Disable all buildings expect the one to enter
        Buildings.transform.SleepChildren(buildingToEnter.transform);
    }

    public void EnterBuilding(Building building)
    {
        GameManager.CurState = GameState.InBuilding;
        transform.SleepChildren();
        buildings.EnterBuilding(building);
    }

    [SerializeField] private GameObject teleportMenu = null;

    public void TeleportMenu() => EnterBuilding(teleportMenu);

    /// <summary> Hides gameUI and returns if gameUi was active before </summary>
    /// <returns></returns>
    public bool HideGameUI()
    {
        bool currState = Gameui.gameObject.activeSelf;
        Gameui.gameObject.SetActive(false);
        return currState;
    }

    public void ShowGameUI()
    {
        if (GameManager.CurState == GameState.Free)
        {
            Gameui.gameObject.SetActive(true);
        }
    }
}