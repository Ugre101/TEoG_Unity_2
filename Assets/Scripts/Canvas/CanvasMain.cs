﻿using System.Collections.Generic;
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
    [SerializeField] private BuildingsMenu buildings = null;
    [SerializeField] private GameObject PauseMenu = null;

    [SerializeField] private MenuPanels menuPanels = new MenuPanels();

    [SerializeField] private CombatMain combatMain = null;
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
    }

    // DRY
    private void KeyDown(KeyBind key, GameObject panel)
    {
        if (key.KeyDown)
        {
            ResumePause(panel);
        }
    }

    public void Intro()
    {
        transform.SleepChildren(transform.GetChild(0));
        GameManager.SetCurState(GameState.Intro);
    }

    public void Resume()
    {
        Menus.transform.SleepChildren();
        ToggleBigPanel(Gameui.gameObject);
        GameManager.SetCurState(GameState.Free);
    }

    public void Pause()
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

    public void StartCombat(CharHolder enemy)
    {
        GameManager.SetCurState(GameState.Battle);
        Battle.transform.SleepChildren();
        ToggleBigPanel(Battle.gameObject);
        List<BasicChar> toAdd = new List<BasicChar> { enemy.BasicChar };
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



    public void EnterNpc(NpcMenuPage page)
    {
        GameManager.SetCurState(GameState.InBuilding);
        ToggleBigPanel(npcMenus.gameObject);
        npcMenus.EnterNpc(page);
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
                GameManager.SetCurState(GameManager.LastState);
            }
        }
        else
        {
            GameManager.SetCurState(GameState.PauseMenu);
            PauseMenu.SetActive(true);
            if (Gameui.gameObject.activeSelf)
            {
                Gameui.gameObject.SetActive(false);
            }
        }
    }

    public void EnterBuilding(GameObject buildingToEnter)
    {
        GameManager.SetCurState(GameState.InBuilding);
        ToggleBigPanel(Buildings.gameObject);
        // Disable all buildings expect the one to enter
        Buildings.transform.SleepChildren(buildingToEnter.transform);
    }

    public void EnterBuilding(Building building)
    {
        GameManager.SetCurState(GameState.InBuilding);
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