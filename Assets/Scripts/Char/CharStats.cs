using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharStats
{
    [SerializeField]
    private int baseValue;

    private float _lastBaseValue;
    private float _currValue;

    private bool _isDirty = true;

    public float Value
    {
        get
        {
            if (_isDirty)
            {
                _lastBaseValue = BaseValue;
                _currValue = CalcFinalValue();
                _isDirty = false;
            }
            return _currValue;
        }
    }

    [field: SerializeField] public List<StatMod> StatMods { get; private set; } = new List<StatMod>();
    [field: SerializeField] public List<TempStatMod> TempMods { get; private set; } = new List<TempStatMod>();

    public int BaseValue { get => baseValue; set { baseValue = value; _isDirty = true; } }

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
        _isDirty = true;
        StatMods.Add(mod);
    }

    public void AddTempMod(TempStatMod mod)
    {
        _isDirty = true;
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
            TempMods.Add(new TempStatMod(mod.Value, mod.StatType, mod.Type, mod.Source, mod.Duration));
        }
    }

    public void RemoveMods(StatMod mod)
    {
        _isDirty = true;
        StatMods.Remove(mod);
    }

    public void RemoveTempMods(TempStatMod mod)
    {
        _isDirty = true;
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
            _isDirty = true;
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
            _isDirty = true;
            return true;
        }
        return false;
    }

    private float CalcFinalValue()
    {
        float finalValue = BaseValue +
            StatMods.FindAll(sm => sm.Type == ModTypes.Flat).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.Type == ModTypes.Flat).Sum(tm => tm.Value);
        float perMulti = 1 +
            StatMods.FindAll(sm => sm.Type == ModTypes.Precent).Sum(sm => sm.Value) +
            TempMods.FindAll(tm => tm.Type == ModTypes.Precent).Sum(tm => tm.Value);
        return Mathf.Round(finalValue * perMulti);
    }

    public void TickTempMods() => TempMods.RemoveAll(tm => tm.Duration < 1);
}