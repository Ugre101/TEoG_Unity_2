using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
public enum StatType
{
    Str,
    Charm,
    End,
    Dex
}

[Serializable]
public class CharStats
{
    public int _baseValue;

    protected float _lastBaseValue;
    protected float _currValue;

    protected bool _isDirty = true;

    public virtual float Value
    {
        get
        {
            if (_isDirty || _baseValue != _lastBaseValue)
            {
                _lastBaseValue = _baseValue;
                _currValue = CalcFinalValue();
                _isDirty = false;
            }
            return _currValue;
        }
    }

    protected readonly List<StatMods> _statMods;
    public readonly ReadOnlyCollection<StatMods> _StatMods;

    public CharStats()
    {
        _baseValue = 0;
        _statMods = new List<StatMods>();
        _StatMods = _statMods.AsReadOnly();
    }

    public CharStats(int baseValue) : this()
    {
        _baseValue = baseValue;
    }

    public virtual void addMods(StatMods mod)
    {
        _isDirty = true;
        _statMods.Add(mod);
    }

    public virtual void removeMods(StatMods mod)
    {
        _isDirty = true;
        _statMods.Remove(mod);
    }

    public bool removeFromSource(object Source)
    {
        bool didRemove = false;
        for (int i = _statMods.Count - 1; i >= 0; i--)
        {
            StatMods mod = _statMods[i];
            if (mod._source == Source)
            {
                _isDirty = true;
                didRemove = true;
                _statMods.Remove(mod);
            }
        }
        return didRemove;
    }

    private float CalcFinalValue()
    {
        float finalValue = _baseValue;
        float perMulti = 1;
        _statMods.Sort((a, b) => a._order.CompareTo(b._order));
        for (int i = 0; i < _statMods.Count; i++)
        {
            StatMods mod = _statMods[i];
            if (mod._type == StatsModType.Flat)
            {
                finalValue += mod._value;
            }
            else if (mod._type == StatsModType.Precent)
            {
                perMulti += mod._value;
            }
        }
        return Mathf.Round(finalValue * perMulti);
    }
}