using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharStats
{
    public int baseValue;

    private float _lastBaseValue;
    private float _currValue;

    private bool _isDirty = true;

    public float Value
    {
        get
        {
            if (_isDirty || baseValue != _lastBaseValue)
            {
                _lastBaseValue = baseValue;
                _currValue = CalcFinalValue();
                _isDirty = false;
            }
            return _currValue;
        }
    }

    [SerializeField]
    private List<StatMods> _statMods = new List<StatMods>();

    public List<StatMods> StatMods => _statMods;

    public CharStats() => baseValue = 10;

    public CharStats(int parBaseValue) => baseValue = parBaseValue;

    public void AddMods(StatMods mod)
    {
        _isDirty = true;
        _statMods.Add(mod);
    }

    public void RemoveMods(StatMods mod)
    {
        _isDirty = true;
        _statMods.Remove(mod);
    }

    public bool RemoveFromSource(object Source)
    {
        bool didRemove = false;
        if (_statMods.Exists(sm => sm.Source == Source))
        {
            foreach (StatMods sm in _statMods.FindAll(s => s.Source == Source))
            {
                _statMods.Remove(sm);
            }
            _isDirty = true;
            didRemove = true;
        }
        return didRemove;
    }

    private float CalcFinalValue()
    {
        float finalValue = baseValue +
            _statMods.FindAll(sm => sm.Type == StatsModType.Flat).Sum(sm => sm.Value); ;
        float perMulti = 1 +
            _statMods.FindAll(sm => sm.Type == StatsModType.Precent).Sum(sm => sm.Value);
        return Mathf.Round(finalValue * perMulti);
    }
}