using UnityEngine;

public enum OrganType
{
    Dick,
    Balls,
    Vagina,
    Boobs
}

public enum FluidType
{
    Cum,
    Milk
}

[System.Serializable]
public class SexualFluid
{
    [SerializeField]
    protected float _current;
    protected float _max;
    protected FluidType _type;
    public virtual float Current { get { return _current; } }
    public virtual float Max { get { return _max; } }

    protected float _baseRate;
    protected float _cumRateFlat = 0;
    protected float _cumRatePer = 1f;
    // protected bool _dirtyRate = true;
    private float CumRate
    {
        get
        {
            return _cumRateFlat * _cumRatePer;
        }
    }
    public SexualFluid(FluidType type)
    {
        _current = 0;
        FluidCalc(1);
        _type = type;
    }

    public void FluidCalc(float size)
    {
        // Volume of sphere 4/3 * pi * r^3
        float firstStep = 4f / 3f * Mathf.PI;
        float secondStep = Mathf.Pow(size, 3);
        float finalStep = firstStep * secondStep;
        _max = finalStep;
        _baseRate = _max / 500;
    }

    public void ReFill()
    {
        if (Current < Max)
        {
            _current += _baseRate + CumRate;
            fluidSlider();
        }
    }
    public void ManualSlider()
    {
        fluidSlider();
    }
    public delegate void FluidSlider();
    public static event FluidSlider fluidSlider;
}

[System.Serializable]
public abstract class SexualOrgan
{
    [SerializeField]
    protected int _baseSize;

    protected int _lastBase;
    protected float _currSize;
    protected bool _isDirty = true;
    protected bool _fluidDirty = true;

    protected float _cost;
    protected float _lastCost;

    public virtual float Size
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                // Calc
                _lastBase = _baseSize;
                _isDirty = false;
                _fluidDirty = true;
                _currSize = _baseSize;
                _cost = Mathf.Min(2000, 30 * Mathf.Pow(1.05f, _baseSize));
            }
            return _currSize;
        }
    }

    public virtual float Cost
    {
        get
        {
            if (_baseSize != _lastBase)
            {
                _cost = Mathf.Min(2000, 30 * Mathf.Pow(1.05f, _baseSize));
            }
            return _cost;
        }
    }

    public SexualOrgan()
    {
        _baseSize = 1;
    }

    public float Grow(int toGrow = 1)
    {
        float growCost = Cost;
        _baseSize += toGrow;
        return growCost;
    }

    public bool Shrink(int toShrink = 1)
    {
        _baseSize -= toShrink;
        return _baseSize <= 0 ? true : false;
    }
}

[System.Serializable]
public class Dick : SexualOrgan
{

}

[System.Serializable]
public class Balls : SexualOrgan
{
    [SerializeField]
    protected SexualFluid _fluid = new SexualFluid(FluidType.Cum);
    public virtual SexualFluid Fluid
    {
        get
        {
            if (_fluidDirty || _baseSize != _lastBase)
            {
                _fluid.FluidCalc(Size);
            }
            return _fluid;
        }
    }

    public string Looks()
    {
        string balls = $"a pair of {Size}cm wide balls";
        balls += $", with {Fluid.Current}l";
        return balls;
    }
}

[System.Serializable]
public class Vagina : SexualOrgan
{
    public string Looks()
    {
        string vagina = "";
        return vagina;
    }
}

[System.Serializable]
public class Boobs : SexualOrgan
{
    [SerializeField]
    protected SexualFluid _fluid = new SexualFluid(FluidType.Milk);

    public virtual SexualFluid Fluid {
        get
        {
            if (_fluidDirty || _baseSize != _lastCost)
            {
                _fluid.FluidCalc(Size);
            }
            return _fluid; } }

    public string Looks()
    {
        string boobs = "";
        boobs += $" {Fluid.Current}";
        return boobs;
    }
}