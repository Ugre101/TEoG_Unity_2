using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public KeyBindings keys;

    [Header("Main panels")]
    public GameObject gameui;

    public GameObject battle;
    public GameObject menus;

    [Header("Battle panels")]
    public GameObject combat;

    public GameObject sex;
    public GameObject lose;
    public GameObject home;

    [Header("Options, Save ,etc")]
    public GameObject pausemenu;

    public GameObject savemenu;
    public GameObject options;
    public GameObject bigeventlog;
    public GameObject questMenu;
    public GameObject inventory;
    public GameObject vore;
    public GameObject essence;
    public GameObject levelUp;
    public GameObject looks;

    [Header("Eventlog stuff")]
    public GameObject openEventlog;

    public GameObject closedEventlog;

    [Space]
    public CombatMain combatButtons;
    public AfterBattleMain afterBattleActions;
    private float _eventTime;

    private void Update()
    {
        if (Input.GetKeyDown(keys.escKey))
        {
            EscapePause();
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameIsPaused)
            {
                gameui.SetActive(!gameui.activeSelf);
            }
        }
        // if in menus or main game(not combat)
        if (gameui.activeSelf || menus.activeSelf)
        {
            if (Input.GetKeyDown(keys.saveKey))
            {
                ResumePause(savemenu);
            }
            else if (Input.GetKeyDown(keys.optionsKey))
            {
                ResumePause(options);
            }
            else if (Input.GetKeyDown(keys.questKey))
            {
                ResumePause(questMenu);
            }
            else if (Input.GetKeyDown(keys.inventoryKey))
            {
                ResumePause(inventory);
            }
            else if (Input.GetKeyDown(keys.voreKey))
            {
                ResumePause(vore);
            }
            else if (Input.GetKeyDown(keys.essenceKey))
            {
                ResumePause(essence);
            }
            else if (Input.GetKeyDown(keys.lvlKey))
            {
                ResumePause(levelUp);
            }
            else if (Input.GetKeyDown(keys.eventKey))
            {
                _eventTime = Time.time;
            }
            else if (Input.GetKeyDown(keys.lookKey))
            {
                ResumePause(looks);
            }
            if (Input.GetKeyUp(keys.eventKey))
            {
                if (Time.time - _eventTime > 0.8f)
                {
                    openEventlog.SetActive(openEventlog.activeSelf ? false : true);
                    closedEventlog.SetActive(closedEventlog.activeSelf ? false : true);
                }
                else if (bigEventLog()) { }
            }
        }
    }

    public void Resume()
    {
        foreach (Transform child in menus.transform)
        {
            child.gameObject.SetActive(false);
        }
        ToggleBigPanel(BigPanels.GameUI);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        ToggleBigPanel(BigPanels.Menus);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool bigEventLog()
    {
        if (gameui.activeSelf)
        {
            Pause();
            bigeventlog.SetActive(true);
            return true;
        }
        else
        {
            Resume();
            return false;
        }
    }

    public void StartCombat(EnemyPrefab enemy)
    {
        Pause();
        foreach (Transform p in battle.transform)
        {
            p.gameObject.SetActive(false);
        }
        // combat.SetActive(true);
        ToggleBigPanel(BigPanels.Battle);
        // combatButtons.enemyTeamChars.Clear();
        // combatButtons.enemyTeamChars.Add(enemy);
        List<EnemyPrefab> toAdd = new List<EnemyPrefab>{enemy};
        combatButtons.SetUpCombat(toAdd);
        // or add range if more that one
    }

    public void LeaveCombat()
    {
        Resume();
    }

    public enum BigPanels
    {
        GameUI,
        Menus,
        Battle,
        Buildings,
        Home
    }

    private void ToggleBigPanel(BigPanels panel)
    {
        foreach (Transform bigPanel in transform)
        {
            bigPanel.gameObject.SetActive(false);
        }
        switch (panel)
        {
            case BigPanels.Battle:
                battle.SetActive(true);
                break;

            case BigPanels.Buildings:
                break;

            case BigPanels.GameUI:
                gameui.SetActive(true);
                break;

            case BigPanels.Menus:
                menus.SetActive(true);
                break;

            case BigPanels.Home:
                home.SetActive(true);
                break;
        }
    }

    public void ResumePause(GameObject toBeActivated)
    {
        if (GameIsPaused)
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
        ToggleBigPanel(BigPanels.Home);
        foreach (Transform child in home.transform)
        {
            child.gameObject.SetActive(false);
        }
        home.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LeaveHome()
    {
        Resume();
    }

    public void EscapePause()
    {
        if (menus.activeSelf)
        {
            Resume();
        }
        else if (pausemenu.activeSelf)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            pausemenu.SetActive(false);
        }
        else
        {
            GameIsPaused = true;
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
        }
    }
}