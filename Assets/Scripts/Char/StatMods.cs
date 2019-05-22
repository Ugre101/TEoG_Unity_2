public enum StatsModType
{
    Flat = 100,
    Precent = 200,
}

public class StatMods
{
    public readonly float _value;
    public readonly StatsModType _type;
    public readonly int _order;
    public readonly object _source;

    public StatMods(float Value, StatsModType Type, int Order, object Source)
    {
        _value = Value;
        _type = Type;
        _order = Order;
        _source = Source;
    }

    public StatMods(float Value, StatsModType Type) : this(Value, Type, (int)Type, null)
    {
    }

    public StatMods(float Value, StatsModType Type, int Order) : this(Value, Type, Order, null)
    {
    }
}