using UnityEngine;

[System.Serializable]
public abstract class Mod
{
    public Mod(float parValue, ModTypes parType, string parSource)
    {
        Value = parValue;
        ModType = parType;
        Source = parSource;
    }

    [field: SerializeField] public float Value { get; protected set; }
    [field: SerializeField] public ModTypes ModType { get; protected set; }
    [field: SerializeField] public string Source { get; protected set; }
}
/*
public abstract class TempMod : Mod
{
    public TempMod(float parValue, ModTypes parType, string parSource, int parDuration) : base(parValue, parType, parSource)
    {
        Duration = parDuration;
        DateSystem.NewHourEvent += TickDown;
    }

    [field: SerializeField] public int Duration { get; protected set; }

    private void TickDown() => Duration--;

    public void IncreaseDuration(int toIncrease) => Duration += toIncrease;
}
*/