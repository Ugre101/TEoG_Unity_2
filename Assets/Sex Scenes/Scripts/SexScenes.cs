using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Sex scene", menuName = ("Sex/Test"))]
public class SexScenes : ScriptableObject
{
    [Header("Player needs")]
    public bool PlayerDick;

    public bool PlayerBalls;
    public bool PlayerBoobs;
    public bool PlayerVagina;

    [Header("Other needs")]
    public bool Dick;

    public bool Balls;
    public bool Boobs;
    public bool Vagina;

    [Tooltip("These values get multied by char stats")]
    [Header("Base arousal gain")]
    public int CasterGain;

    public int ReciverGain;
    public int EnduranceMultiplier = 1;
    public int CharmMultiplier = 1;
    public int StrengthMultiplier = 1;

    public virtual bool CanDo(BasicChar player, BasicChar Other)
    {
        if (PlayerDick)
        {
            if (player.SexualOrgans.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBalls)
        {
            if (player.SexualOrgans.Balls.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBoobs)
        {
            if (player.SexualOrgans.Boobs.Count > 0 ? player.SexualOrgans.Boobs.Max(b => b.Size) < 3 : false)
            {
                return false;
            }
        }
        if (PlayerVagina)
        {
            if (player.SexualOrgans.Vaginas.Count < 1)
            {
                return false;
            }
        }
        if (Dick)
        {
            if (Other.SexualOrgans.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (Balls)
        {
            if (Other.SexualOrgans.Balls.Count < 1)
            {
                return false;
            }
        }
        if (Boobs)
        {
            if (Other.SexualOrgans.Boobs.Count > 0 ? Other.SexualOrgans.Boobs.Max(b => b.Size) < 3 : false)
            {
                return false;
            }
        }
        if (Vagina)
        {
            if (Other.SexualOrgans.Vaginas.Count < 1)
            {
                return false;
            }
        }
        return true;
    }

    public virtual string StartScene(PlayerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual string ContinueScene(PlayerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual void ArousalGain(PlayerMain player, BasicChar other)
    {
        float PlayerGain = CasterGain * Mathf.Pow(EnduranceMultiplier, player.Stats.End);
        float OtherGain = ReciverGain * Mathf.Pow(CharmMultiplier, player.Stats.Cha) *
            Mathf.Pow(EnduranceMultiplier, other.Stats.End);
        player.SexStats.GainArousal(PlayerGain);
        other.SexStats.GainArousal(OtherGain);
    }

    public string CmOrInch(float size)
    {
        return Settings.MorInch(size);
    }

    public string BiggestDick(BasicChar whom)
    {
        return CmOrInch(whom.SexualOrgans.Dicks.Max(d => d.Size));
    }
}

public class VoreScene : SexScenes
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (!player.Vore.Active)
        {
            return false;
        }
        return base.CanDo(player, Other);
    }
}

public class MonoScene : MonoBehaviour
{
    public virtual bool CanDo(PlayerMain player, BasicChar other)
    {
        return true;
    }

    public virtual void DoScene(PlayerMain player, BasicChar other)
    {
    }
}