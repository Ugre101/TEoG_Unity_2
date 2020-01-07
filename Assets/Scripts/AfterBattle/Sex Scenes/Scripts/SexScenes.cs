using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Sex scene", menuName = ("Sex/Test"))]
public abstract class SexScenes : ScriptableObject
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

    [field: SerializeField] public bool IImpregnate { get; private set; } = false;
    [field: SerializeField] public bool IGetImpregnated { get; private set; } = false;

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

    public string BiggestDick(BasicChar whom) => Settings.MorInch(whom.SexualOrgans.Dicks.Biggest());
}

public abstract class LoseScene : SexScenes
{
}

public abstract class EssScene : SexScenes
{
    public virtual bool CanDo(EnemyPrefab enemyPrefab) => false;
}

public abstract class VoreScene : SexScenes
{
    public virtual bool CanDo(BasicChar player, Vore.ThePrey Other)
    {
        if (!player.Vore.Active)
        {
            return false;
        }
        return true;
    }

    public virtual string Vore(PlayerMain player, Vore.ThePrey other)
    {
        return $"";
    }
}