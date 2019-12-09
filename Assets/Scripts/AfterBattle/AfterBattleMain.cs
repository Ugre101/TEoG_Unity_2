using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Vore;

public class AfterBattleMain : MonoBehaviour
{
    public PlayerMain player;
    public List<EnemyPrefab> enemies;
    public TextMeshProUGUI _textBox;
    public VoreHandler voreHandler;

    [SerializeField]
    private SexButton sexButton = null;

    [SerializeField]
    private VoreButton voreButton = null;

    #region Button containers

    [Header("Buttons containers")]
    [SerializeField]
    private GameObject buttons = null;

    //    [SerializeField]
    //    private GameObject DrainActions = null, MiscActions = null;

    [SerializeField]
    private GameObject drainMasc = null, drainFemi = null;

    #endregion Button containers

    #region Scene lists

    [Header("ScriptableObject Scenes")]
    public List<SexScenes> dickScenes;

    public List<SexScenes> boobScenes, mouthScenes, vaginaScenes, analScenes;
    public List<VoreScene> voreScenes;

    #endregion Scene lists

    [Header("Other")]
    public SexScenes LastScene;

    public List<SexScenes> allSexScenes;
    public GameObject Leave;
    public GameObject TakeHome;
    public Dorm dorm;
    public SexChar playerChar, enemyChar;
    private EnemyPrefab newTarget;
    public EnemyPrefab Target => newTarget != null ? newTarget : enemies[0];

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    public PlayerMain Caster => player;

    //TODO add extra for perks
    private int MaxOrgasm => 1 + Mathf.FloorToInt(player.Stats.End / 20);

    private void OnDisable()
    {
        enemies.Clear();
        SexStats.OrgasmedEvent -= RefreshScenes;
    }

    public void Setup(List<EnemyPrefab> chars)
    {
        gameObject.SetActive(true);
        enemies = chars;
        _textBox.text = null;
        LastScene = null;
        newTarget = null;
        // in future make it so several statuses spawn if team har more than one member.
        // if enemies more than one, make selector view next to status
        playerChar.Setup(player);
        enemyChar.Setup(Target);
        SexStats.OrgasmedEvent += RefreshScenes;
        player.SexStats.Reset();
        RefreshScenes();
    }

    public void RefreshScenes()
    {
        if (player.SexStats.SessionOrgasm < MaxOrgasm)
        {
            if (allSexScenes.Count == 0)
            {
                allSexScenes = dickScenes.Concat(mouthScenes).Concat(boobScenes)
                    .Concat(vaginaScenes).Concat(analScenes).ToList();
            }
            SceneChecker(buttons, allSexScenes);
            Leave.SetActive(true);
        }
        else
        {
            transform.KillChildren(buttons.transform);
        }
        if (Target.CanTake(Target.SexStats.SessionOrgasm))
        {
            TakeHome.SetActive(dorm.CanTake(Target));
        }
        if (Target.SexStats.CanDrain)
        {
            drainMasc.gameObject.SetActive(Target.CanDrainMasc);
            drainFemi.gameObject.SetActive(Target.CanDrainFemi);
        }
        else
        {
            drainFemi.gameObject.SetActive(false);
            drainMasc.gameObject.SetActive(false);
        }
    }

    private void SceneChecker(GameObject container, List<SexScenes> scenes)
    {
        transform.KillChildren(container.transform);
        foreach (SexScenes scene in scenes.FindAll(s => s.CanDo(player, Target)))
        {
            SexButton button = Instantiate(sexButton, container.transform);
            button.Setup(player, Target, this, scene);
        }
        foreach (VoreScene vore in voreScenes.FindAll(vs => vs.CanDo(player, Target)))
        {
            VoreButton btn = Instantiate(voreButton, container.transform);
            btn.Setup(player, Target, this, vore);
        }
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}