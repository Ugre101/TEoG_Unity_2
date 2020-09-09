using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public sealed class BodyStat : FloatStat
{
    public BodyStat(float val)
    {
        BaseValue = val;
        DateSystem.NewHourEvent += TickTempMods;
        Save.LoadEvent += OnLoad;
        _ = Value;
        ValueChanged?.Invoke();
    }

    private void OnLoad()
    {
        IsDirty = true;
        _ = Value;
    }

    /// <summary>Max(Value - Abs(toLose),0.01f)</summary>
    public float LoseFlat(float toLose)
    {
        float lost = Mathf.Clamp(toLose, 0, BaseValue);
        BaseValue -= lost;
        return lost;
    }

    /// <summary>Value *= clamp(value, 0.01f, 1f) </summary>
    public void LosePrecent(float lostPrecent) => BaseValue *= Mathf.Clamp(lostPrecent, 0.001f, 1f);

    /// <summary>Value += Abs(toGain)</summary>
    public void GainFlat(float toGain) => BaseValue += Mathf.Abs(toGain);

    /// <summary>Value *= clamp(gainPrecent, 1.001f, infinity)</summary>
    public void GainPrecent(float gainPrecent) => BaseValue *= Mathf.Clamp(gainPrecent, 1.001f, Mathf.Infinity);

    protected override float CalcValue()
    {
        float flat = BaseValue + StatMods.FindAll(sm => sm.ModType == ModTypes.Flat).Sum(m => m.Value)
                               + TempMods.FindAll(sm => sm.ModType == ModTypes.Flat).Sum(m => m.Value);
        float precent = 1f + StatMods.FindAll(sm => sm.ModType == ModTypes.Precent).Sum(m => m.Value)
                           + TempMods.FindAll(sm => sm.ModType == ModTypes.Precent).Sum(m => m.Value);
        return flat * precent;
    }

    [SerializeField] private List<StatMod> statMods = new List<StatMod>();
    [SerializeField] private List<TempStatMod> tempStatMods = new List<TempStatMod>();
    public List<StatMod> StatMods => statMods;
    public List<TempStatMod> TempMods => tempStatMods;

    #region AddRemoveMods

    public void AddMods(StatMod mod)
    {
        StatMods.Add(mod);
        IsDirty = true;
    }

    public void AddTempMod(TempStatMod mod)
    {
        if (TempMods.Exists(tm => tm.Source.Equals(mod.Source)))
        {
            TempStatMod toChange = TempMods.Find(tm => tm.Source.Equals(mod.Source));
            float diminishingReturn = (float) toChange.Duration / mod.Duration;
            int toIncrease = Mathf.Max(0, Mathf.FloorToInt(mod.Duration / Mathf.Max(1, 2 * diminishingReturn)));
            toChange.IncreaseDuration(toIncrease);
        }
        else
        {
            // Clone otherwise diminishingReturn doesn't work as duration increase on both.
            TempMods.Add(new TempStatMod(mod.Value, mod.ModType, mod.Source, mod.Duration));
        }

        IsDirty = true;
        AddedTempEvent?.Invoke();
    }

    public delegate void DelegateAddedTemp();

    public event DelegateAddedTemp AddedTempEvent;

    public void RemoveMod(StatMod mod)
    {
        StatMods.Remove(mod);
        IsDirty = true;
    }

    public void RemoveTempMod(TempStatMod mod)
    {
        TempMods.Remove(mod);
        IsDirty = true;
    }

    public bool RemoveFromSource(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        if (!StatMods.Exists(sm => sm.Source.Equals(source))) return false;
        foreach (StatMod sm in StatMods.FindAll(s => s.Source.Equals(source)))
        {
            StatMods.Remove(sm);
        }

        IsDirty = true;
        return true;
    }

    public bool RemoveTempFromSource(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        if (!TempMods.Exists(sm => sm.Source.Equals(source))) return false;
        foreach (TempStatMod sm in TempMods.FindAll(s => s.Source.Equals(source)))
        {
            TempMods.Remove(sm);
        }

        IsDirty = true;
        return true;
    }

    #endregion AddRemoveMods

    public void TickTempMods()
    {
        if (TempMods.RemoveAll(tm => tm.Duration < 1) <= 0) return;
        AddedTempEvent?.Invoke();
        IsDirty = true;
    }

    public delegate void ValueChange();

    public event ValueChange ValueChanged;
}

[System.Serializable]
public class Body
{
    public Body(float parHeight, float parFat, float parMuscle)
    {
        height = new BodyStat(parHeight);
        fat = new BodyStat(parFat);
        muscle = new BodyStat(parMuscle);
    }

    [SerializeField] private BodyStat height, fat, muscle;

    public BodyStat Height => height;
    public BodyStat Fat => fat;

    // TODO convert muscle to a precent/factor instead of a kg val.
    public BodyStat Muscle => muscle;

    // TODO centaurs and etc need to weight more and in future maybe add diffrent settings for female body frame

    ///<summary> Bones and organs = Height * 0.15; add to that weight of Fat and Muscle </summary>
    public float Weight => Height.Value * 0.15f + Fat.Value + Muscle.Value;

    ///<summary>Body fat percentage</summary>
    public float FatPrecent => Fat.Value / Weight * 100f;

    private bool FatPerLowerThan(float parPer) => FatPrecent <= parPer;

    private bool MuscleLessHeight(float f) => Muscle.Value < Height.Value * f;

