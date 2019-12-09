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

    protected int lastBase;
    protected float currSize;

    protected Races race = Races.Humanoid;
    public Races Race => race;

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

    public SexualOrgan() => baseSize = 2;

    public SexualOrgan(int parBase) => baseSize = parBase;

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
    public static float Total(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Sum();

    public static float Biggest(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public static float Smallest(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Min();

    public static void RefreshOrgans(this BasicChar bc, bool autoEss = false)
    {
        bc.SexualOrgans.Dicks.RemoveAll(d => d.Size <= 0);
        bc.SexualOrgans.Balls.RemoveAll(b => b.Size <= 0);
        bc.SexualOrgans.Vaginas.RemoveAll(v => v.Size <= 0);
        bc.SexualOrgans.Boobs.RemoveAll(b => b.Size <= 0);
        if (autoEss)
        {
            if (bc.Masc.Amount > 0)
            {
                if (bc.SexualOrgans.Dicks.Total()
                    <= bc.SexualOrgans.Balls.Total() * 2f + 1f)
                {
                    if (bc.SexualOrgans.Dicks.Exists(d
                        => bc.Masc.Amount >= d.Cost))
                    {
                        foreach (Dick d in bc.SexualOrgans.Dicks)
                        {
                            if (bc.Masc.Amount >= d.Cost)
                            {
                                bc.Masc.Lose(d.Grow());
                            }
                        }
                    }
                    else if (bc.Masc.Amount >= bc.SexualOrgans.Dicks.Cost())
                    {
                        bc.Masc.Lose(bc.SexualOrgans.Dicks.Cost());
                        bc.SexualOrgans.Dicks.AddDick();
                    }
                }
                else
                {
                    if (bc.SexualOrgans.Balls.Exists(b => bc.Masc.Amount >= b.Cost))
                    {
                        foreach (Balls b in bc.SexualOrgans.Balls)
                        {
                            if (bc.Masc.Amount >= b.Cost)
                            {
                                bc.Masc.Lose(b.Grow());
                            }
                        }
                    }
                    else if (bc.Masc.Amount >= bc.SexualOrgans.Balls.Cost())
                    {
                        bc.Masc.Lose(bc.SexualOrgans.Balls.Cost());
                        bc.SexualOrgans.Balls.AddBalls();
                    }
                }
            }
            if (bc.Femi.Amount > 0)
            {
                if (bc.SexualOrgans.Boobs.Total()
                    <= bc.SexualOrgans.Vaginas.Total() * 1.5f + 1f)
                {
                    if (bc.SexualOrgans.Boobs.Exists(b => bc.Femi.Amount >= b.Cost))
                    {
                        foreach (Boobs b in bc.SexualOrgans.Boobs)
                        {
                            if (bc.Femi.Amount >= b.Cost)
                            {
                                bc.Femi.Lose(b.Grow());
                            }
                        }
                    }
                    else if (bc.Femi.Amount >= bc.SexualOrgans.Boobs.Cost())
                    {
                        bc.Femi.Lose(bc.SexualOrgans.Boobs.Cost());
                        bc.SexualOrgans.Boobs.AddBoobs();
                    }
                }
                else
                {
                    if (bc.SexualOrgans.Vaginas.Exists(v => bc.Femi.Amount >= v.Cost))
                    {
                        foreach (Vagina v in bc.SexualOrgans.Vaginas)
                        {
                            if (bc.Femi.Amount >= v.Cost)
                            {
                                bc.Femi.Lose(v.Grow());
                            }
                        }
                    }
                    else if (bc.Femi.Amount >= bc.SexualOrgans.Vaginas.Cost())
                    {
                        bc.Femi.Lose(bc.SexualOrgans.Vaginas.Cost());
                        bc.SexualOrgans.Vaginas.AddVag();
                    }
                }
            }
        }
    }
}