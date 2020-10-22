using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AfterBattleMain : MonoBehaviour
{
    private static BasicChar Player => PlayerMain.Player;

    private List<BasicChar> enemies = new List<BasicChar>();

    [SerializeField] private TextMeshProUGUI textBox = null;

    #region Button prefabs

    [SerializeField] private SexButton sexButton = null;

    [SerializeField] private VoreButton voreButton = null;

    [SerializeField] private EssSexButton essSexButton = null;

    #endregion Button prefabs

    #region Button containers

    [Header("Buttons containers")] [SerializeField]
    private Transform buttons = null;

    [SerializeField] private Transform drainActions = null, miscActions = null;

    #endregion Button containers

    #region Scenes

    [Header("ScriptableObject Scenes")] [SerializeField]
    private List<SexScenes> dickScenes = new List<SexScenes>();

    [SerializeField] private List<SexScenes> boobScenes = new List<SexScenes>(),
        mouthScenes = new List<SexScenes>(),
        vaginaScenes = new List<SexScenes>(),
        analScenes = new List<SexScenes>();

    [SerializeField] private List<EssScene> essScenes = new List<EssScene>();

    [SerializeField] private List<VoreScene> voreScenes = new List<VoreScene>();

    [SerializeField] private List<SexScenes> miscScenes = new List<SexScenes>();
    //   [SerializeField] private LeaveAfterBattle leaveScene = null;

    #endregion Scenes

    private SexScenes LastScene { get; set; }

    private List<SexScenes> allSexScenes = new List<SexScenes>();

    private List<SexScenes> AllSexScenes
    {
        get
        {
            if (allSexScenes.Count == 0)
            {
                allSexScenes = dickScenes.Concat(mouthScenes).Concat(boobScenes)
                    .Concat(vaginaScenes).Concat(analScenes).ToList();
            }

            return allSexScenes;
        }
    }

    [SerializeField] private SexChar playerChar = null, enemyChar = null;

    [SerializeField] private SortButton sortAll = null,
        sortMouth = null,
        sortAnal = null,
        sortDick = null,
        sortVagina = null,
        sortBreasts = null,
        sortVore = null;

    private BasicChar newTarget;
    private BasicChar Target => newTarget ?? (enemies.Count > 0 ? enemies[0] : null);

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    private BasicChar Caster => Player;
    public delegate void stuff();
    public delegate void refresh(BasicChar Caster, BasicChar Target);
    public static event stuff HideButtons;
    public static event refresh RefreshScenes;
    private void Start()
    {
        sortAll.Setup("All" ,() => SexHander.SortScenes(buttons, addedSexButtons, AllSexScenes));
        sortMouth.Setup("Mouth",() => SexHander.SortScenes(buttons, addedSexButtons, mouthScenes));
        sortAnal.Setup("Anal",() => SexHander.SortScenes(buttons, addedSexButtons, analScenes));
        sortDick.Setup("Dick",() => SexHander.SortScenes(buttons, addedSexButtons, dickScenes));
        sortVagina.Setup("Vagina",() => SexHander.SortScenes(buttons, addedSexButtons, vaginaScenes));
        sortBreasts.Setup("Breass",() => SexHander.SortScenes(buttons, addedSexButtons, boobScenes));
        //  sortVore.onClick.AddListener(() => SceneChecker(voreScenes));
        TakeToDorm.TakenToDorm += EnemyRemoved;
        SexButton.PlayScene += HandleSexScene;
        EssSexButton.PlayScene += HandleEssScene;
        VoreButton.PlayerScene += HandleVoreScene;
    }

    private SexButton leaveBtn;

    private void EnemyRemoved()
    {
        if (newTarget == Target)
        {
            newTarget = null;
        }

        Target.SexStats.OrgasmedEvent -= CallRefreshScenes;
        Target.SexStats.OrgasmedEvent -= GetImpreg;
        Target.SexStats.OrgasmedEvent -= OtherOrgasmed;
        enemies.Remove(Target);

        if (enemies.Count < 1)
        {
            SetSexScenesInactive();
            leaveBtn.gameObject.SetActive(true);
        }
        else
        {
            CallRefreshScenes();
        }
    }

    private void OnDisable()
    {
        Player.SexStats.OrgasmedEvent -= CallRefreshScenes;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= CallRefreshScenes);

        Player.SexStats.OrgasmedEvent -= Impreg;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= GetImpreg);

        Player.SexStats.OrgasmedEvent -= PlayerOrgasmed;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= OtherOrgasmed);
        enemies.Clear();
    }

    public void Setup(List<BasicChar> chars)
    {
        sortVore.gameObject.SetActive(Settings.Vore);
        gameObject.SetActive(true);
        enemies = chars;
        textBox.text = null;
        LastScene = null;
        newTarget = null;
        // in future make it so several statuses spawn if team har more than one member.
        // if enemies more than one, make selector view next to status
        playerChar.Setup(Player);
        enemyChar.Setup(Target);

        BindSexstats(Player);

        enemies.ForEach(e => e.SexStats.OrgasmedEvent += OtherOrgasmed);
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += CallRefreshScenes);
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += GetImpreg);

        Player.SexStats.Reset();

        if (!buttonsIsInstatiened)
        {
            InstantiateScenes();
            if (leaveBtn == null)
            {
                leaveBtn = addedMiscButtons.Find(b => b.Scene.GetType() == typeof(LeaveAfterBattle));
            }
        }

        CallRefreshScenes();
    }

    private void BindSexstats(BasicChar basicChar)
    {
        basicChar.SexStats.OrgasmedEvent += PlayerOrgasmed;
        basicChar.SexStats.OrgasmedEvent += CallRefreshScenes;
        basicChar.SexStats.OrgasmedEvent += Impreg;
    }

    private readonly List<SexButton> addedSexButtons = new List<SexButton>();
    private readonly List<SexButton> addedMiscButtons = new List<SexButton>();
    private readonly List<VoreButton> addedVoreButtons = new List<VoreButton>();
    private readonly List<EssSexButton> addedEssSexButtons = new List<EssSexButton>();

    private bool buttonsIsInstatiened = false;

    private void InstantiateScenes()
    {
        buttonsIsInstatiened = true;
        buttons.transform.KillChildren();
        foreach (SexScenes scene in AllSexScenes)
        {
            SexButton btn = Instantiate(sexButton, buttons.transform);
            btn.Setup(scene);
            addedSexButtons.Add(btn);
        }

        foreach (VoreScene vore in voreScenes)
        {
            VoreButton btn = Instantiate(voreButton, buttons.transform);
            btn.Setup(vore);
            addedVoreButtons.Add(btn);
        }

        miscActions.KillChildren();
        foreach (SexScenes sexScenes in miscScenes)
        {
            SexButton btn = Instantiate(sexButton, miscActions);
            btn.Setup(sexScenes);
            addedMiscButtons.Add(btn);
        }

        drainActions.KillChildren();
        foreach (EssScene essScene in essScenes)
        {
            EssSexButton btn = Instantiate(essSexButton, drainActions);
            btn.Setup(essScene);
            addedEssSexButtons.Add(btn);
        }
    }

    private void CallRefreshScenes() => RefreshScenes?.Invoke(Caster, Target);

    private void SetSexScenesInactive() => HideButtons?.Invoke();

    private void Impreg()
    {
        if (LastScene.IImpregnate && Target.GetImpregnatedBy(Caster))
        {
            InsertToTextBox($" {Target.Identity.FirstName} got pregnant!");
        }
    }

    private void GetImpreg()
    {
        if (LastScene.IGetImpregnated && Caster.GetImpregnatedBy(Target))
        {
            InsertToTextBox($" You got pregnant!");
        }
    }

    private void PlayerOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.PlayerOrgasmed(Player, Target));

        DrainChangeHandler drainChange = new DrainChangeHandler(Player, Target);
        EssenceExtension.HandleAutoGiveEssence(Caster, Target);

        string bothChanges = drainChange.BothChanges;
        if (bothChanges != string.Empty)
        {
            InsertToTextBox(bothChanges);
        }
    }

    private void OtherOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.OtherOrgasmed(Player, Target));

        DrainChangeHandler drainChange = new DrainChangeHandler(Player, Target);
        EssenceExtension.HandleAutoDrainEssence(Caster, Target);
      EssenceExtension.HandleTransmuteEssence(Caster,Target);

        string bothChanges = drainChange.BothChanges;
        if (bothChanges != string.Empty)
        {
            InsertToTextBox(bothChanges);
        }
    }


    private void AddToTextBox(string text) => textBox.text = text;

    // TODO fix to extra info comes after
    private void InsertToTextBox(string text) => textBox.text += text;

    private void SceneBasics(SexScenes scene)
    {
        AddToTextBox(LastScene == scene ? scene.ContinueScene(Player, Target) : scene.StartScene(Player, Target));
        LastScene = scene;
    }

    private void HandleSexScene(SexScenes scene)
    {
        SceneBasics(scene);
        scene.ArousalGain(Player, Target);
    }

    private void HandleEssScene(EssScene scene)
    {
        SceneBasics(scene);
        Target.SexStats.Drained();
        CallRefreshScenes();
    }

    private void HandleVoreScene(VoreScene voreScene)
    {
        AddToTextBox(voreScene.Vore(Player, Target));
        LastScene = voreScene;
        Target.IfHaveHolderDestoryIt();
        EnemyRemoved();
    }

    [SerializeField] private SkillDict skillDict = null;

    private void SexSpells()
    {
        foreach (Skill s in Player.Skills.SexSkills(skillDict))
        {
            // TODO INSTATIXZXE
        }
    }
}