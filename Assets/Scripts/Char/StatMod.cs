using UnityEngine;

public enum ModTypes
{
    Flat = 100,
    Precent = 200,
}

[System.Serializable]
public class StatMod : Mod
{
    [field: SerializeField] public StatTypes StatType { get; private set; }

    public StatMod(float parValue, StatTypes parStatTypes, string parSource, ModTypes parType) : base(parValue, parType, parSource)
    {
        StatType = parStatTypes;
    }
}