using UnityEngine;

public enum ModTypes
{
    Flat = 100,
    Precent = 200,
}

[System.Serializable]
public class StatMod : Mod
{
    public StatMod(float parValue, string parSource, ModTypes parType) : base(parValue, parType, parSource)
    {
    }

    public static StatMod FlatMod(float val, string source) => new StatMod(val, source, ModTypes.Flat);

    public static StatMod PrecentMod(float val, string source) => new StatMod(val, source, ModTypes.Precent);
}

[System.Serializable]
public class AssingStatmod
{
    [SerializeField] private StatMod statMod;
    [SerializeField] private StatTypes statTypes;

    public static AssingStatmod Create(float value, string source, ModTypes modTypes, StatTypes statTypes)
        => new AssingStatmod(new StatMod(value, source, modTypes), statTypes);

    public AssingStatmod(StatMod statMod, StatTypes statTypes)
    {
        this.statMod = statMod;
        this.statTypes = statTypes;
    }

    public StatMod StatMod => statMod;
    public StatTypes StatTypes => statTypes;
}