using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static bool GameIsPaused = false;

    // Main panels
    public GameObject gameui, battle, menus;

    // Battle panels
    public GameObject combat, sex, lose;
    public CombatEnemies CombatEnemies;
    private List<GameObject> _battleList = new List<GameObject>();
    // Menu panels
    public GameObject pausemenu, savemenu, options, bigeventlog;
    private List<GameObject> _menuList = new List<GameObject>();
    private void Start()
    {
        _battleList.Add(combat);
        _battleList.Add(sex);
        _battleList.Add(lose);

        _menuList.Add(pausemenu);
        _menuList.Add(savemenu);
        _menuList.Add(options);
        _menuList.Add(bigeventlog);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
               (gameui.activeSelf || menus.activeSelf))
        {
            GameIsPaused = GameIsPaused ? false : true;
            if (GameIsPaused)
            {
                ToggleBigPanel(menus);
                pausemenu.SetActive(true);
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        if (whoActive() != null)
        {
            whoActive().SetActive(false);
        }
        ToggleBigPanel(gameui);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        ToggleBigPanel(menus);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private GameObject whoActive()
    {
        if (gameui != null ? gameui.activeSelf : false)
        {
            return gameui;
        }
        else if (pausemenu != null ? pausemenu.activeSelf : false)
        {
            return pausemenu;
        }
        else if (savemenu != null ? savemenu.activeSelf : false)
        {
            return savemenu;
        }
        else if (options != null ? options.activeSelf : false)
        {
            return options;
        }
        else if (bigeventlog != null ? bigeventlog.activeSelf : false)
        {
            return bigeventlog;
        }
        else
        {
            return null;
        }
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

    public void StartCombat(BasicChar enemy)
    {
        Pause();
        foreach(GameObject p in _battleList)
        {
            p.SetActive(false);
        }
        combat.SetActive(true);
        ToggleBigPanel(battle);
        CombatEnemies.AddEnemy(enemy);
    }

    public void LeaveCombat()
    {
        Resume();
    }

    private void ToggleBigPanel(GameObject panel)
    {
        gameui.SetActive(false);
        menus.SetActive(false);
        battle.SetActive(false);
        if (panel == gameui)
        {
            gameui.SetActive(true);
        }
        else if (panel == menus)
        {
            menus.SetActive(true);
        } else if (panel == battle)
        {
            battle.SetActive(true);
        }
    }
}