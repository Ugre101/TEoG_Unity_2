﻿using System.Linq;
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

    public virtual bool CanDo(ThePrey player, ThePrey Other)
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

    public virtual string StartScene(PlayerMain player, ThePrey other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual string ContinueScene(PlayerMain player, ThePrey other)
    {
        ArousalGain(player, other);
        return $"";
    }

    public virtual void ArousalGain(PlayerMain player, ThePrey other)
    {
        float PlayerGain = CasterGain * Mathf.Pow(EnduranceMultiplier, player.Stats.End);
        float OtherGain = ReciverGain * Mathf.Pow(CharmMultiplier, player.Stats.Cha) *
            Mathf.Pow(EnduranceMultiplier, other.Stats.End);
        player.SexStats.GainArousal(PlayerGain);
        other.SexStats.GainArousal(OtherGain);
    }

    public string BiggestDick(ThePrey whom) => Settings.MorInch(whom.SexualOrgans.Dicks.Biggest());
}

public class VoreScene : SexScenes
{
    public override bool CanDo(ThePrey player, ThePrey Other)
    {
        if (!player.Vore.Active)
        {
            return false;
        }
        return base.CanDo(player, Other);
    }

    public virtual string Vore(PlayerMain player, ThePrey other)
    {
        return $"";
    }
}