    private bool MuscleMoreHeight(float f) => Muscle.Value > Height.Value * f;

    public string Fitness()
    {
        string a = FatPerLowerThan(2f) ? "You look malnourished " :
            FatPerLowerThan(14f) ? "You have an athletic body " :
            FatPerLowerThan(18f) ? "You have a fit body " :
            FatPerLowerThan(26f) ? "You have a healthy body " :
            FatPerLowerThan(31f) ? "You have an pudgy body " :
            FatPerLowerThan(36f) ? "You have a plump body " :
            "You are an mountain of flesh "; // morbidly obese

        string b = MuscleLessHeight(0.18f) ? "with unnoticeable muscle" :
            MuscleLessHeight(0.20f) ? "with some defined muscle" :
            MuscleLessHeight(0.22f) ? "with well-defined muscle" :
            MuscleLessHeight(0.26f) ? "with bulky muscle" :
            MuscleLessHeight(0.30f) ? "with hulking muscle" :
            MuscleLessHeight(0.34f) ? "with enormous muscle" :
            "with colossal muscle"; // This is relative does a fairy ever have colossal muscle?

        string c = FatPerLowerThan(25f) ? "." :
            FatPerLowerThan(31f) && MuscleLessHeight(0.18f) ? " covered in fat." :
            FatPerLowerThan(38f) && MuscleLessHeight(0.20f) ? " buried in fat." :
            FatPerLowerThan(55f) && MuscleMoreHeight(0.22f) ? "... Otherwise, you couldn't move." :
            FatPerLowerThan(55f) && MuscleLessHeight(0.22f) ? "... Your weight is a burden to your ability to move." :
            "... No-one knows how you move.";

        return a + b + c;
    }

    public string GetHeightSynonom(bool capital = true)
    {
        float val = Height.Value;
        if (val < 10)
            return "Pixie sized";
        else if (val < 40)
            return "Gnome sized";
        else if (val < 100)
            return "Dwarf sized";
        else if (val < 160)
            return "Short sized";
        else
            return val.MorInch();
    }
}

public static class BodyExtension
{
    public static string HeightCompToRace(this BasicChar basicChar)
    {
        Races race = basicChar.RaceSystem.CurrentRace();
        float avg = AvgRaceSizes.GetAvgSize(race);
        float ratio = basicChar.Body.Height.Value / avg;
        if (ratio < 0.3f)
            return $" pixie sized among {basicChar.Race()}'s";
        else if (ratio < 0.5f)
            // height is 50cm, which is
            return $" half the height of your average {basicChar.Race()}"; // Race + "'s"
        else if (ratio < 0.7f)
            return $" short for a {basicChar.Race()}";
        else if (ratio < 0.9f)
            return $" shorter than average for a {basicChar.Race()}";
        else if (ratio < 1.1f)
            return $" average height for a {basicChar.Race()}";
        else if (ratio < 1.3f)
            return $" taller than your average {basicChar.Race()}";
        else if (ratio < 1.5f)
            return $" very tall for a {basicChar.Race()}";
        else if (ratio < 2f)
            return $" almost double the height of your average {basicChar.Race()}";
        else
            return $" a giant among {basicChar.Race()}'s";
    }

    public static class AvgRaceSizes
    {
        private const float Humanoid = 160f;
        private const float Human = 160f;
        private const float Elf = 160f;
        private const float Orc = 180f;
        private const float Troll = 185f;
        private const float Dwarf = 130f;
        private const float Halfling = 105f;
        private const float Fairy = 20f;
        private const float Incubus = 165f;
        private const float Succubus = 160f;
        private const float Equine = 200f;
        private const float Dragon = 220f;
        private const float DragonKin = 200f;
        private const float Amazon = 180f;

        public static float GetAvgSize(Races race)
        {
            switch (race)
            {
                case Races.Humanoid: return Humanoid;
                case Races.Human: return Human;
                case Races.Elf: return Elf;
                case Races.Orc: return Orc;
                case Races.Troll: return Troll;
                case Races.Dwarf: return Dwarf;
                case Races.Halfling: return Halfling;
                case Races.Fairy: return Fairy;
                case Races.Incubus: return Incubus;
                case Races.Succubus: return Succubus;
                case Races.Equine: return Equine;
                case Races.Dragon: return Dragon;
                case Races.DragonKin: return DragonKin;
                case Races.Amazon: return Amazon;
                default:
                    return 160f;
            }
        }
    }

    public static float TotalFatBurn(this BasicChar basicChar)
    {
        Body body = basicChar.Body;
        float fatBurnRate = body.Fat.BaseValue * 0.0001f;
        if (basicChar.Vore.Active)
        {
            VorePerksSystem perks = basicChar.Vore.Perks;
            if (perks.HasPerk(VorePerks.PredatoryMetabolism) && body.FatPrecent > 0.18f)
            {
                // TODO test pred metabol
                fatBurnRate += body.Fat.BaseValue * (0.0001f * perks.GetPerkLevel(VorePerks.PredatoryMetabolism)) *
                               body.FatPrecent;
            }
        }

        if (basicChar.Perks.HasPerk(PerksTypes.Gluttony))
        {
            fatBurnRate += PerkEffects.Gluttony.ExtraFatBurn(basicChar.Perks);
        }
        else if (basicChar.Perks.HasPerk(PerksTypes.LowMetabolism))
        {
            fatBurnRate -= PerkEffects.LowMetabolism.LowerBurn(basicChar.Perks);
        }

        return fatBurnRate;
    }
}