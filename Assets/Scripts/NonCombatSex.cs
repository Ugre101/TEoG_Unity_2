using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseSexMonoBehavior : MonoBehaviour
{
    [SerializeField] protected TextLog textLog = null;
    [SerializeField] private SexCharsHandler playerSexCharHandler = null, partnerSexCharHandler = null;
    [SerializeField] protected SexButton sexButton = null;
    [SerializeField] protected VoreButton voreButton = null;
    [SerializeField] protected EssSexButton essSexButton = null;
    [SerializeField] protected Transform buttons = null, MiscActions = null, DrainActions = null;

    [Header("ScriptableObject Scenes")]
    [SerializeField] private List<SexScenes> dickScenes = new List<SexScenes>();

    [SerializeField] private List<SexScenes> boobScenes = new List<SexScenes>(), mouthScenes = new List<SexScenes>(), vaginaScenes = new List<SexScenes>(), analScenes = new List<SexScenes>();
    [SerializeField] private List<EssScene> essScenes = new List<EssScene>();
    [SerializeField] private List<VoreScene> voreScenes = new List<VoreScene>();
    [SerializeField] private List<SexScenes> miscScenes = new List<SexScenes>();
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

    protected BasicChar player => PlayerMain.Player;

    protected bool firstTimeUsed = true;

    protected readonly List<SexButton> addedSexButtons = new List<SexButton>();
    protected readonly List<SexButton> addedMiscButtons = new List<SexButton>();
    protected readonly List<VoreButton> addedVoreButtons = new List<VoreButton>();
    protected readonly List<EssSexButton> addedEssSexButtons = new List<EssSexButton>();

    public virtual void Start()
    {
        //     TakeToDorm.TakenToDorm += EnemyRemoved;
        SexButton.PlayScene += SexHander.HandleSexScene;
        //   EssSexButton.PlayScene += HandleEssScene;
        //   VoreButton.PlayerScene += HandleVoreScene;
    }

    public virtual void Setup(List<BasicChar> partners)
    {
        if (firstTimeUsed)
        {
            InstantiateScenes();
        }
        SexHander.Setup(partners);

        playerSexCharHandler.Setup(player);
        partnerSexCharHandler.Setup(SexHander.Partners);
        SexHander.SetText += textLog.SetText;
        SexHander.AddText += textLog.AddText;
    }

    protected virtual void InstantiateScenes()
    {
        firstTimeUsed = false;
        buttons.transform.KillChildren();
        InstantiateAListOfScenes(AllSexScenes, buttons, addedSexButtons);

        foreach (VoreScene vore in voreScenes)
        {
            VoreButton btn = Instantiate(voreButton, buttons.transform);
            btn.Setup(vore);
            addedVoreButtons.Add(btn);
        }

        MiscActions.KillChildren();
        InstantiateAListOfScenes(miscScenes, MiscActions, addedMiscButtons);

        DrainActions.KillChildren();
        foreach (EssScene essScene in essScenes)
        {
            EssSexButton btn = Instantiate(essSexButton, DrainActions);
            btn.Setup(essScene);
            addedEssSexButtons.Add(btn);
        }
    }

    protected virtual void InstantiateAListOfScenes(List<SexScenes> test, Transform temp, List<SexButton> addedBtns)
    {
        foreach (SexScenes scene in test)
        {
            SexButton btn = Instantiate(sexButton, temp);
            btn.Setup(scene);
            addedBtns.Add(btn);
        }
    }

    protected virtual void OnDisable()
    {
        SexHander.SetEventsToNull();
    }
}

public static class SexHander
{
    private static List<BasicChar> PlayerTeam { get; set; }

    public static List<BasicChar> Partners { get; private set; }

    public static void Setup(List<BasicChar> partners)
    {
        PlayerTeam = new List<BasicChar>() { PlayerMain.Player };
        Partners = partners;
        PlayerTeam.ForEach(pt => BindSexStats(pt));
        Partners.ForEach(p => BindSexStats(p));
    }

