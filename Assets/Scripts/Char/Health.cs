using System.Collections.Generic;
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
            if (isDirty)
            {
                lastTotal = CalcFinalMax();
            }
            return lastTotal;
        }
    }

    public int SetMax { set { baseMax = value; isDirty = true; } }
    private bool isDirty = true;
    [field: SerializeField] public List<HealthMod> HealthMods { get; private set; } = new List<HealthMod>();
    [field: SerializeField] public List<TempHealthMod> TempHealthMods { get; private set; } = new List<TempHealthMod>();

    private int CalcFinalMax()
    {
        float flatValue = baseMax;
        float perValue = 1;
        return Mathf.RoundToInt(flatValue * perValue);
    }

    public Health(int parMax)
    {
        baseMax = parMax;
        current = parMax;
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
}

[System.Serializable]
public class HealthMod
{
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public ModTypes ModType { get; private set; }
    [field: SerializeField] public HealthTypes HealthType { get; private set; }
    [field: SerializeField] public string Source { get; private set; }

    public HealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource)
    {
        Value = parVal;
        ModType = parModType;
        HealthType = parHealthType;
        Source = parSource;
    }
}

public class TempHealthMod : HealthMod
{
    [field: SerializeField] public int Duration { get; private set; }

    public TempHealthMod(float parVal, ModTypes parModType, HealthTypes parHealthType, string parSource, int parDuration)
        : base(parVal, parModType, parHealthType, parSource)
    {
        Duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    private void TickDown() => Duration--;

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}