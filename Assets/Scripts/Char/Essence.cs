using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Essence
{
    [SerializeField] private float _amount;

    public float Amount => Mathf.Floor(_amount);
    public string StringAmount => _amount > 999 ? Math.Round(_amount / 1000, 1) + "k" : _amount.ToString();

    public Essence() => _amount = 0;

    public Essence(float parAmount) => _amount = parAmount;

    public void Gain(float toGain)
    {
        _amount += Mathf.Max(0, toGain);
        ChangeEvent?.Invoke();
        GainEvent?.Invoke();
    }

    public float Lose(float toLose)
    {
        float lose = Mathf.Min(_amount, toLose);
        _amount -= lose;
        ChangeEvent?.Invoke();
        return lose;
    }

    public delegate void Gained();

    public event Gained GainEvent;

    public delegate void EssenceSlider();

    public event EssenceSlider ChangeEvent;
}

public static class EssenceExtension
{
    private static bool HasOneOfNeededPerks(BasicChar basicChar, List<PerksTypes> perksTypes)
    {
        foreach (PerksTypes type in perksTypes)
        {
            if (HasPerk(type))
            {
                return true;
            }
        }
        return false;
        bool HasPerk(PerksTypes type) => basicChar.Perks.HasPerk(type);
    }

    public static bool CanTransmuteEssence(BasicChar basicChar)
    {
        // A bit overkill right now but it should make it easier in the long run.
        List<PerksTypes> onOffPerks = new List<PerksTypes>()
        { PerksTypes.EssenceShaper, PerksTypes.EssenceTransformer };
        return HasOneOfNeededPerks(basicChar, onOffPerks);
    }

    public static bool CanAutoDrainEssence(BasicChar basicChar)
    {
        List<PerksTypes> onOffPerks = new List<PerksTypes>() { PerksTypes.MasculineVacuum, PerksTypes.FemenineVacuum, PerksTypes.HermaphroditeVacuum };
        return HasOneOfNeededPerks(basicChar, onOffPerks);
    }

    public static bool CanAutoGiveEssence(BasicChar basicChar)
    {
        List<PerksTypes> onOffPerks = new List<PerksTypes>() { PerksTypes.FemenineFlow, PerksTypes.MasculineFlow, PerksTypes.HermaphroditeFlow };
        return HasOneOfNeededPerks(basicChar, onOffPerks);
    }

    public enum TransmuteFromTo
    {
        Off,
        MascToFemi,
        FemiToMasc,
    }

    public static TransmuteFromTo TransmuteOption { get; set; } = TransmuteFromTo.Off;

    public static TransmuteFromTo ToggleTransmuteOption => UgreTools.CycleThoughEnum(TransmuteOption);

    public static void TransmuteEssenceMascToFemi(BasicChar caster, BasicChar changed) => changed.Essence.Femi.Gain(changed.LoseMasc(TotalTransmuteAmount(caster)));

    public static void TransmuteEssenceMascToFemi(BasicChar caster) => caster.Essence.Femi.Gain(caster.LoseMasc(TotalTransmuteAmount(caster)));

    public static void TransmuteEssenceFemiToMasc(BasicChar caster, BasicChar changed) =>
        changed.Essence.Masc.Gain(changed.LoseFemi(TotalTransmuteAmount(caster)));

    public static void TransmuteEssenceFemiToMasc(BasicChar caster) =>
    caster.Essence.Masc.Gain(caster.LoseFemi(TotalTransmuteAmount(caster)));

    public static float TotalTransmuteAmount(BasicChar basicChar)
    {
        float tot = 0;
        if (basicChar.Perks.HasPerk(PerksTypes.EssenceTransformer))
        {
            tot += PerkEffects.EssenecePerks.EssShaper.TransmuteAmount(basicChar.Perks);
            tot += PerkEffects.EssenecePerks.EssTransformer.TransmuteAmount(basicChar.Perks);
        }
        return tot;
    }

