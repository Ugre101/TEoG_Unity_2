using System.Collections;
using UnityEngine;

public static class BasicCharExtensions
{
    public static string Height(this BasicChar who) => Settings.MorInch(who.Body.Height.Value);

    public static string Weight(this BasicChar who) => Settings.KgorP(who.Body.Weight);

    public static string Summary(this BasicChar who)
    {
        // string title = who.Identity.FullName;
        string desc = $"A {who.Height()} tall {who.Race} {who.Gender.ToString()}.";
        // string stats = $"{who.Age.AgeYears}years old\nWeight: {Weight(who)}\nHeight: {Height(who)}";
        return desc;
    }

    public static string BodyStats(this BasicChar who)
    {
        return $"{who.Age.AgeYears}years old\nHeight: {who.Height()}\nWeight: {who.Weight()}\nMuscle: {Settings.KgorP(who.Body.Muscle.Value)}\nFat: {Settings.KgorP(who.Body.Fat.Value)}";
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
    private static void OverTimeTick(BasicChar basicChar)
    {
        Gain(basicChar.HP);
        Gain(basicChar.WP);
        if (basicChar.Vore.Active)
        {
            basicChar.Vore.Digest();
        }
        float fatBurnRate = 0.01f;
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

        void Gain(Health health)
        {
            if (!health.IsMax)
            {
                float gain = basicChar.RestRate;
                if (basicChar.Perks.HasPerk(PerksTypes.Gluttony))
                {
                    gain += PerkEffects.Gluttony.ExtraRecovery(basicChar.Perks);
                }
                health.Gain(gain);
            }
        }
    }

    private static void ReGainFluidsTick(BasicChar basicChar)
    {
        Organs so = basicChar.SexualOrgans;
        if (so.Balls.Count > 0)
        {
            foreach (Balls ball in so.Balls)
            {
                ball.Fluid.ReFill();
            }
        }
        if (so.Lactating)
        {
            foreach (Boobs boob in so.Boobs)
            {
                boob.Fluid.ReFill();
            }
        }
    }
}