using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HealthTypes
{
    Health,
    WillPower
}

[System.Serializable]
public class Health
{
    [SerializeField]
    private float current;

    [SerializeField]
    private int baseMax;

    private int lastTotal;

    private int MaxFinal
    {
        get
        {
            if (IsDirty)
            {
                lastTotal = CalcFinalMax();

                IsDirty = false;
                UpdateSliderEvent?.Invoke();
            }
            return lastTotal;
        }
    }

    public int SetMax { set { baseMax = value; IsDirty = true; } }

    private bool dirty = true;

    private bool IsDirty
    {
        get => dirty;
        set
        {
            dirty = value;
            if (value)
            {
                _ = MaxFinal;
            }
        }
    }

    public bool IsMax => current >= MaxFinal;
    private List<CharStats> affectedBy = new List<CharStats>();
    public List<CharStats> AffectedBy => affectedBy;
    [field: SerializeField] public List<HealthMod> HealthMods { get; private set; } = new List<HealthMod>();
    [field: SerializeField] public List<TempHealthMod> TempHealthMods { get; private set; } = new List<TempHealthMod>();

    public void TickTempMods() => TempHealthMods.RemoveAll(tm => tm.Duration < 1);

    private int CalcFinalMax()
    {
        float flatValue = baseMax +
            HealthMods.FindAll(hm => hm.ModType == ModTypes.Flat).Sum(hm => hm.Value) +
            TempHealthMods.FindAll(thm => thm.ModType == ModTypes.Flat).Sum(thm => thm.Value) +
            AffectedBy.Sum(ab => ab.Value);
        float perValue = 1 +
            HealthMods.FindAll(hm => hm.ModType == ModTypes.Precent).Sum(hm => hm.Value) +
            TempHealthMods.FindAll(thm => thm.ModType == ModTypes.Precent).Sum(thm => thm.Value);
        return Mathf.RoundToInt(flatValue * perValue);
    }

    public Health(int parMax)
    {
        baseMax = parMax;
        current = parMax;
        DateSystem.NewHourEvent += TickTempMods;
    }

    public Health(int parMax, CharStats affectedBy)
    {
        baseMax = parMax;
        current = parMax;
        this.affectedBy = new List<CharStats>() { affectedBy };
        this.affectedBy.ForEach(ab =>
        {
            ab.ValueChanged += () => IsDirty = true;
        });
        DateSystem.NewHourEvent += TickTempMods;
    }

    public Health(int parMax, List<CharStats> affectedBy)
    {
        baseMax = parMax;
        current = parMax;
        this.affectedBy = affectedBy;
        this.affectedBy.ForEach(ab =>
        {
            ab.ValueChanged += () => IsDirty = true;
        });

        DateSystem.NewHourEvent += TickTempMods;
    }

    public bool TakeDmg(float dmg)
    {
        current = Mathf.Max(0, current - dmg);
        UpdateSliderEvent?.Invoke();
        if (current <= 0)
        {
            DeadEvent?.Invoke();
            return true;
        }
        return false;
    }

    public void Gain(float gain)
    {
        current += Mathf.Clamp(gain, 0, MaxFinal - current);
        UpdateSliderEvent?.Invoke();
    }

    public void FullGain() => current = MaxFinal;

    public float SliderValue => current / MaxFinal;

    public string Status => $"{current} / {MaxFinal}";

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
    }

    public void RemoveMods(HealthMod mod)
    {
        HealthMods.Remove(mod);
        IsDirty = true;
    }

    public void RemoveTempMods(TempHealthMod mod)
    {
        TempHealthMods.Remove(mod);
        IsDirty = true;
    }

    public bool RemoveFromSource(string Source)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return false;
        }
        if (HealthMods.Exists(sm => sm.Source.Equals(Source)))
        {
            foreach (HealthMod sm in HealthMods.FindAll(s => s.Source.Equals(Source)))
            {
                HealthMods.Remove(sm);
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
        if (TempHealthMods.Exists(sm => sm.Source.Equals(Source)))
        {
            foreach (TempHealthMod sm in TempHealthMods.FindAll(s => s.Source.Equals(Source)))
            {
                TempHealthMods.Remove(sm);
            }
            IsDirty = true;
            return true;
        }
        return false;
    }

    #endregion AddAndRemoveMods
}