    public static float TotalStableEssence(this BasicChar basicChar)
    {
        float baseStable = basicChar.Essence.StableEssence.Value;
        Perks perks = basicChar.Perks;
        if (perks.HasPerk(PerksTypes.EssenceHoarder))
        {
            baseStable += perks.GetPerkLevel(PerksTypes.EssenceHoarder) * 300;
        }
        if (perks.HasPerk(PerksTypes.EssenceShaper))
        {
            baseStable += perks.GetPerkLevel(PerksTypes.EssenceShaper) * 100;
        }
        return baseStable;
    }

    public static float GiveEssence(this BasicChar giver)
    {
        float baseGive = 0;
        Perks perks = giver.Perks;
        baseGive = addPerkVal(PerksTypes.EssenceShaper, PerkEffects.EssenecePerks.EssShaper.ExtraGive(perks));
        baseGive = addPerkVal(PerksTypes.EssenceTransformer, PerkEffects.EssenecePerks.EssTransformer.ExtrGive(perks));
        baseGive = addPerkVal(PerksTypes.MasculineFlow, PerkEffects.EssenecePerks.EssMascFlow.EssGive(perks));
        baseGive = addPerkVal(PerksTypes.FemenineFlow, PerkEffects.EssenecePerks.EssFemiFlow.EssGive(perks));
        baseGive = addPerkVal(PerksTypes.HermaphroditeFlow, PerkEffects.EssenecePerks.EssHemiFlow.EssGive(perks));
        return baseGive;

        float addPerkVal(PerksTypes type, float gain)
        {
            if (perks.HasPerk(type))
            {
                baseGive += gain;
            }
            return baseGive;
        }
    }

    public static void GiveMasc(this BasicChar giver, BasicChar reciver, bool giveAll = false)
    {
        if (giveAll)
        {
            reciver.Essence.Masc.Gain(giver.LoseMasc(giver.GiveEssence()));
        }
        else
        {
            reciver.Essence.Masc.Gain(giver.Essence.Masc.Lose(giver.GiveEssence()));
        }
    }

    public static void GiveMasc(this BasicChar giver, BasicChar reciver, float bonus, bool giveAll = false)
    {
        if (giveAll)
        {
            reciver.Essence.Masc.Gain(giver.LoseMasc(giver.GiveEssence() + bonus));
        }
        else
        {
            reciver.Essence.Masc.Gain(giver.Essence.Masc.Lose(giver.GiveEssence() + bonus));
        }
    }

    public static void GiveFemi(this BasicChar giver, BasicChar reciver, bool recyleOrgansIfNotEnough = false)
    {
        if (recyleOrgansIfNotEnough)
        {
            reciver.Essence.Femi.Gain(giver.LoseFemi(giver.GiveEssence()));
        }
        else
        {
            reciver.Essence.Femi.Gain(giver.Essence.Masc.Lose(giver.GiveEssence()));
        }
    }

    public static void GiveFemi(this BasicChar giver, BasicChar reciver, float bonus, bool recyleOrgansIfNotEnough = false)
    {
        if (recyleOrgansIfNotEnough)
        {
            reciver.Essence.Femi.Gain(giver.LoseFemi(giver.GiveEssence() + bonus));
        }
        else
        {
            reciver.Essence.Femi.Gain(giver.Essence.Masc.Lose(giver.GiveEssence() + bonus));
        }
    }

