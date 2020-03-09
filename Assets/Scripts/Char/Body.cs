using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BodyStat : FloatStat
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
    public void LoseFlat(float toLose) => BaseValue = Mathf.Max(BaseValue - Mathf.Abs(toLose), 0f);

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
            float diminishingReturn = (float)toChange.Duration / mod.Duration;
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

    public bool RemoveFromSource(string Source)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return false;
        }
        if (StatMods.Exists(sm => sm.Source.Equals(Source)))
        {
            foreach (StatMod sm in StatMods.FindAll(s => s.Source.Equals(Source)))
            {
                StatMods.Remove(sm);
            }
            IsDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveTempFromSource(string Source)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return false;
        }
        if (TempMods.Exists(sm => sm.Source.Equals(Source)))
        {
            foreach (TempStatMod sm in TempMods.FindAll(s => s.Source.Equals(Source)))
            {
                TempMods.Remove(sm);
            }
            IsDirty = true;
            return true;
        }
        return false;
    }

    #endregion AddRemoveMods

    public void TickTempMods()
    {
        if (TempMods.RemoveAll(tm => tm.Duration < 1) > 0)
        {
            AddedTempEvent?.Invoke();
            IsDirty = true;
        }
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
        "You are an mountain of flesh ";  // morbidly obese

        string b = MuscleLessHeight(0.18f) ? "with unnoticable muscle" :
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
}