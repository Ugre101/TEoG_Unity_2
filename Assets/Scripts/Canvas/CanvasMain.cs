using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private BigPanel Menus = null;

    [SerializeField] private MenuPanels menuPanels = new MenuPanels();

    [SerializeField] private EventMain eventMain = null;
    [SerializeField] private NpcMenus npcMenus = null;

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
        if (EventMain.GetEventMain == null)
        {
            eventMain.gameObject.SetActive(true);
        }
        GameManager.GameStateChangeEvent += CloseOnStateChange;
    }

    private void Start()
    {
    }

    private void CloseOnStateChange(GameState newState)
    {
        if (GameManager.LastState == GameState.Menu && newState != GameState.Menu)
        {
            transform.SleepChildren();
        }
    }

    private void Update()
    {
        if (KeyBindings.EscKey.KeyDown)
        {
            EscapePause();
        }

        // if in menus or main game(not combat)
        if (!GameManager.KeyBindsActive) return;
        
        KeyDown(KeyBindings.SaveKey, menuPanels.Savemenu);
        KeyDown(KeyBindings.OptionsKey, menuPanels.Options);
        KeyDown(KeyBindings.QuestKey, menuPanels.QuestMenu);
        KeyDown(KeyBindings.InventoryKey, menuPanels.Inventory);
        KeyDown(KeyBindings.VoreKey, menuPanels.Vore);
        KeyDown(KeyBindings.EssenceKey, menuPanels.Essence);
        KeyDown(KeyBindings.LvlKey, menuPanels.LevelUp);
        KeyDown(KeyBindings.LookKey, menuPanels.Looks);
        if (KeyBindings.EventKey.KeyDown)
        {
            BigEventLog();
        }
    }

    // DRY
    private void KeyDown(KeyBind key, GameObject panel)
    {
        if (key.KeyDown)
        {
            ResumePause(panel);
        }
    }

    public void Resume()
    {
        Menus.transform.SleepChildren();
        transform.SleepChildren();
        //     ToggleBigPanel(Gameui.gameObject);
        GameManager.SetCurState(GameState.Free);
    }

    private void Pause()
    {
        ToggleBigPanel(Menus.gameObject);
        Menus.transform.SleepChildren();
        GameManager.SetCurState(GameState.Menu);
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

    private void ToggleBigPanel(GameObject toActivate) => transform.SleepChildren(toActivate.transform);

    private void ToggleBigPanel(List<Transform> toActivate) => transform.SleepChildren(toActivate);

    private GameObject activeGameObject = null;

    public void ResumePause(GameObject toBeActivated)
    {
        if (GameManager.CurState.Equals(GameState.Menu) && (activeGameObject == null || activeGameObject.GetInstanceID() == toBeActivated.GetInstanceID()))
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

    public void EnterNpc(NpcMenuPage page)
    {
        GameManager.SetCurState(GameState.InBuilding);
        ToggleBigPanel(npcMenus.gameObject);
        npcMenus.EnterNpc(page);
    }

    public void EscapePause()  // TODO Clean up
    {
        switch (GameManager.CurState)
        {
            case GameState.Menu:
            case GameState.PauseMenu when GameManager.LastState == GameState.Free || GameManager.LastState == GameState.Menu || GameManager.LastState == GameState.PauseMenu:
                Resume();
                break;
            case GameState.PauseMenu:
                GameManager.ReturnToLastState();
                break;
            default:
                GameManager.SetCurState(GameState.PauseMenu);
                break;
        }
    }
}