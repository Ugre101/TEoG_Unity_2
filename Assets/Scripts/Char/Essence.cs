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
        EssenceSliderEvent?.Invoke();
    }

    public float Lose(float toLose)
    {
        float lose = Mathf.Min(_amount, toLose);
        _amount -= lose;
        EssenceSliderEvent?.Invoke();
        return lose;
    }

    public delegate void EssenceSlider();

    public event EssenceSlider EssenceSliderEvent;
}

public static class EssenceExtension
{
    private static TransmuteFromTo transmuteOption = TransmuteFromTo.Off;

    public enum TransmuteFromTo
    {
        Off,
        MascToFemi,
        FemiToMasc,
    }

    public static TransmuteFromTo TransmuteOption { get => transmuteOption; set => transmuteOption = Enum.IsDefined(typeof(TransmuteFromTo), value) ? value : 0; }

    public static TransmuteFromTo ToggleTransmuteOption => TransmuteOption++;

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

    public static float EssGive(this BasicChar basicChar)
    {
        float baseGive = 0;
        Perks perks = basicChar.Perks;
        baseGive = test(PerksTypes.EssenceShaper, PerkEffects.EssenecePerks.EssShaper.ExtraGive(perks));
        baseGive = test(PerksTypes.EssenceTransformer, PerkEffects.EssenecePerks.EssTransformer.ExtrGive(perks));
        baseGive = test(PerksTypes.MasculineFlow, PerkEffects.EssenecePerks.EssMascFlow.EssGive(perks));
        baseGive = test(PerksTypes.FemenineFlow, PerkEffects.EssenecePerks.EssFemiFlow.EssGive(perks));
        baseGive = test(PerksTypes.HermaphroditeFlow, PerkEffects.EssenecePerks.EssHemiFlow.EssGive(perks));
        return baseGive;

        float test(PerksTypes type, float gain)
        {
            if (perks.HasPerk(type))
            {
                baseGive += gain;
            }
            return baseGive;
        }
    }

    private static float EssDrain(this BasicChar basicChar)
    {
        float baseDrain = 5f;
        Perks perks = basicChar.Perks;
        baseDrain = test(PerksTypes.EssenceFlow, PerkEffects.EssenecePerks.EssFlow.ExtraDrain(perks));
        baseDrain = test(PerksTypes.FemenineVacuum, PerkEffects.EssenecePerks.EssFemiVacuum.ExtraDrain(perks));
        baseDrain = test(PerksTypes.MasculineVacuum, PerkEffects.EssenecePerks.EssMascVacuum.ExtraDrain(perks));
        baseDrain = test(PerksTypes.HermaphroditeVacuum, PerkEffects.EssenecePerks.EssHemiVacuum.ExtraDrain(perks));
        baseDrain = test(PerksTypes.EssenceThief, PerkEffects.EssenecePerks.EssThief.ExtraDrain(perks));
        baseDrain = test(PerksTypes.EssenceBandit, PerkEffects.EssenecePerks.EssBandit.ExtraDrain(perks));
        return baseDrain;

        float test(PerksTypes type, float gain)
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

    public static bool CanDrainMasc(this BasicChar who) => who.Essence.Masc.Amount > 0 || who.SexualOrgans.Balls.Count > 0 || who.SexualOrgans.Dicks.Count > 0;

    public static bool CanDrainFemi(this BasicChar who) => who.Essence.Femi.Amount > 0 || who.SexualOrgans.Boobs.Count > 0 || who.SexualOrgans.Dicks.Count > 0;

    public static float LoseMasc(this BasicChar who, float mascToLose)
    {
        float have = who.Essence.Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            List<Dick> dicks = who.SexualOrgans.Dicks;
            List<Balls> balls = who.SexualOrgans.Balls;
            while (missing > fromOrgans && (dicks.Count > 0 || balls.Count > 0))// have needed organs
            {
                if (balls.Count > 0 && dicks.Count > 0)
                {
                    if (dicks.Total() >= balls.Total() * 2f + 1f)
                    {
                        fromOrgans += dicks.ReCycle();
                    }
                    else
                    {
                        fromOrgans += balls.ReCycle();
                    }
                }
                else if (balls.Count > 0)
                {
                    fromOrgans += balls.ReCycle();
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
            List<Vagina> vaginas = who.SexualOrgans.Vaginas;
            List<Boobs> boobs = who.SexualOrgans.Boobs;
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
                        fromOrgans += vaginas.ReCycle();
                    }
                }
                else if (vaginas.Count > 0)
                {
                    fromOrgans += vaginas.ReCycle();
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