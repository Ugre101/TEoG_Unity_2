using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Sex scene", menuName = ("Sex/Test"))]
public abstract class SexScenes : ScriptableObject
{
    [Header("Player needs/use")]
    [SerializeField] protected bool PlayerDick;

    [SerializeField] protected bool PlayerBalls, PlayerBoobs, PlayerVagina, playerAnal, playerHands, playerMouth;

    [Header("Other needs/use")]
    [SerializeField] protected bool Dick;

    [SerializeField] protected bool Balls, Boobs, Vagina, otherAnal, otherHands, otherMouth;

    [Tooltip("These values get multied by char stats")]
    [Header("Base arousal gain")]
    public int CasterGain;

    [SerializeField] private List<StatMultiplier> casterStatMultipliers = new List<StatMultiplier>();
    public int ReciverGain;
    [SerializeField] private List<StatMultiplier> reciverStatMultipliers = new List<StatMultiplier>();
    public int EnduranceMultiplier = 1;
    public int CharmMultiplier = 1;
    public int StrengthMultiplier = 1;

    [field: SerializeField] public bool IImpregnate { get; private set; } = false;
    [field: SerializeField] public bool IGetImpregnated { get; private set; } = false;

    public virtual bool CanDo(BasicChar player, BasicChar Other)
    {
        if (PlayerDick)
        {
            if (!player.SexualOrgans.HaveDick())
            {
                return false;
            }
        }
        if (PlayerBalls)
        {
            if (!player.SexualOrgans.HaveBalls())
            {
                return false;
            }
        }
        if (PlayerBoobs)
        {
            if (!player.SexualOrgans.HaveBoobs(3))
            {
                return false;
            }
        }
        if (PlayerVagina)
        {
            if (!player.SexualOrgans.HaveVagina())
            {
                return false;
            }
        }
        if (Dick)
        {
            if (!Other.SexualOrgans.HaveDick())
            {
                return false;
            }
        }
        if (Balls)
        {
            if (!Other.SexualOrgans.HaveBalls())
            {
                return false;
            }
        }
        if (Boobs)
        {
            if (!Other.SexualOrgans.HaveBoobs(3))
            {
                return false;
            }
        }
        if (Vagina)
        {
            if (!Other.SexualOrgans.HaveVagina())
            {
                return false;
            }
        }
        return true;
    }

    public virtual string StartScene(PlayerMain player, BasicChar other) => $"";

    public virtual string ContinueScene(PlayerMain player, BasicChar other) => $"";

    public virtual string PlayerOrgasmed(PlayerMain player, BasicChar other) => "You orgasmed";

    public virtual string OtherOrgasmed(PlayerMain player, BasicChar other) => "They orgasmed";

    public virtual void ArousalGain(PlayerMain player, BasicChar other)
    {
        float PlayerGain = CasterGain * Mathf.Pow(EnduranceMultiplier, player.Stats.End);
        float OtherGain = ReciverGain * Mathf.Pow(CharmMultiplier, player.Stats.Cha) *
            Mathf.Pow(EnduranceMultiplier, other.Stats.End);

        SexStats pSexStats = player.SexStats;
        SexStats oSexStats = other.SexStats;
        pSexStats.GainArousal(PlayerGain);
        oSexStats.GainArousal(OtherGain);
        // Gain Exp
        List<OrganAndItsExp> playerOrgansExp = new List<OrganAndItsExp>() { new OrganAndItsExp(PlayerDick, pSexStats.DickExp), new OrganAndItsExp(PlayerVagina, pSexStats.VagExp), new OrganAndItsExp(PlayerBoobs, pSexStats.BreastsExp),
            new OrganAndItsExp(playerAnal, pSexStats.AnalExp), new OrganAndItsExp(playerHands, pSexStats.HandExp), new OrganAndItsExp(playerMouth, pSexStats.MouthExp) };
        int playerExp = SexExpTotal(playerOrgansExp);
        foreach (OrganAndItsExp organ in playerOrgansExp)
        {
            GainSexExp(organ.UseOrgan, organ.OrganExp);
        }

        List<OrganAndItsExp> otherOrgansExp = new List<OrganAndItsExp>() {new OrganAndItsExp(Dick,oSexStats.DickExp), new OrganAndItsExp(Vagina,oSexStats.VagExp), new OrganAndItsExp(Boobs,oSexStats.BreastsExp),
        new OrganAndItsExp(otherAnal,oSexStats.AnalExp),new OrganAndItsExp(otherHands,oSexStats.HandExp),new OrganAndItsExp(otherMouth,oSexStats.MouthExp)};
        int otherExp = SexExpTotal(otherOrgansExp);
        foreach (OrganAndItsExp organ in otherOrgansExp)
        {
            GainSexExp(organ.UseOrgan, organ.OrganExp);
        }

        int sexExpDiff = playerExp - otherExp;
    }

    private void GainSexExp(bool organ, ExpSystem expSystem)
    {
        if (organ)
        {
            expSystem.GainExp(Random.Range(1, 5));
        }
    }

    private int SexExpTotal(List<OrganAndItsExp> organs)
    {
        int tot = 0;
        foreach (OrganAndItsExp organ in organs)
        {
            if (organ.UseOrgan)
            {
                tot += organ.OrganExp.Level;
            }
        }
        return tot;
    }

    public virtual float CasterMulti(BasicChar caster)
    {
        float multi = 1f;
        foreach (StatMultiplier multiplier in casterStatMultipliers)
        {
            multi += caster.Stats.GetStat(multiplier.Stat).Value * multiplier.Multiplier;
        }
        return multi;
    }

    public virtual float ReciverMulti(BasicChar reciver)
    {
        float multi = 1f;
        foreach (StatMultiplier multiplier in reciverStatMultipliers)
        {
            multi += reciver.Stats.GetStat(multiplier.Stat).Value * multiplier.Multiplier;
        }
        return multi;
    }


    private class OrganAndItsExp
    {
        public OrganAndItsExp(bool useOrgan, ExpSystem organExp)
        {
            UseOrgan = useOrgan;
            OrganExp = organExp;
        }

        public bool UseOrgan { get; }
        public ExpSystem OrganExp { get; }
    }
}

public abstract class LoseScene : SexScenes
{
}

public abstract class EssScene : SexScenes
{
    public virtual bool CanDo(BasicChar basicChar) => false;
}

public abstract class VoreScene : SexScenes
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (!player.Vore.Active)
        {
            return false;
        }
        return true;
    }

    public virtual string Vore(PlayerMain player, BasicChar other)
    {
        return $"";
    }
}
