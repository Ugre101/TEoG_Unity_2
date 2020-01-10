using UnityEngine;

public enum ModTypes
{
    Flat = 100,
    Precent = 200,
}

[System.Serializable]
public class StatMod : Mod
{
    [SerializeField] private StatTypes statType;

    public StatMod(float parValue, StatTypes parStatTypes, string parSource, ModTypes parType) : base(parValue, parType, parSource)
    {
        this.statType = parStatTypes;
    }

    public StatTypes StatType => statType;
}