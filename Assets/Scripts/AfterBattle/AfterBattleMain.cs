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
    [SerializeField] private LeaveAfterBattle leaveScene = null;

    #endregion Scenes

    public SexScenes LastScene { get; set; }

    private List<SexScenes> allSexScenes = new List<SexScenes>();

    [SerializeField] private SexChar playerChar = null, enemyChar = null;

    [SerializeField] private Button sortAll = null, sortMouth = null, sortAnal = null, sortDick = null, sortVagina = null, sortBreasts = null, sortVore = null;

    private BasicChar newTarget;
    public BasicChar Target => newTarget != null ? newTarget : enemies.Count > 0 ? enemies[0] : null;

    // this only exist to make it easier in future if I want to add say teammates who can have scenes or something
    public PlayerMain Caster => player;

    private int MaxOrgasm => player.MaxOrgasm();

    private void Start()
    {
        sortAll.onClick.AddListener(() => SceneChecker(allSexScenes, player.Vore.Active));
        sortMouth.onClick.AddListener(() => SceneChecker(mouthScenes));
        sortAnal.onClick.AddListener(() => SceneChecker(analScenes));
        sortDick.onClick.AddListener(() => SceneChecker(dickScenes));
        sortVagina.onClick.AddListener(() => SceneChecker(vaginaScenes));
        sortBreasts.onClick.AddListener(() => SceneChecker(boobScenes));
        sortVore.onClick.AddListener(ShowVore);
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
            buttons.transform.KillChildren();
            DrainActions.transform.KillChildren();
            if (leaveScene != null)
            {
                MiscActions.transform.KillChildren();
                Instantiate(sexButton, MiscActions).Setup(leaveScene);
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

        player.SexStats.OrgasmedEvent += PlayerOrgasmed;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += OtherOrgasmed);

        player.SexStats.OrgasmedEvent += RefreshScenes;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += RefreshScenes);

        player.SexStats.OrgasmedEvent += Impreg;
        enemies.ForEach(e => e.SexStats.OrgasmedEvent += GetImpreg);

        player.SexStats.Reset();
        RefreshScenes();
    }

    public void RefreshScenes()
    {
        if (allSexScenes.Count == 0)
        {
            allSexScenes = dickScenes.Concat(mouthScenes).Concat(boobScenes)
                .Concat(vaginaScenes).Concat(analScenes).ToList();
        }
        SceneChecker(allSexScenes, player.Vore.Active);

        MiscActions.KillChildren();
        foreach (SexScenes sexScenes in miscScenes.FindAll(m => m.CanDo(player, Target)))
        {
            Instantiate(sexButton, MiscActions).Setup(sexScenes);
        }
        DrainActions.KillChildren();
        if (Target.SexStats.CanDrain)
        {
            essScenes.FindAll(ess => ess.CanDo(Target)).ForEach(ess => Instantiate(essSexButton, DrainActions).Setup(ess));
        }
    }

    private void Impreg()
    {
        if (LastScene.IImpregnate)
        {
            if (Target.Impregnate(Caster))
            {
                InsertToTextBox($" {Target.Identity.FirstName} got pregnant!");
            }
        }
    }

    private void GetImpreg()
    {
        if (LastScene.IGetImpregnated)
        {
            if (Caster.Impregnate(Target))
            {
                InsertToTextBox($" You got pregnant!");
            }
        }
    }

    private void PlayerOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.PlayerOrgasmed(player, Target));
        HandleAutoGiveEssence();
        if (player.Perks.HasPerk(PerksTypes.EssenceShaper) || player.Perks.HasPerk(PerksTypes.EssenceTransformer))
        {
            // TODO transmute essence
        }
    }

    private void OtherOrgasmed()
    {
        InsertToTextBox("\n\n" + LastScene.OtherOrgasmed(player, Target));
        HandleAutoDrainEssence();
    }

    private void HandleAutoDrainEssence()
    {
        if ((player.Perks.HasPerk(PerksTypes.FemenineVacuum) && player.Perks.HasPerk(PerksTypes.MasculineVacuum)) || player.Perks.HasPerk(PerksTypes.HermaphroditeVacuum))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            player.DrainFemi(Target);
            player.DrainMasc(Target);
            InsertToTextBox(drainChange.BothChanges);
        }
        else if (player.Perks.HasPerk(PerksTypes.FemenineVacuum))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            player.DrainFemi(Target);
            InsertToTextBox(drainChange.BothChanges);
        }
        else if (player.Perks.HasPerk(PerksTypes.MasculineVacuum))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            player.DrainMasc(Target);
            InsertToTextBox(drainChange.BothChanges);
        }
    }

    private void HandleAutoGiveEssence()
    {
        if ((player.Perks.HasPerk(PerksTypes.FemenineFlow) && player.Perks.HasPerk(PerksTypes.MasculineFlow)) || player.Perks.HasPerk(PerksTypes.HermaphroditeFlow))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            float bonus = PerkEffects.EssenecePerks.EssFemiFlow.EssGiveBonus(player.Perks) + PerkEffects.EssenecePerks.EssMascFlow.EssGiveBonus(player.Perks) + PerkEffects.EssenecePerks.EssHemiFlow.EssGiveBonus(player.Perks);
            player.GiveFemi(Target, bonus, true);
            player.GiveMasc(Target, bonus, true);
            player.SexStats.Drained();
            InsertToTextBox(drainChange.BothChanges);
        }
        else if (player.Perks.HasPerk(PerksTypes.FemenineFlow))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            float bonus = PerkEffects.EssenecePerks.EssFemiFlow.EssGiveBonus(player.Perks);
            player.GiveFemi(Target, bonus, true);
            player.SexStats.Drained();
            InsertToTextBox(drainChange.BothChanges);
        }
        else if (player.Perks.HasPerk(PerksTypes.MasculineFlow))
        {
            DrainChangeHandler drainChange = new DrainChangeHandler(player, Target);
            float bonus = PerkEffects.EssenecePerks.EssMascFlow.EssGiveBonus(player.Perks);
            player.GiveMasc(Target, bonus, true);
            player.SexStats.Drained();
            InsertToTextBox(drainChange.BothChanges);
        }
    }

    private void SceneChecker(List<SexScenes> scenes, bool showVore = false)
    {
        buttons.transform.KillChildren();
        if (player.CanOrgasmMore())
        {
            foreach (SexScenes scene in scenes.FindAll(s => s.CanDo(player, Target)))
            {
                Instantiate(sexButton, buttons.transform).Setup(scene);
            }
        }
        if (showVore)
        {
            AddVoresScenes();
        }
    }

    private void AddVoresScenes()
    {
        foreach (VoreScene vore in voreScenes.FindAll(vs => vs.CanDo(player, Target)))
        {
            Instantiate(voreButton, buttons.transform).Setup(vore);
        }
    }

    private void ShowVore()
    {
        buttons.transform.KillChildren();
        foreach (VoreScene vore in voreScenes.FindAll(vs => vs.CanDo(player, Target)))
        {
            Instantiate(voreButton, buttons.transform).Setup(vore);
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