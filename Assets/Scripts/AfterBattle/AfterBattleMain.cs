using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattleMain : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private List<EnemyPrefab> enemies = new List<EnemyPrefab>();

    [SerializeField]
    private CanvasMain canvasMain = null;

    [SerializeField]
    private TextMeshProUGUI textBox = null;

    [SerializeField]
    private SexButton sexButton = null;

    [SerializeField]
    private VoreButton voreButton = null;

    [SerializeField]
    private EssSexButton essSexButton = null;

    #region Button containers

    [Header("Buttons containers")]
    [SerializeField]
    private Transform buttons = null;

    [SerializeField]
    private Transform DrainActions = null;

    //, MiscActions = null;

    #endregion Button containers

    #region Scene lists

    [Header("ScriptableObject Scenes")]
    [SerializeField]
    private List<SexScenes> dickScenes = new List<SexScenes>();

    [SerializeField]
    private List<SexScenes> boobScenes = new List<SexScenes>(), mouthScenes = new List<SexScenes>(), vaginaScenes = new List<SexScenes>(), analScenes = new List<SexScenes>();

    [SerializeField]
    private List<EssScene> essScenes = new List<EssScene>();

    [SerializeField]
    private List<VoreScene> voreScenes = new List<VoreScene>();

    #endregion Scene lists

    [Header("Other")]
    public SexScenes LastScene;

    [SerializeField]
    private List<SexScenes> allSexScenes;

    [SerializeField]
    private GameObject Leave = null;

    [SerializeField]
    private GameObject TakeHome = null;

    [SerializeField]
    private Dorm dorm = null;

    [SerializeField]
    private SexChar playerChar = null, enemyChar = null;

    [SerializeField]
    private Button sortAll = null, sortMouth = null, sortVore = null;

    private EnemyPrefab newTarget;
    public EnemyPrefab Target => newTarget != null ? newTarget : enemies[0];

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    public PlayerMain Caster => player;

    //TODO add extra for perks
    private int MaxOrgasm => 1 + Mathf.FloorToInt(player.Stats.End / 20);

    private void Start()
    {
        if (canvasMain == null) { canvasMain = CanvasMain.GetCanvasMain; }
        sortAll.onClick.AddListener(() => SceneChecker(allSexScenes, player.Vore.Active));
        sortMouth.onClick.AddListener(() => SceneChecker(mouthScenes));
        sortVore.onClick.AddListener(() => SceneChecker(player.Vore.Active));
        VoreButton.VoredEvent += Vored;
    }

    private void Vored()
    {
        if (newTarget == Target)
        {
            newTarget = null;
        }
        enemies.Remove(Target);
        if (enemies.Count < 1)
        {
            buttons.transform.KillChildren();
        }
    }

    private void OnDisable()
    {
        enemies.Clear();
        player.SexStats.OrgasmedEvent -= RefreshScenes;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= RefreshScenes);
        player.SexStats.OrgasmedEvent -= Impreg;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= GetImpreg);
    }

    public void Setup(List<EnemyPrefab> chars)
    {
        sortVore.gameObject.SetActive(player.Vore.Active);
        gameObject.SetActive(true);
        enemies = chars;
        textBox.text = null;
        LastScene = null;
        newTarget = null;
        // in future make it so several statuses spawn if team har more than one member.
        // if enemies more than one, make selector view next to status
        playerChar.Setup(player);
        player.SexStats.OrgasmedEvent += RefreshScenes;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += RefreshScenes);
        player.SexStats.OrgasmedEvent += Impreg;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += GetImpreg);

        enemyChar.Setup(Target);

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
            SceneChecker(allSexScenes, player.Vore.Active);
            Leave.SetActive(true);
        }
        else
        {
            buttons.transform.KillChildren();
        }
        if (Target.CanTake(Target.SexStats.SessionOrgasm))
        {
            TakeHome.SetActive(dorm.HasSpace);
        }
        DrainActions.KillChildren();
        if (Target.SexStats.CanDrain)
        {
            essScenes.ForEach(ess =>
            {
                if (ess.CanDo(Target))
                {
                    EssSexButton essBtn = Instantiate(essSexButton, DrainActions);
                    essBtn.Setup(this, ess);
                }
            });
        }
    }

    private void Impreg()
    {
        if (LastScene.IImpregnate)
        {
            if (Target.Impregnate(Caster))
            {
                AddToTextBox($"{Target.Identity.FirstName} got pregnant!");
            }
        }
    }

    private void GetImpreg()
    {
        if (LastScene.IGetImpregnated)
        {
            if (Caster.Impregnate(Target))
            {
                AddToTextBox($"You got pregnant!");
            }
        }
    }

    private void SceneChecker(List<SexScenes> scenes, bool showVore = false)
    {
        buttons.transform.KillChildren();
        foreach (SexScenes scene in scenes.FindAll(s => s.CanDo(player, Target)))
        {
            SexButton button = Instantiate(sexButton, buttons.transform);
            button.Setup(player, Target, this, scene);
        }
        if (showVore)
        {
            foreach (VoreScene vore in voreScenes.FindAll(vs => vs.CanDo(player, new Vore.ThePrey(Target))))
            {
                VoreButton btn = Instantiate(voreButton, buttons.transform);
                btn.Setup(player, Target, this, vore);
            }
        }
    }

    private void SceneChecker(bool showVore)
    {
        buttons.transform.KillChildren();
        if (showVore)
        {
            foreach (VoreScene vore in voreScenes.FindAll(vs => vs.CanDo(player, new Vore.ThePrey(Target))))
            {
                VoreButton btn = Instantiate(voreButton, buttons.transform);
                btn.Setup(player, Target, this, vore);
            }
        }
    }

    public void AddToTextBox(string text) => textBox.text = text;
}