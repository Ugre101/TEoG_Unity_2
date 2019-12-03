using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


