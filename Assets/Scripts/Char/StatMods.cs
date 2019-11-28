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
    private  StatsModType type;
    [SerializeField]
    private int order;
    public object Source { get; private set; }

    public float Value => value;

    public StatsModType Type => type;

    public int Order => order;

    public StatMods(float parValue, StatsModType parType, int parOrder, object parSource)
    {
        value = parValue;
        type = parType;
        order = parOrder;
        Source = parSource;
    }

    public StatMods(float parValue, StatsModType parType) : this(parValue, parType, (int)parType, null)
    {
    }

    public StatMods(float parValue, StatsModType parType, int parOrder) : this(parValue, parType, parOrder, null)
    {
    }
}