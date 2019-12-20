using UnityEngine;

public enum ModTypes
{
    Flat = 100,
    Precent = 200,
}

[System.Serializable]
public class StatMod
{
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public StatTypes StatType { get; private set; }
    [field: SerializeField] public ModTypes Type { get; private set; }
    [field: SerializeField] public string Source { get; private set; }

    public StatMod(float parValue, StatTypes parStatTypes, ModTypes parType, string parSource)
    {
        Value = parValue;
        Type = parType;
        StatType = parStatTypes;
        Source = parSource;
    }
}