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
    public float _current;
    protected float _max;
    protected FluidType _type;
    public virtual float Current { get { return _current; } }
    public virtual float Max { get { return _max; } }

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
    }

    public void ReFill()
    {
        if (Current < Max)
        {
            _current++;
        }
    }
}

[System.Serializable]
public abstract class SexualOrgan
{
    [SerializeField]
    protected int _baseSize;

    protected int _lastBase;
    protected float _currSize;
    protected bool _isDirty = true;

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

    public float CalcSize()
    {
        float FinalValue = _baseSize;
        return FinalValue;
    }

    public SexualOrgan()
    {
        _baseSize = 1;
    }
    public float Cost()
    {
        float cost = Mathf.Min(2000, 30 * Mathf.Pow(1.05f, _baseSize));
        return cost;
    }
    public void Grow(int toGrow = 1)
    {
        _baseSize += toGrow;
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
    public string Looks()
    {
        string dick = $"a {Size}cm long dick";
        return dick;
    }
}
[System.Serializable]
public class Balls : SexualOrgan
{
    [SerializeField]
    protected SexualFluid _fluid = new SexualFluid(FluidType.Cum);

    public virtual SexualFluid Fluid { get { return _fluid; } }

    public string Looks()
    {
        string balls = $"a pair of {Size}cm wide balls";
        balls += $", with {Fluid.Current}l";
        return balls;
    }

    public override float Size
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                // Calc
                _lastBase = _baseSize;
                _isDirty = false;
                _currSize = CalcSize();
                _fluid.FluidCalc(_currSize);
            }
            return _currSize;
        }
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

    public virtual SexualFluid Fluid { get { return _fluid; } }
    public string Looks()
    {
        string boobs = "";
        boobs += $" {Fluid.Current}";
        return boobs;
    }
    public override float Size
    {
        get
        {
            if (_isDirty || _baseSize != _lastBase)
            {
                // Calc
                _lastBase = _baseSize;
                _isDirty = false;
                _currSize = CalcSize();
                _fluid.FluidCalc(_currSize);
            }
            return _currSize;
        }
    }
}
