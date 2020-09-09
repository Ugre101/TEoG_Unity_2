using HealthRecovery;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HealthTypes
{
    Health,
    WillPower
}

[System.Serializable]
public class Health : IntStat
{
    [SerializeField] private float current;
    [SerializeField] private List<HealthMod> healthMods = new List<HealthMod>();
    [SerializeField] private List<TempHealthMod> tempHealthMods = new List<TempHealthMod>();
    [SerializeField] private Recovery recovery = new Recovery();
    public List<HealthMod> HealthMods => healthMods;

    public List<TempHealthMod> TempHealthMods => tempHealthMods;
    public Recovery Recovery => recovery;

    public sealed override int Value
    {
        get
        {
            if (!IsDirty) return lastValue;
            
            lastValue = GetCalcValue();
            IsDirty = false;
            UpdateSliderEvent?.Invoke();
            current = Mathf.Clamp(current, 0, lastValue);
            return lastValue;
        }
    }

    protected override bool IsDirty
    {
        get => isDirty;
        set
        {
            isDirty = value;
            if (value)
            {
                _ = Value;
            }
        }
    }

    public bool IsMax => current >= Value;

    private readonly List<AffectedByStat> AffectedBy = new List<AffectedByStat>();

    public void TickTempMods()
    {
        if (TempHealthMods.RemoveAll(tm => tm.Duration < 1) <= 0) return;
        AddedTempEvent?.Invoke();
        IsDirty = true;
    }

    protected override int GetCalcValue()
    {
        float flatValue = BaseValue +
            HealthMods.FindAll(hm => hm.ModType == ModTypes.Flat).Sum(hm => hm.Value) +
            TempHealthMods.FindAll(thm => thm.ModType == ModTypes.Flat).Sum(thm => thm.Value) +
            AffectedBy.Sum(ab => basicChar.Stats.GetStat(ab.Stat).Value * ab.Multiplier);
        float perValue = 1 +
            HealthMods.FindAll(hm => hm.ModType == ModTypes.Precent).Sum(hm => hm.Value) +
            TempHealthMods.FindAll(thm => thm.ModType == ModTypes.Precent).Sum(thm => thm.Value);
        return Mathf.FloorToInt(flatValue * perValue);
    }

    private readonly BasicChar basicChar;

    public Health(BasicChar basicChar)
    {
        this.basicChar = basicChar;
        baseValue = 100;
        DateSystem.NewHourEvent += TickTempMods;
    }

    public Health(BasicChar basicChar, List<AffectedByStat> affectedBy) : this(basicChar)
    {
        this.AffectedBy = affectedBy;
        this.AffectedBy.ForEach(ab => basicChar.Stats.GetStat(ab.Stat).ValueChanged += () => IsDirty = true);
        current = Value;
    }

    public Health(BasicChar basicChar, AffectedByStat affectedBy) : this(basicChar, new List<AffectedByStat>() { affectedBy })
    {
    }

    public bool TakeDmg(float dmg)
    {
        current = Mathf.Max(0, current - dmg);
        UpdateSliderEvent?.Invoke();
        if (!(current <= 0)) return false;
        DeadEvent?.Invoke();
        return true;
    }

    public void Gain(float gain)
    {
        current += Mathf.Clamp(gain, 0, Value - current);
        UpdateSliderEvent?.Invoke();
    }

    public void FullGain() => current = Value;
    /// <summary> Set health to max by precent, min: 0f and max: 1f </summary>
    public void SetToPercent(float percent) => current = Value * Mathf.Clamp(percent, 0, 1);
    public float SliderValue => current / Value;

    public string Status => current < 999 ?
        $"{Mathf.FloorToInt(current)} / {Value}"
        : $"{Math.Round(current / 1000, 1)}k / {Math.Round((float)Value / 1000, 1)}k";

    public delegate void UpdateSlider();

    public event UpdateSlider UpdateSliderEvent;

    public delegate void Dead();

    public event Dead DeadEvent;

    public void ManualSliderUpdate() => UpdateSliderEvent?.Invoke();

    #region AddAndRemoveMods

    public void AddMods(HealthMod mod)
    {
        HealthMods.Add(mod);
        IsDirty = true;
    }

    public void AddTempMod(TempHealthMod mod)
    {
        if (TempHealthMods.Exists(tm => tm.Source.Equals(mod.Source)))
        {
            TempHealthMod toChange = TempHealthMods.Find(tm => tm.Source.Equals(mod.Source));
            float diminishingReturn = (float)toChange.Duration / (float)mod.Duration;
            int toIncrease = Mathf.Max(0, Mathf.FloorToInt(mod.Duration / Mathf.Max(1, 2 * diminishingReturn)));
            toChange.IncreaseDuration(toIncrease);
        }
        else
        {
            // Clone otherwise diminishingReturn doesn't work as duration increase on both.
            TempHealthMods.Add(new TempHealthMod(mod.Value, mod.ModType, mod.HealthType, mod.Source, mod.Duration));
        }
        IsDirty = true;
        AddedTempEvent?.Invoke();
    }

    public delegate void DelegateAddedTemp();

    public event DelegateAddedTemp AddedTempEvent;

    public void RemoveMods(HealthMod mod)
    {
        HealthMods.Remove(mod);
        IsDirty = true;
    }

    public void RemoveTempMods(TempHealthMod mod)
    {
        TempHealthMods.Remove(mod);
        IsDirty = true;
        AddedTempEvent?.Invoke();
    }

    public bool RemoveFromSource(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return false;
        }

        if (!HealthMods.Exists(sm => sm.Source.Equals(source))) return false;
        
        foreach (HealthMod sm in HealthMods.FindAll(s => s.Source.Equals(source)))
        {
            HealthMods.Remove(sm);
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

        if (!TempHealthMods.Exists(sm => sm.Source.Equals(source))) return false;
        
        foreach (TempHealthMod sm in TempHealthMods.FindAll(s => s.Source.Equals(source)))
        {
            TempHealthMods.Remove(sm);
        }

        IsDirty = true;
        AddedTempEvent?.Invoke();
        return true;
    }

    #endregion AddAndRemoveMods
}

public class AffectedByStat
{
    public AffectedByStat(StatTypes stat, float multiplier)
    {
        this.Stat = stat;
        this.Multiplier = multiplier;
    }

    public readonly StatTypes Stat;
    public float Multiplier { get; }
}