    public static void Leave()
    {
        PlayerTeam.ForEach(pt => UnBindSexStats(pt));
        Partners.ForEach(p => UnBindSexStats(p));
        SetEventsToNull();
    }

    private static void BindSexStats(BasicChar basicChar)
    {
        basicChar.SexStats.OrgasmedEvent += CasterOrgasmed;
        basicChar.SexStats.OrgasmedEvent += TargetOrgasmed;
    }

    public static void UnBindSexStats(BasicChar basicChar)
    {
        basicChar.SexStats.OrgasmedEvent -= CasterOrgasmed;
        basicChar.SexStats.OrgasmedEvent -= TargetOrgasmed;
    }

    public static void SetEventsToNull()
    {
        RefreshScenes = null;
        AddText = null;
        SetText = null;
    }

    public static void Setup(BasicChar partner) => Setup(new List<BasicChar>() { partner });

    public static void SetTextLog(string text) => SetText?.Invoke(text);

    public delegate void Refresh();

    public static event Refresh RefreshScenes;

    public static Action<string> SetText;

    public static void AddToTextLog(string text) => AddText?.Invoke(text);

    public static Action<string> AddText;

    public static SexScenes LastScene { get; private set; }
    public static bool PlayerTeamTurn = true;

    public static BasicChar Caster => PlayerTeamTurn ? PlayerTeam[0] : Partners[0];
    public static BasicChar Target => PlayerTeamTurn ? Partners[0] : PlayerTeam[0];

    private static void Impreg()
    {
        if (LastScene.IImpregnate)
        {
            if (Target.GetImpregnatedBy(Caster))
            {
                AddText($" {Caster.Identity.FirstName} impregnated {Target.Identity.FirstName}!");
            }
        }
    }

    private static void GetImpreg()
    {
        if (LastScene.IGetImpregnated)
        {
            if (Caster.GetImpregnatedBy(Target))
            {
                AddText($" {Target.Identity.FirstName} got {Caster.HimHerSelf()} pregnant by {Target.Identity.FirstName}!");
            }
        }
    }

    private static void CasterOrgasmed()
    {
        EssenceExtension.HandleAutoGiveEssence(Caster, Target);
        Impreg();
        RefreshScenes?.Invoke();
    }

    private static void TargetOrgasmed()
    {
        EssenceExtension.HandleAutoDrainEssence(Caster, Target);
        GetImpreg();
        RefreshScenes?.Invoke();
    }

    private static void TurnManager()
    {
        if (Partners.Count == 0)
        {
            GameManager.ReturnToLastState(); // Break out as it should't be zero atm
        }

        // Called after player action
        if (PlayerTeam.Count > 0)
        {
            for (int i = 0; i < PlayerTeam.Count; i++)
            {
                // Team member action if allowed / enabled
            }
        }

        for (int i = 0; i < Partners.Count; i++)
        {
            // Partner does action?
        }
    }

    public static void SortScenes(Transform allScenesConatiner, List<SexButton> addedSexButtons, List<SexScenes> scenes)
    {
        allScenesConatiner.SleepChildren();
        foreach (SexScenes scene in scenes.FindAll(s => s.CanDo(Caster, Target)))
        {
            SexButton btn = addedSexButtons.Find(sb => sb.Scene.name == scene.name);
            btn.gameObject.SetActive(Caster.CanOrgasmMore() ? btn.Scene.CanDo(Caster, Target) : false);
        }
    }

    public static void SceneBasics(SexScenes scene)
    {
        SetText(LastScene == scene ? scene.ContinueScene(Caster, Target) : scene.StartScene(Caster, Target));
        LastScene = scene;
    }

    public static void HandleSexScene(SexScenes scene)
    {
        SceneBasics(scene);
        scene.ArousalGain(Caster, Target);
    }
}