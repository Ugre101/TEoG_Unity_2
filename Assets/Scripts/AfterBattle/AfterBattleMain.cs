using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattleMain : MonoBehaviour
{
    private PlayerMain player => PlayerHolder.Player;

    private List<BasicChar> enemies = new List<BasicChar>();

    [SerializeField] private TextMeshProUGUI textBox = null;

    #region Button prefabs

    [SerializeField] private SexButton sexButton = null;

    [SerializeField] private VoreButton voreButton = null;

    [SerializeField] private EssSexButton essSexButton = null;

    #endregion Button prefabs

    #region Button containers

    [Header("Buttons containers")]
    [SerializeField]
    private Transform buttons = null, DrainActions = null, MiscActions = null;

    #endregion Button containers

    #region Scenes

    [Header("ScriptableObject Scenes")]
    [SerializeField] private List<SexScenes> dickScenes = new List<SexScenes>();

    [SerializeField] private List<SexScenes> boobScenes = new List<SexScenes>(), mouthScenes = new List<SexScenes>(), vaginaScenes = new List<SexScenes>(), analScenes = new List<SexScenes>();

    [SerializeField] private List<EssScene> essScenes = new List<EssScene>();

    [SerializeField] private List<VoreScene> voreScenes = new List<VoreScene>();
    [SerializeField] private List<SexScenes> miscScenes = new List<SexScenes>();
    //   [SerializeField] private LeaveAfterBattle leaveScene = null;

    #endregion Scenes

    public SexScenes LastScene { get; set; }

    private List<SexScenes> allSexScenes = new List<SexScenes>();

    public List<SexScenes> AllSexScenes
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

    [SerializeField] private Button sortAll = null, sortMouth = null, sortAnal = null, sortDick = null, sortVagina = null, sortBreasts = null, sortVore = null;

    private BasicChar newTarget;
    public BasicChar Target => newTarget != null ? newTarget : enemies.Count > 0 ? enemies[0] : null;

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    public PlayerMain Caster => player;

    private void Start()
    {
        sortAll.onClick.AddListener(() => SceneChecker(AllSexScenes, player.Vore.Active));
        sortMouth.onClick.AddListener(() => SceneChecker(mouthScenes));
        sortAnal.onClick.AddListener(() => SceneChecker(analScenes));
        sortDick.onClick.AddListener(() => SceneChecker(dickScenes));
        sortVagina.onClick.AddListener(() => SceneChecker(vaginaScenes));
        sortBreasts.onClick.AddListener(() => SceneChecker(boobScenes));
        //  sortVore.onClick.AddListener(() => SceneChecker(voreScenes));
        TakeToDorm.TakenToDorm += EnemyRemoved;
        SexButton.PlayScene += HandleSexScene;
        EssSexButton.PlayScene += HandleEssScene;
        VoreButton.PlayerScene += HandleVoreScene;
    }

    private void EnemyRemoved()
    {
        if (newTarget == Target)
        {
            newTarget = null;
        }
        Target.SexStats.OrgasmedEvent -= RefreshScenes;
        Target.SexStats.OrgasmedEvent -= GetImpreg;
        Target.SexStats.OrgasmedEvent -= OtherOrgasmed;
        enemies.Remove(Target);
        if (enemies.Count < 1)
        {
            SexButton leaveBtn = addedMiscButtons.Find(b => b.Scene.GetType() == typeof(LeaveAfterBattle));
            if (!leaveBtn.gameObject.activeSelf)
            {
                leaveBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            RefreshScenes();
        }
    }

    private void OnDisable()
    {
        player.SexStats.OrgasmedEvent -= RefreshScenes;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= RefreshScenes);

        player.SexStats.OrgasmedEvent -= Impreg;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent -= GetImpreg);

        player.SexStats.OrgasmedEvent -= PlayerOrgasmed;
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
        playerChar.Setup(player);
        enemyChar.Setup(Target);

        BindSexstats(player);

        enemies.ForEach(e => e.SexStats.OrgasmedEvent += OtherOrgasmed);
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += RefreshScenes);
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += GetImpreg);

        player.SexStats.Reset();

        if (!buttonsIsInstatiened)
        {
            InstantiateScenes();
        }
        RefreshScenes();
    }

    private void BindSexstats(BasicChar basicChar)
    {
        basicChar.SexStats.OrgasmedEvent += PlayerOrgasmed;
        basicChar.SexStats.OrgasmedEvent += RefreshScenes;
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

        MiscActions.KillChildren();
        foreach (SexScenes sexScenes in miscScenes)
        {
            SexButton btn = Instantiate(sexButton, MiscActions);
            btn.Setup(sexScenes);
            addedMiscButtons.Add(btn);
        }

        DrainActions.KillChildren();
        foreach (EssScene essScene in essScenes)
        {
            EssSexButton btn = Instantiate(essSexButton, DrainActions);
            btn.Setup(essScene);
            addedEssSexButtons.Add(btn);
        }
    }

    public void RefreshScenes()
    {
        foreach (SexButton btn in addedSexButtons)
        {
            btn.gameObject.SetActive(player.CanOrgasmMore() ? btn.Scene.CanDo(player, Target) : false);
        }

        foreach (EssSexButton btn in addedEssSexButtons)
        {
            btn.gameObject.SetActive(Target.SexStats.CanDrain ? btn.Scene.CanDo(player, Target) : false);
        }

        foreach (VoreButton btn in addedVoreButtons)
        {
            btn.gameObject.SetActive(Settings.Vore ? btn.voreScene.CanDo(player, Target) : false);
        }

        foreach (SexButton btn in addedMiscButtons)
        {
            btn.gameObject.SetActive(btn.Scene.CanDo(player, Target));
        }
    }

    private void Impreg()
    {
        if (LastScene.IImpregnate)
        {
            if (Target.GetImpregnatedBy(Caster))
            {
                InsertToTextBox($" {Target.Identity.FirstName} got pregnant!");
            }
        }
    }

    private void GetImpreg()
    {
        if (LastScene.IGetImpregnated)
        {
            if (Caster.GetImpregnatedBy(Target))
            {
                InsertToTextBox($" You got pregnant!");
            }
        }
    }

    private void PlayerOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.PlayerOrgasmed(player, Target));

        DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
        EssenceExtension.HandleAutoGiveEssence(Caster, Target);

        string bothChanges = drainChange.BothChanges;
        if (bothChanges != string.Empty)
        {
            InsertToTextBox(bothChanges);
        }
    }

    private void OtherOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.OtherOrgasmed(player, Target));

        DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
        EssenceExtension.HandleAutoDrainEssence(Caster, Target);
        HandleTransmuteEssence();

        string bothChanges = drainChange.BothChanges;
        if (bothChanges != string.Empty)
        {
            InsertToTextBox(bothChanges);
        }
    }

    private void HandleTransmuteEssence()
    {
        if (HasPerk(PerksTypes.EssenceShaper) || HasPerk(PerksTypes.EssenceTransformer))
        {
            switch (EssenceExtension.ToggleTransmuteOption)
            {
                case EssenceExtension.TransmuteFromTo.Off:
                    break;

                case EssenceExtension.TransmuteFromTo.MascToFemi:
                    EssenceExtension.TransmuteEssenceMascToFemi(player, Target);
                    break;

                case EssenceExtension.TransmuteFromTo.FemiToMasc:
                    EssenceExtension.TransmuteEssenceFemiToMasc(player, Target);
                    break;
            }
            // TODO transmute essence
        }
        bool HasPerk(PerksTypes type) => player.Perks.HasPerk(type);
    }

    private void SceneChecker(List<SexScenes> scenes, bool showVore = false)
    {
        buttons.SleepChildren();
        foreach (SexScenes scene in scenes.FindAll(s => s.CanDo(player, Target)))
        {
            foreach (SexButton btn in addedSexButtons)
            {
                if (btn.Scene.name == scene.name)
                {
                    btn.gameObject.SetActive(btn.Scene.CanDo(player, Target));
                }
            }
        }
    }

    public void AddToTextBox(string text) => textBox.text = text;

    // TODO fix to extra info comes after
    public void InsertToTextBox(string text) => textBox.text += text;

    private void SceneBasics(SexScenes scene)
    {
        AddToTextBox(LastScene == scene ? scene.ContinueScene(player, Target) : scene.StartScene(player, Target));
        LastScene = scene;
    }

    private void HandleSexScene(SexScenes scene)
    {
        SceneBasics(scene);
        scene.ArousalGain(player, Target);
    }

    private void HandleEssScene(EssScene scene)
    {
        SceneBasics(scene);
        Target.SexStats.Drained();
        RefreshScenes();
    }

    private void HandleVoreScene(VoreScene voreScene)
    {
        AddToTextBox(voreScene.Vore(player, Target));
        LastScene = voreScene;
        Target.IfHaveHolderDestoryIt();
        EnemyRemoved();
    }

    [SerializeField] private SkillDict skillDict = null;

    private void SexSpells()
    {
        foreach (Skill s in player.Skills.SexSkills(skillDict))
        {
            // TODO INSTATIXZXE
        }
    }
}

