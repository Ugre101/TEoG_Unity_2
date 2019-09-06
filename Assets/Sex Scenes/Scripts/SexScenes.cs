using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Sex scene", menuName = ("Sex/Test"))]
public class SexScenes : ScriptableObject
{
    public Settings settings;

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
            if (player.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBalls)
        {
            if (player.Balls.Count < 1)
            {
                return false;
            }
        }
        if (PlayerBoobs)
        {
            if (player.Boobs.Max(b => b.Size) < 3)
            {
                return false;
            }
        }
        if (PlayerVagina)
        {
            if (player.Vaginas.Count < 1)
            {
                return false;
            }
        }
        if (Dick)
        {
            if (Other.Dicks.Count < 1)
            {
                return false;
            }
        }
        if (Balls)
        {
            if (Other.Balls.Count < 1)
            {
                return false;
            }
        }
        if (Boobs)
        {
            if (Other.Boobs.Max(b => b.Size) < 3)
            {
                return false;
            }
        }
        if (Vagina)
        {
            if (Other.Vaginas.Count < 1)
            {
                return false;
            }
        }
        return true;
    }

    public virtual string StartScene(playerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual string ContinueScene(playerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual void ArousalGain(playerMain player, BasicChar other)
    {
        float PlayerGain = CasterGain;
        float OtherGain = 0;
        player.sexStats.GainArousal(PlayerGain);
        other.sexStats.GainArousal(OtherGain);
    }

    public string CmOrInch(float size)
    {
        return settings != null ? settings.MorInch(size) : size + "cm";
    }

    public string BiggestDick(BasicChar whom)
    {
        return CmOrInch(whom.Dicks.Max(d => d.Size));
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
    public virtual bool CanDo(playerMain player, BasicChar other)
    {
        return true;
    }

    public virtual void DoScene(playerMain player, BasicChar other)
    {
    }
}