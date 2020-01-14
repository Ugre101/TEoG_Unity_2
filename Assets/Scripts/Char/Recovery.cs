using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Recovery : Stat
{
    public Recovery()
    {
        baseValue = 2;
    }

    [SerializeField] private List<HealthMod> healthMods = new List<HealthMod>();
    [SerializeField] private List<TempHealthMod> tempHealthMods = new List<TempHealthMod>();

    public List<HealthMod> Mods => healthMods;
    public List<TempHealthMod> TempMods => tempHealthMods;

    protected override int CalcValue
    {
        get
        {
            float flat = BaseValue
                + Mods.FindAll(m => m.ModType == ModTypes.Flat).Sum(m => m.Value)
                + TempMods.FindAll(m => m.ModType == ModTypes.Flat).Sum(m => m.Value);
            float precent = 1f
                + Mods.FindAll(m => m.ModType == ModTypes.Precent).Sum(m => m.Value)
                + TempMods.FindAll(m => m.ModType == ModTypes.Precent).Sum(m => m.Value);
            return Mathf.FloorToInt(flat * precent);
        }
    }

    #region AddAndRemoveMods

    public void AddMods(HealthMod mod)
    {
        Mods.Add(mod);
        IsDirty = true;
    }

    public void AddTempMod(TempHealthMod mod)
    {
        if (TempMods.Exists(tm => tm.Source.Equals(mod.Source)))
        {
            TempHealthMod toChange = TempMods.Find(tm => tm.Source.Equals(mod.Source));
            float diminishingReturn = (float)toChange.Duration / (float)mod.Duration;
            int toIncrease = Mathf.Max(0, Mathf.FloorToInt(mod.Duration / Mathf.Max(1, 2 * diminishingReturn)));
            toChange.IncreaseDuration(toIncrease);
        }
        else
        {
            // Clone otherwise diminishingReturn doesn't work as duration increase on both.
            TempMods.Add(new TempHealthMod(mod.Value, mod.ModType, mod.HealthType, mod.Source, mod.Duration));
        }
        IsDirty = true;
    }

    public void RemoveMods(HealthMod mod)
    {
        Mods.Remove(mod);
        IsDirty = true;
    }

    public void RemoveTempMods(TempHealthMod mod)
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
        if (Mods.Exists(sm => sm.Source.Equals(Source)))
        {
            foreach (HealthMod sm in Mods.FindAll(s => s.Source.Equals(Source)))
            {
                Mods.Remove(sm);
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
            foreach (TempHealthMod sm in TempMods.FindAll(s => s.Source.Equals(Source)))
            {
                TempMods.Remove(sm);
            }
            IsDirty = true;
            return true;
        }
        return false;
    }

    #endregion AddAndRemoveMods
}