using UnityEngine;

public enum StatsModType
{
    Flat = 100,
    Precent = 200,
}

[System.Serializable]
public class StatMods
{
    [SerializeField]
    private float value;

    [SerializeField]
    private StatTypes statType;

    [SerializeField]
    private StatsModType type;

    [SerializeField]
    private string source;

    public float Value => value;
    public StatTypes StatType => statType;
    public StatsModType Type => type;
    public string Source => source;

    public StatMods(float parValue, StatTypes parStatTypes, StatsModType parType, string parSource = null)
    {
        value = parValue;
        type = parType;
        statType = parStatTypes;
        source = parSource;
    }
}