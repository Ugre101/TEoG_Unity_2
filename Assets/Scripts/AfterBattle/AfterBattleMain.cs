using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfterBattleMain : MonoBehaviour
{
    public playerMain player;
    public List<EnemyPrefab> enemies;
    public TextMeshProUGUI _textBox;

    [SerializeField]
    private SexButton sexButton = null;

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

    #endregion Scene lists

    [Header("Other")]
    public SexScenes LastScene;

    public GameObject Leave;
    public GameObject TakeHome;
    public Dorm dorm;
    public SexChar playerChar, enemyChar;
    private EnemyPrefab newTarget;
    public EnemyPrefab Target => newTarget != null ? newTarget : enemies[0];

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    public playerMain Caster => player;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        enemies.Clear();
        SexStats.orgasmed -= RefreshScenes;
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
        SexStats.orgasmed += RefreshScenes;
        player.SexStats.Reset();
        RefreshScenes();
    }

    public void RefreshScenes()
    {
        if (player.SexStats.SessionOrgasm < 1)
        {
            SceneChecker(buttons,
                new List<List<SexScenes>> { dickScenes, mouthScenes, boobScenes, vaginaScenes, analScenes });
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
            drainMasc.gameObject.SetActive(Target.Essence.CanDrainMasc);
            drainFemi.gameObject.SetActive(Target.Essence.CanDrainFemi);
        }
        else
        {
            drainFemi.gameObject.SetActive(false);
            drainMasc.gameObject.SetActive(false);
        }
    }

    private void SceneChecker(GameObject container, List<List<SexScenes>> scenes)
    {
        transform.KillChildren(container.transform);
        foreach (List<SexScenes> list in scenes)
        {
            foreach (SexScenes scene in list)
            {
                if (scene.CanDo(player, Target))
                {
                    SexButton button = Instantiate(sexButton, container.transform);
                    button.Setup(player, Target, this, scene);
                }
            }
        }
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}