using HealthRecovery;
using System.Collections;
using UnityEngine;

public static class BasicCharExtensions
{
    public static string Race(this BasicChar who, bool capital = false) => capital
        ? who.RaceSystem.CurrentRace().ToString()
        : who.RaceSystem.CurrentRace().ToString().ToLower();

    public static string HeightMorInch(this Body body) => Settings.MorInch(body.Height.Value);

    public static string MuscleKgOrP(this Body body) => Settings.KgorP(body.Muscle.Value);

    public static string FatKgOrP(this Body body) => Settings.KgorP(body.Fat.Value);

    public static string WeightKgOrP(this Body body) => Settings.KgorP(body.Weight);

    public static string Summary(this BasicChar who)
    {
        string title = who.Identity.FullName;
        Body body = who.Body;
        string desc = $" is a {body.HeightMorInch()} tall {who.Race()} {who.GetGender().ToString()}.";
        string stats = $"{who.Age.AgeYears}years old\nWeight: {body.WeightKgOrP()}\nHeight: {body.HeightMorInch()}";
        return desc;
    }

    public static string BodyStats(this BasicChar who) => $"{who.Age.AgeYears}years old\nHeight: {who.Body.HeightMorInch()}\nWeight: {who.Body.WeightKgOrP()}\nMuscle: {who.Body.MuscleKgOrP()}\nFat: {who.Body.FatKgOrP()}";

    public static void Eat(this BasicChar eater, Meal meal)
    {
        eater.HP.Gain(meal.HpGain);
        eater.WP.Gain(meal.WpGain);
        eater.GainFatAndRefillScat(meal.FatGain);
        if (meal is MealWithBuffs buffs)
        {
            if (buffs.TempMods.Count > 0)
            {
                eater.Stats.AddTempMods(buffs.TempMods);
            }
            if (buffs.TempHealthMods.Count > 0)
            {
                buffs.TempHealthMods.ForEach(m =>
                {
                    if (m.HealthType == HealthTypes.Health)
                    {
                        eater.HP.AddTempMod(m);
                    }
                    else if (m.HealthType == HealthTypes.WillPower)
                    {
                        eater.WP.AddTempMod(m);
                    }
                });
            }
        }
    }

    public static IEnumerator TickEverySecond(BasicChar basicChar)
    {
        // Time.time is affected by timescale so no pause check is needed
        float time = Time.time;
        while (true)
        {
            if (time + 1f < Time.time)
            {
                time = Time.time;
                OverTimeTick(basicChar);
            }
            yield return null;
        }
    }

    /// <summary> Handles hp/wp recovery, fat burn, vore </summary>
    public static void OverTimeTick(this BasicChar basicChar, int times = 1)
    {
        if (!basicChar.HP.IsMax)
        {
            basicChar.HP.Gain(basicChar.HpRecoveryTotal(times));
        }
        if (!basicChar.WP.IsMax)
        {
            basicChar.WP.Gain(basicChar.WpRecoveryTotal(times));
        }
        if (basicChar.Vore.Active)
        {
            basicChar.Vore.Digest(times);
        }
        Body body = basicChar.Body;
        // TODO Fix fatburn
        BodyStat fat = body.Fat;
        float fatBurnRate = basicChar.TotalFatBurn();
        fat.LoseFlat(fatBurnRate * times);

        basicChar.ReGainFluidsTick(times);
    }

    private static void ReGainFluidsTick(this BasicChar basicChar, int times)
    {
        Organs so = basicChar.SexualOrgans;
        if (so.HaveBalls())
        {
            PregnancyBlessings pregnancyBlessings = basicChar.PregnancySystem.PregnancyBlessings;
            if (pregnancyBlessings.HasBlessing(PregnancyBlessingsIds.SpermFactory))
            {
                int blessVal = pregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.SpermFactory);
                basicChar.Body.Fat.LoseFlat(blessVal / 100); // TODO is this balanced?
                so.Balls.ForEach(b => b.Fluid.ReFill((so.BallsBunusRefillRate.Value + blessVal) * times));
            }
            else
            {
                so.Balls.ForEach(b => b.Fluid.ReFill(so.BallsBunusRefillRate.Value * times));
            }
        }
        if (so.Lactating)
        {
            so.Boobs.ForEach(b => b.Fluid.ReFill(so.BoobsBonusRefillRate.Value * times));
        }
    }

    public static void GainMuscle(this BasicChar basicChar, float stimuli)
    {
        // TODO finish
        float str = basicChar.Stats.Strength.BaseValue;
        // basing on height is bad idea
        float height = basicChar.Body.Height.Value;
    }

    public static void GainFatAndRefillScat(this BasicChar basicChar, float fatGain, float scatRatio = 0.1f)
    {
        basicChar.Body.Fat.GainFlat(fatGain);
        basicChar.SexualOrgans.Anals.ForEach(a =>
        {
            if (!a.Fluid.IsFull)
            {
                a.Fluid.ReFillWith(fatGain * scatRatio);
            }
            else
            {
                // TODO need to shit
            }
        });
    }
}