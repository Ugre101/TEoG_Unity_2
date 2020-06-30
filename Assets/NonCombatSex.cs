using System;
using System.Collections.Generic;
using UnityEngine;

public class NonCombatSex : MonoBehaviour
{
    private PlayerMain player => PlayerHolder.Player;
    [SerializeField] private SexCharsHandler playerSexCharHandler = null, partnerSexCharHandler = null;
    [SerializeField] private TextLog textLog = null;
    [SerializeField] private Transform sexBtns = null, miscBtns = null, esenceBtn = null;

    public void Setup(BasicChar partner) => Setup(new List<BasicChar>() { partner });

    public void Setup(List<BasicChar> partners)
    {
        SexHander.Setup(partners);
        playerSexCharHandler.Setup(player);
        partnerSexCharHandler.Setup(partners);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}

public static class SexHander
{
    private static PlayerMain Player => PlayerHolder.Player;
    private static List<BasicChar> playerTeam;

    public static List<BasicChar> Partners { get; private set; }

    public static void Setup(List<BasicChar> partners)
    {
        SexHander.Partners = partners;
        BindSexStats(Player);
        playerTeam.ForEach(pt => BindSexStats(pt));
        SexHander.Partners.ForEach(p => BindSexStats(p));
    }

    public static void Leave()
    {
        UnBindSexStats(Player);
        playerTeam.ForEach(pt => UnBindSexStats(pt));
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

    public static BasicChar Caster => PlayerTeamTurn ? playerTeam[0] : Partners[0];
    public static BasicChar Target => PlayerTeamTurn ? Partners[0] : playerTeam[0];

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
        // Called after player action
        if (playerTeam.Count > 0)
        {
            for (int i = 0; i < playerTeam.Count; i++)
            {
                // Team member action if allowed / enabled
            }
        }
        if (Partners.Count > 0)
        {
            for (int i = 0; i < Partners.Count; i++)
            {
                // Partner does action?
            }
        }
        else
        {
            GameManager.ReturnToLastState(); // Break out as it should't be zero atm
        }
    }
}