using HealthRecovery;

public static class BasicCharExtensions
{
    public static string Race(this BasicChar who, bool capital = false) => capital
        ? who.RaceSystem.CurrentRace().ToString()
        : who.RaceSystem.CurrentRace().ToString().ToLower();

    public static string HeightMorInch(this Body body) => body.Height.Value.MorInch();

    public static string MuscleKgOrP(this Body body) => body.Muscle.Value.KgorP();

    public static string FatKgOrP(this Body body) => body.Fat.Value.KgorP();

    public static string WeightKgOrP(this Body body) => body.Weight.KgorP();

    public static string Summary(this BasicChar who)
    {
        string title = who.Identity.FullName;
        Body body = who.Body;
        string desc = $" is a {body.HeightMorInch()} tall {who.Race()} {who.Gender().ToString()}.";
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
        SexualOrgans so = basicChar.SexualOrgans;
        if (so.HaveBalls())
        {
            PregnancyBlessings pregnancyBlessings = basicChar.PregnancySystem.PregnancyBlessings;
            if (pregnancyBlessings.HasBlessing(PregnancyBlessingsIds.SpermFactory))
            {
                int blessVal = pregnancyBlessings.GetBlessingValue(PregnancyBlessingsIds.SpermFactory);
                basicChar.Body.Fat.LoseFlat(blessVal / 100); // TODO is this balanced?
                so.Balls.List.ForEach(b => b.Fluid.ReFill((so.Balls.BallsBunusRefillRate.Value + blessVal) * times));
            }
            else
            {
                so.Balls.List.ForEach(b => b.Fluid.ReFill(so.Balls.BallsBunusRefillRate.Value * times));
            }
        }
        if (so.Boobs.Lactating)
        {
            so.Boobs.List.ForEach(b => b.Fluid.ReFill(so.Boobs.BoobsBonusRefillRate.Value * times));
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
        basicChar.SexualOrgans.Anals.List.ForEach(a =>
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

    public static BasicCharCustomSave Save(this BasicChar toSave)
    {
        BasicCharCustomSave newSave =
            new BasicCharCustomSave(toSave.Identity.Save(),
                                    toSave.RelationshipTracker.Save());
        return newSave;
    }

    public static void Load(this BasicChar toLoad, BasicCharCustomSave load)
    {
        toLoad.Identity.Load(load.IdentitySave);
        if (load.ReletionShip.HasValue)
        {
            toLoad.RelationshipTracker.Load(load.ReletionShip.Value);
        }
    }
}