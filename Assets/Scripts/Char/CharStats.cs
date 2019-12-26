using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharStats
{
    [SerializeField]
    private int baseValue;

    private float _currValue;

    private bool IsDirty { get; set; } = true;

    public float Value
    {
        get
        {
            if (IsDirty)
            {
                _currValue = CalcFinalValue();
                IsDirty = false;
            }
            return _currValue;
        }
    }

    [field: SerializeField] public List<StatMod> StatMods { get; private set; } = new List<StatMod>();
    [field: SerializeField] public List<TempStatMod> TempMods { get; private set; } = new List<TempStatMod>();

    public int BaseValue { get => baseValue; set { baseValue = value; IsDirty = true; } }

    public CharStats()
    {
        BaseValue = 10;
        DateSystem.NewHourEvent += TickTempMods;
    }

    public CharStats(int parBaseValue)
    {
        BaseValue = parBaseValue;
        DateSystem.NewHourEvent += TickTempMods;
    }

    public void AddMods(StatMod mod)
    {
        IsDirty = true;
        StatMods.Add(mod);
    }

    public void AddTempMod(TempStatMod mod)
    {
        IsDirty = true;
        if (TempMods.Exists(tm => tm.Source.Equals(mod.Source)))
        {
            TempStatMod toChange = TempMods.Find(tm => tm.Source.Equals(mod.Source));
            float diminishingReturn = (float)toChange.Duration / (float)mod.Duration;
            int toIncrease = Mathf.Max(0, Mathf.FloorToInt(mod.Duration / Mathf.Max(1, 2 * diminishingReturn)));
            toChange.IncreaseDuration(toIncrease);
        }
        else
        {
            // Clone otherwise diminishingReturn doesn't work as duration increase on both.
            TempMods.Add(new TempStatMod(mod.Value, mod.StatType, mod.ModType, mod.Source, mod.Duration));
        }
    }

    public void RemoveMods(StatMod mod)
    {
        IsDirty = true;
        StatMods.Remove(mod);
    }

    public void RemoveTempMods(TempStatMod mod)
    {
        IsDirty = true;
        TempMods.Remove(mod);
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

    private float CalcFinalValue()
    {
        float finalValue = BaseValue +
            StatMods.FindAll(sm => sm.ModType == ModTypes.Flat).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.ModType == ModTypes.Flat).Sum(tm => tm.Value);
        float perMulti = 1 +
            StatMods.FindAll(sm => sm.ModType == ModTypes.Precent).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.ModType == ModTypes.Precent).Sum(tm => tm.Value);
        return Mathf.Round(finalValue * perMulti);
    }

    public void TickTempMods() => TempMods.RemoveAll(tm => tm.Duration < 1);
}