public static class AfterBattleHandler
{
    private static List<BasicChar> enemies = new List<BasicChar>();

    // Test class to see if I can split out stuff from afterbattle to make it more like a view
    public static void AddToTextBox(string text) => SetTextLog?.Invoke(text);

    // TODO fix to extra info comes after
    public static void InsertToTextBox(string text) => AddToTextlog?.Invoke(text);

    public static Action<string> SetTextLog;
    public static Action<string> AddToTextlog;

    private static SexScenes LastScene;
    public static BasicChar Target;
    public static BasicChar newTarget;

    public static void Setup(List<BasicChar> chars)
    {
        enemies = chars;
        LastScene = null;
        newTarget = null;

        //    player.SexStats.OrgasmedEvent += PlayerOrgasmed;
        //    enemies.ForEach(e => e.SexStats.OrgasmedEvent += OtherOrgasmed);

        //    player.SexStats.OrgasmedEvent += () => NeedRefresh?.Invoke();
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += () => NeedRefresh?.Invoke());

        //    player.SexStats.OrgasmedEvent += Impreg;
        //    enemies.ForEach(e => e.SexStats.OrgasmedEvent += GetImpreg);

        //   player.SexStats.Reset();
        //    RefreshScenes();
    }

    private static void SceneBasics(SexScenes scene, PlayerMain player, BasicChar Target)
    {
        AddToTextBox(LastScene == scene ? scene.ContinueScene(player, Target) : scene.StartScene(player, Target));
        LastScene = scene;
    }

    public static void HandleSexScene(SexScenes scene, PlayerMain player, BasicChar Target)
    {
        SceneBasics(scene, player, Target);
        scene.ArousalGain(player, Target);
    }

    public static void HandleEssScene(EssScene scene, PlayerMain player, BasicChar Target)
    {
        SceneBasics(scene, player, Target);
        Target.SexStats.Drained();
        NeedRefresh?.Invoke();
    }

    public delegate void Refresh();

    public static event Refresh NeedRefresh;
}