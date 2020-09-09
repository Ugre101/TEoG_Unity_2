﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public sealed class CharStats : IntStat
{
    [SerializeField] private List<StatMod> statMods = new List<StatMod>();

    [SerializeField] private List<TempStatMod> tempMods = new List<TempStatMod>();

    protected override bool IsDirty
    {
        get => isDirty;
        set
        {
            isDirty = value;
            _ = Value;
            ValueChanged?.Invoke();
        }
    }

    public override int Value => base.Value;
    public List<StatMod> StatMods => statMods;
    public List<TempStatMod> TempMods => tempMods;

    public CharStats(int parBaseValue)
    {
        BaseValue = parBaseValue;
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

    public CharStats() : this(10)
    {
    }

    #region AddAndRemoveMods

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

    #endregion AddAndRemoveMods

    protected override int GetCalcValue()
    {
        float finalValue = BaseValue +
            StatMods.FindAll(sm => sm.ModType == ModTypes.Flat).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.ModType == ModTypes.Flat).Sum(tm => tm.Value);
        float perMulti = 1 +
            StatMods.FindAll(sm => sm.ModType == ModTypes.Precent).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.ModType == ModTypes.Precent).Sum(tm => tm.Value);
        return Mathf.FloorToInt(finalValue * perMulti);
    }

    public void TickTempMods()
    {
        if (TempMods.RemoveAll(tm => tm.Duration < 1) <= 0) return;
        AddedTempEvent?.Invoke();
        IsDirty = true;
    }

    public delegate void ValueChange();

    public event ValueChange ValueChanged;
}
