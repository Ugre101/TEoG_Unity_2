using System;
using System.Collections.Generic;
using UnityEngine;

public class NonCombatSex : MonoBehaviour
{
    [SerializeField] private SexChar player = null, partner = null;
    [SerializeField] private TextLog textLog = null;
    [SerializeField] private Transform sexBtns = null, miscBtns = null, esenceBtn = null;

    public void Setup(params BasicChar[] partners)
    {
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
    private static List<BasicChar> Partners;

    public static void Setup(List<BasicChar> partners)
    {
        Partners = partners;
    }

    private static void BindSexStats(BasicChar basicChar)
    {
        //   basicChar.SexStats.OrgasmedEvent += PlayerOrgasmed;
        basicChar.SexStats.OrgasmedEvent += InvokeRefresh;
        basicChar.SexStats.OrgasmedEvent += Impreg;
        basicChar.SexStats.OrgasmedEvent += GetImpreg;
    }

    public static void UnBindSexStats(BasicChar basicChar)
    {
        basicChar.SexStats.OrgasmedEvent -= InvokeRefresh;
        basicChar.SexStats.OrgasmedEvent -= Impreg;
        basicChar.SexStats.OrgasmedEvent -= GetImpreg;
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

    private static void InvokeRefresh() => RefreshScenes?.Invoke();

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
}