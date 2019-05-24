using UnityEngine;
public enum OrganType
{
    Dick,
    Balls,
    Vagina,
    Boobs
};
[System.Serializable]
public class SexualOrgan
{

    protected OrganType Type;
    public int _baseSize;
    protected int _lastBase;
    protected float _currSize;
    protected bool _isDirty = true;

    protected float _capacity;
    public float _fluid;
    public virtual float Size
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                // Calc
                _lastBase = _baseSize;
                _isDirty = false;
                _currSize = CalcSize();
            }
            return _currSize;
        }
    }
    public virtual float Capacity
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                _capacity = CalcCapacity();
            }
            return _capacity;
        }
    }
    public virtual float Fluid
    {
        get
        {
            return _fluid;
        }
    }
    public string Looks()
    {
        switch (Type)
        {
            case OrganType.Balls:
                string balls = $"a pair of {Size}cm wide balls";
                balls += $", with {Capacity}l";
                return balls;
            case OrganType.Dick:
                string dick = $"a {Size}cm long dick";
                return dick;
            case OrganType.Vagina:
                string vagina = "";
                return vagina;
            case OrganType.Boobs:
                string boobs = "";
                boobs += $" {Capacity}";
                return boobs;
            default:
                return "error";
        }
    }
    private float CalcSize()
    {
        float FinalValue = _baseSize;
        return FinalValue;
    }
    private float CalcCapacity()
    {
        // Volume of sphere 4/3 * pi * r^3
        float firstStep = 4f / 3f * Mathf.PI;
        float secondStep = Mathf.Pow(Size, 3);
        float finalStep = firstStep * secondStep;
        return finalStep;
    }
    public void Refill()
    {
        if (_fluid < _capacity)
        {
            _fluid++;
        }
    }
    public SexualOrgan(OrganType type)
    {
        Type = type;
        _baseSize = 1;
        if (type == OrganType.Balls || type == OrganType.Boobs)
        {
            _capacity = CalcCapacity();
            _fluid = 0;
        }
    }
}