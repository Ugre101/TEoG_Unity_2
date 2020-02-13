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
    [SerializeField] private float current = 0;

    public float Current
    {
        get => current;
        private set
        {
            current = value;
            FluidSlider?.Invoke();
        }
    }

    [SerializeField] private FluidType type;

    public FluidType Type => type;

    public float MaxAmount { get; private set; }
    public float ReFillRate { get; private set; }

    public SexualFluid(FluidType parType, float parSize)
    {
        type = parType;
        FluidCalc(parSize);
    }

    public void FluidCalc(float size)
    {
        // Volume of sphere 4/3 * pi * r^3
        MaxAmount = 4f / 3f * Mathf.PI * Mathf.Pow(size, 3);
        ReFillRate = MaxAmount / 500;
    }

    public void ReFill()
    {
        if (Current < MaxAmount)
        {
            float reFilled = current + ReFillRate;
            Current = Mathf.Clamp(reFilled, 0, MaxAmount);
        }
    }

    public void ReFill(float bonus)
    {
        if (Current < MaxAmount)
        {
            float reFilled = current + ReFillRate + bonus;
            Current = Mathf.Clamp(reFilled, 0, MaxAmount);
        }
    }

    public float DisCharge()
    {
        float disCharge = Mathf.Clamp(MaxAmount * 0.33f, 0, current);
        Current -= disCharge;
        return Mathf.Round(disCharge);
    }

    public float DisCharge(float percentage)
    {
        float disCharge = Mathf.Clamp(MaxAmount * percentage, 0, current);
        Current -= disCharge;
        return Mathf.Round(disCharge);
    }

    public delegate void fluidSlider();

    public static event fluidSlider FluidSlider;
}

[System.Serializable]
public abstract class SexualOrgan
{
    [SerializeField] protected int baseSize;

    public int BaseSize
    {
        get => baseSize;
        private set
        {
            baseSize = value;
            SomethingChanged?.Invoke();
        }
    }

    protected int lastBase;
    protected float currSize;

    [SerializeField] protected Races race = Races.Humanoid;

    public Races Race => race;

    public virtual float Size
    {
        get
        {
            if (BaseSize != lastBase)
            {
                // Calc
                lastBase = BaseSize;
                currSize = BaseSize;
            }
            return currSize;
        }
    }

    public SexualOrgan(int parBase)
    {
        BaseSize = parBase;
    }

    public SexualOrgan() : this(2)
    {
    }

    protected float lastCost;
    public abstract float Cost { get; }

    public float Grow(int toGrow = 1)
    {
        float growCost = Cost;
        BaseSize += toGrow;
        return growCost;
    }

    public bool Shrink(int toShrink = 1)
    {
        BaseSize -= toShrink;
        return BaseSize <= 0;
    }

    public void ChangeRace(Races changeTo) => race = changeTo;

    public delegate void Change();

    public static event Change SomethingChanged;
}

public static class SexOrganExtension
{
    public static float Total(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Sum();

    public static float BiggestSize(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public static SexualOrgan Biggest(this IEnumerable<SexualOrgan> list) => (SexualOrgan)list.Where(so => so.Size == list.BiggestSize()).Select(so => so);

    public static float Smallest(this IEnumerable<SexualOrgan> list) => list.Select(so => so.Size).DefaultIfEmpty(0).Min();

    public static void RefreshOrgans(this BasicChar bc, bool autoEss = false)
    {
        Organs so = bc.SexualOrgans;
        List<Dick> dicks = so.Dicks;
        List<Balls> balls = so.Balls;
        List<Vagina> vaginas = so.Vaginas;
        List<Boobs> boobs = so.Boobs;
        dicks.RemoveAll(d => d.Size <= 0);
        balls.RemoveAll(b => b.Size <= 0);
        vaginas.RemoveAll(v => v.Size <= 0);
        boobs.RemoveAll(b => b.Size <= 0);
        float ballsRatio = balls.Total() * 2f + 1f;
        float vaginaRatio = vaginas.Total() * 1.5f + 1f;
        if (so.GenderPrefActive)
        {
            if (so.GenderPref == Genders.Cuntboy)
            {
                vaginaRatio -= 100;
            }
            else if (so.GenderPref == Genders.Dickgirl)
            {
                vaginaRatio += 100;
            }
        }
        Essence masc = bc.Essence.Masc;
        int StableAmount = bc.Essence.StableEssence.Value;
        if (masc.Amount > StableAmount)
        {
            if (dicks.Total() <= ballsRatio)
            {
                if (dicks.Exists(d => masc.Amount >= d.Cost))
                {
                    foreach (Dick d in dicks)
                    {
                        if (masc.Amount >= d.Cost)
                        {
                            masc.Lose(d.Grow());
                            break;
                        }
                    }
                }
                else if (masc.Amount >= dicks.Cost())
                {
                    masc.Lose(dicks.Cost());
                    dicks.AddDick();
                }
            }
            else
            {
                if (balls.Exists(b => masc.Amount >= b.Cost))
                {
                    foreach (Balls b in balls)
                    {
                        if (masc.Amount >= b.Cost)
                        {
                            masc.Lose(b.Grow());
                            break;
                        }
                    }
                }
                else if (masc.Amount >= balls.Cost())
                {
                    masc.Lose(balls.Cost());
                    balls.AddBalls();
                }
            }
        }
        Essence femi = bc.Essence.Femi;
        if (femi.Amount > StableAmount)
        {
            if (boobs.Total() <= vaginaRatio)
            {
                if (boobs.Exists(b => femi.Amount >= b.Cost))
                {
                    foreach (Boobs b in boobs)
                    {
                        if (femi.Amount >= b.Cost)
                        {
                            femi.Lose(b.Grow());
                            break;
                        }
                    }
                }
                else if (femi.Amount >= boobs.Cost())
                {
                    femi.Lose(boobs.Cost());
                    boobs.AddBoobs();
                }
            }
            else
            {
                if (vaginas.Exists(v => femi.Amount >= v.Cost))
                {
                    foreach (Vagina v in vaginas)
                    {
                        if (femi.Amount >= v.Cost)
                        {
                            femi.Lose(v.Grow());
                            break;
                        }
                    }
                }
                else if (femi.Amount >= vaginas.Cost())
                {
                    femi.Lose(vaginas.Cost());
                    vaginas.AddVag();
                }
            }
        }
    }
}