using System;
using System.Collections.Generic;
using System.Linq;
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

[Serializable]
public class SexualFluid
{
    [SerializeField]
    protected float current;

    [SerializeField]
    private FluidType type;

    protected float maxAmount;
    public virtual float Current => current;
    public virtual float Max => maxAmount;
    public FluidType Type => type;
    protected float baseRate;

    // protected bool _dirtyRate = true;
    private float ReFillRate => baseRate;

    public SexualFluid(FluidType parType, float parSize = 1f)
    {
        current = 0;
        FluidCalc(parSize);
        type = parType;
    }

    public void FluidCalc(float size)
    {
        // Volume of sphere 4/3 * pi * r^3
        maxAmount = 4f / 3f * Mathf.PI * Mathf.Pow(size, 3);
        baseRate = maxAmount / 500;
    }

    public void ReFill(float bonus = 0)
    {
        if (Current < Max)
        {
            current += Mathf.Min(ReFillRate + bonus, Max - Current);
            FluidSlider?.Invoke();
        }
    }

    public void ManualSlider() => FluidSlider?.Invoke();

    public delegate void fluidSlider();

    public static event fluidSlider FluidSlider;
}

[System.Serializable]
public abstract class SexualOrgan
{
    [SerializeField]
    protected int baseSize;

    protected Races race = Races.Humanoid;
    public Races Race => race;

    protected int lastBase;
    protected float currSize;

    [SerializeField]
    protected float baseCost = 30;

    protected float cost;
    protected float lastCost;

    public virtual float Size
    {
        get
        {
            if (baseSize != lastBase)
            {
                // Calc
                lastBase = baseSize;
                currSize = baseSize;
                cost = Mathf.Ceil(Mathf.Min(2000, baseCost * Mathf.Pow(1.05f, baseSize)));
            }
            return currSize;
        }
    }

    public virtual float Cost
    {
        get
        {
            if (baseSize != lastBase)
            {
                cost = Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, baseSize)));
            }
            return cost;
        }
    }

    public SexualOrgan()
    {
        baseSize = 2;
    }

    public float Grow(int toGrow = 1)
    {
        float growCost = Cost;
        baseSize += toGrow;
        SomethingChanged?.Invoke();
        return growCost;
    }

    public bool Shrink(int toShrink = 1)
    {
        baseSize -= toShrink;
        SomethingChanged?.Invoke();
        return baseSize <= 0;
    }

    public void ChangeRace(Races changeTo) => race = changeTo;

    public delegate void Change();

    public static event Change SomethingChanged;
}

public static class SexOrganExtension
{
    public static float Total(this IEnumerable<SexualOrgan> list)
    {
        return list.Sum(so => so.Size);
    }
}

[System.Serializable]
public class Dick : SexualOrgan
{
}

public static class DickExtensions
{
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
        if (toShrink.Shrink())
        {
            dicks.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }
}

[System.Serializable]
public class Balls : SexualOrgan
{
    [SerializeField]
    private SexualFluid _fluid = new SexualFluid(FluidType.Cum);

    public virtual SexualFluid Fluid
    {
        get
        {
            if (baseSize != lastBase)
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
        if (toShrink.Shrink())
        {
            balls.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }

    public static float CumTotal(this List<Balls> balls)
    {
        return balls.Sum(b => b.Fluid.Current);
    }

    public static float CumMax(this List<Balls> balls)
    {
        return balls.Sum(b => b.Fluid.Max);
    }
}

[Serializable]
public class Vagina : SexualOrgan
{
    [SerializeField]
    private Womb womb = new Womb();

    public Womb Womb => womb;

    public string Looks()
    {
        string vagina = "";
        return vagina;
    }
}

public static class VaginaExtensions
{
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
        if (toShrink.Shrink())
        {
            vaginas.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }
}

[System.Serializable]
public class Boobs : SexualOrgan
{
    [SerializeField]
    private SexualFluid _fluid = new SexualFluid(FluidType.Milk);

    public virtual SexualFluid Fluid
    {
        get
        {
            if (baseSize != lastBase)
            {
                _fluid.FluidCalc(Size);
            }
            return _fluid;
        }
    }

    public string Looks()
    {
        string boobs = "";
        boobs += $" {Fluid.Current}";
        return boobs;
    }
}

public static class BoobExtensions
{
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
        if (toShrink.Shrink())
        {
            boobs.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }

    public static float MilkTotal(this List<Boobs> boobs)
    {
        return boobs.Sum(b => b.Fluid.Current);
    }

    public static float MilkMax(this List<Boobs> boobs)
    {
        return boobs.Sum(b => b.Fluid.Max);
    }
}