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
        // string title = who.Identity.FullName;
        string desc = $"A {who.Body.HeightMorInch()} tall {who.Race()} {who.Gender.ToString()}.";
        // string stats = $"{who.Age.AgeYears}years old\nWeight: {Weight(who)}\nHeight: {Height(who)}";
        return desc;
    }

    public static string BodyStats(this BasicChar who) => $"{who.Age.AgeYears}years old\nHeight: {who.Body.HeightMorInch()}\nWeight: {who.Body.WeightKgOrP()}\nMuscle: {who.Body.MuscleKgOrP()}\nFat: {who.Body.FatKgOrP()}";

    public static void Eat(this BasicChar eater, Meal meal)
    {
        eater.HP.Gain(meal.HpGain);
        eater.WP.Gain(meal.WpGain);
        eater.Body.Fat.GainFlat(meal.FatGain);
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
    public static void OverTimeTick(this BasicChar basicChar)
    {
        basicChar.HP.TickRecovery();
        basicChar.WP.TickRecovery();
        if (basicChar.Vore.Active)
        {
            basicChar.Vore.Digest();
        }
        float fatBurnRate = 0.02f;
        if (basicChar.Perks.HasPerk(PerksTypes.Gluttony))
        {
            fatBurnRate += PerkEffects.Gluttony.ExtraFatBurn(basicChar.Perks);
        }
        else if (basicChar.Perks.HasPerk(PerksTypes.LowMetabolism))
        {
            fatBurnRate -= PerkEffects.LowMetabolism.LowerBurn(basicChar.Perks);
        }
        basicChar.Body.Fat.LoseFlat(fatBurnRate);
        ReGainFluidsTick(basicChar);
    }

    private static void ReGainFluidsTick(BasicChar basicChar)
    {
        Organs so = basicChar.SexualOrgans;
        if (so.HaveBalls())
        {
            so.Balls.ForEach(b => b.Fluid.ReFill(so.BallsBunusRefillRate.MaxValue));
        }
        if (so.Lactating)
        {
            so.Boobs.ForEach(b => b.Fluid.ReFill(so.BoobsBonusRefillRate.MaxValue));
        }
    }

    public static void GainMuscle(this BasicChar basicChar, float stimuli)
    {
        // TODO finish
        float str = basicChar.Stats.Strength.BaseValue;
        // basing on height is bad idea
        float height = basicChar.Body.Height.Value;
    }
}