    /// <summary>Total drain amount with perks, if you gonna drain somebody use EssenceDrain instead of this </summary>
    private static float EssDrain(this BasicChar basicChar)
    {
        float baseDrain = 5f;
        Perks perks = basicChar.Perks;
        baseDrain = addPerkVal(PerksTypes.EssenceFlow, PerkEffects.EssenecePerks.EssFlow.ExtraDrain(perks));
        baseDrain = addPerkVal(PerksTypes.FemenineVacuum, PerkEffects.EssenecePerks.EssFemiVacuum.ExtraDrain(perks));
        baseDrain = addPerkVal(PerksTypes.MasculineVacuum, PerkEffects.EssenecePerks.EssMascVacuum.ExtraDrain(perks));
        baseDrain = addPerkVal(PerksTypes.HermaphroditeVacuum, PerkEffects.EssenecePerks.EssHemiVacuum.ExtraDrain(perks));
        baseDrain = addPerkVal(PerksTypes.EssenceThief, PerkEffects.EssenecePerks.EssThief.ExtraDrain(perks));
        baseDrain = addPerkVal(PerksTypes.EssenceBandit, PerkEffects.EssenecePerks.EssBandit.ExtraDrain(perks));
        return baseDrain;

        float addPerkVal(PerksTypes type, float gain)
        {
            if (perks.HasPerk(type))
            {
                baseDrain += gain;
            }
            return baseDrain;
        }
    }

    public static float EssenceDrain(this BasicChar drainer, BasicChar toDrain)
    {
        float returnVal = drainer.EssDrain();
        if (toDrain.Perks.HasPerk(PerksTypes.EssenceFlow))
        {
            returnVal += PerkEffects.EssenecePerks.EssFlow.GetExtraDrained(toDrain.Perks);
        }
        return returnVal;
    }

    public static void DrainMasc(this BasicChar drainer, BasicChar toDrain) => drainer.Essence.Masc.Gain(toDrain.LoseMasc(drainer.EssenceDrain(toDrain)));

    public static void DrainFemi(this BasicChar drainer, BasicChar toDrain) => drainer.Essence.Femi.Gain(toDrain.LoseFemi(drainer.EssenceDrain(toDrain)));

    public static bool CanDrainMasc(this BasicChar who) => who.Essence.Masc.Amount > 0 || who.SexualOrgans.HaveBalls() || who.SexualOrgans.HaveDick();

    public static bool CanDrainFemi(this BasicChar who) => who.Essence.Femi.Amount > 0 || who.SexualOrgans.HaveBoobs() || who.SexualOrgans.HaveDick();

    public static float LoseMasc(this BasicChar who, float mascToLose)
    {
        float have = who.Essence.Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            Organs so = who.SexualOrgans;
            DickContainer dicks = so.Dicks;
            List<Balls> balls = so.Balls.List;
            while (missing > fromOrgans && (so.HaveDick() || so.HaveBalls()))// have needed organs
            {
                if (so.HaveBalls() && so.HaveDick())
                {
                    if (dicks.List.Total() >= balls.Total() * 2f + 1f)
                    {
                        fromOrgans += dicks.ReCycle();
                    }
                    else
                    {
                        fromOrgans += so.Balls.ReCycle();
                    }
                }
                else if (so.HaveBalls())
                {
                    fromOrgans += so.Balls.ReCycle();
                }
                else
                {
                    fromOrgans += dicks.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                who.Essence.Masc.Gain(left);
            }
        }
        return have;
    }

    public static float LoseFemi(this BasicChar who, float femiToLose)
    {
        float have = who.Essence.Femi.Lose(femiToLose);
        float missing = femiToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            List<Vagina> vaginas = who.SexualOrgans.Vaginas.List;
            List<Boobs> boobs = who.SexualOrgans.Boobs.List;
            while (missing > fromOrgans && (vaginas.Count > 0 || boobs.Count > 0))// have needed organs
            {
                if (boobs.Count > 0 && vaginas.Count > 0)
                {
                    if (boobs.Total() >= vaginas.Total() * 2f + 1f)
                    {
                        fromOrgans += boobs.ReCycle();
                    }
                    else
                    {
                        fromOrgans += who.SexualOrgans.Vaginas.ReCycle();
                    }
                }
                else if (vaginas.Count > 0)
                {
                    fromOrgans +=who.SexualOrgans.Vaginas.ReCycle();
                }
                else
                {
                    fromOrgans += boobs.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                who.Essence.Femi.Gain(left);
            }
        }
        return have;
    }
}