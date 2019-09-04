using UnityEngine;
using System.Collections.Generic;
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
            return _baseRate + (_cumRateFlat * _cumRatePer);
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
            _current += Mathf.Min(CumRate,Max - Current);
            FluidSlider?.Invoke();
        }
    }
    public void ManualSlider()
    {
        FluidSlider?.Invoke();
    }
    public delegate void fluidSlider();
    public static event fluidSlider FluidSlider;
}

[System.Serializable]
public abstract class SexualOrgan
{
    [SerializeField]
    protected int _baseSize;
    protected Races race = Races.Humanoid;
    public Races Race { get { return race; } }

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
                _cost = Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, _baseSize)));
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
                _cost = Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, _baseSize)));
            }
            return _cost;
        }
    }

    public SexualOrgan()
    {
        _baseSize = 2;
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
    public void ChangeRace(Races changeTo)
    {
        race = changeTo;
    }
}

[System.Serializable]
public class Dick : SexualOrgan
{

}

public static class DickExtensions
{
    public static float Total(this List<Dick> dicks)
    {
        float tot = 0;
        foreach (Dick d in dicks)
        {
            tot += d.Size;
        }
        return tot;
    }
    public static void AddDick(this List<Dick> dicks)
    {
        dicks.Add(new Dick());
    }
    public static float Cost(this List<Dick> dicks)
    {
        return Mathf.Round(30 * Mathf.Pow(4, dicks.Count));
    }
    public static float ReCycle(this List<Dick> dicks)
    {
        Dick toShrink = dicks[dicks.Count - 1];
        toShrink.Shrink();
        if (toShrink.Size <= 1)
        {
            dicks.Remove(toShrink);
            return 30f;
        }else
        {
            return toShrink.Cost;
        }
    }

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
public static class BallsExtensions
{
    public static float Total(this List<Balls> balls)
    {
        float tot = 0;
        foreach (Balls b in balls)
        {
            tot += b.Size;
        }
        return tot;
    }
    public static void AddBalls(this List<Balls> balls)
    {
        balls.Add(new Balls());
    }
    public static float Cost(this List<Balls> balls)
    {
        return Mathf.Round(30 * Mathf.Pow(4, balls.Count));
    }
    public static float ReCycle(this List<Balls> balls)
    {
        Balls toShrink = balls[balls.Count - 1];
        toShrink.Shrink();
        if (toShrink.Size <= 1)
        {
            balls.Remove(toShrink);
            return 30f;
        }else
        {
            return toShrink.Cost;
        }
    }
    public static float CumTotal(this List<Balls> balls)
    {
        float tot = 0f;
        foreach (Balls b in balls)
        {
            tot += b.Fluid.Current;
        }
        return tot;
    }
    public static float CumMax(this List<Balls> balls)
    {
        float max = 0f;
        foreach (Balls b in balls)
        {
            max += b.Fluid.Max;
        }
        return max;
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
public static class VaginaExtensions
{
    public static float Total(this List<Vagina> vaginas)
    {
        float tot = 0;
        foreach (Vagina v in vaginas)
        {
            tot += v.Size;
        }
        return tot;
    }
    public static void AddVag(this List<Vagina> vaginas)
    {
        vaginas.Add(new Vagina());
    }
    public static float Cost(this List<Vagina> vaginas)
    {
        return Mathf.Round(30 * Mathf.Pow(4, vaginas.Count));
    }
    public static float ReCycle(this List<Vagina> vaginas)
    {
        Vagina toShrink = vaginas[vaginas.Count - 1];
        toShrink.Shrink();
        if (toShrink.Size <= 1)
        {
            vaginas.Remove(toShrink);
            return 30f;
        }else
        {
            return toShrink.Cost;
        }
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
public static class BoobExtensions
{
    public static float Total(this List<Boobs> boobs)
    {
        float tot = 0;
        foreach (Boobs b in boobs)
        {
            tot += b.Size;
        }
        return tot;
    }
    public static void AddBoobs(this List<Boobs> boobs)
    {
        boobs.Add(new Boobs());
    }
    public static float Cost(this List<Boobs> boobs)
    {
        return Mathf.Round(30 * Mathf.Pow(4, boobs.Count));
    }
    public static float ReCycle(this List<Boobs> boobs)
    {
        Boobs toShrink = boobs[boobs.Count - 1];
        toShrink.Shrink();
        if (toShrink.Size <= 1)
        {
            boobs.Remove(toShrink);
            return 30f;
        }else
        {
            return toShrink.Cost;
        }
    }
    public static float MilkTotal(this List<Boobs> boobs)
    {
        float tot = 0f;
        foreach (Boobs b in boobs)
        {
            tot += b.Fluid.Current;
        }
        return tot;
    }
    public static float MilkMax(this List<Boobs> boobs)
    {
        float max = 0f;
        foreach (Boobs b in boobs)
        {
            max += b.Fluid.Max;
        }
        return max;
    }
}