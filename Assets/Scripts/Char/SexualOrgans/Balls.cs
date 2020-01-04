using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Balls : SexualOrgan
{
    public Balls() : base()
    {
        _fluid = new SexualFluid(FluidType.Cum, Size);
    }

    public Balls(int size) : base(size)
    {
        _fluid = new SexualFluid(FluidType.Cum, Size);
    }

    [SerializeField]
    private SexualFluid _fluid;

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
}

public static class BallsExtensions
{
    public static void AddBalls(this List<Balls> balls) => balls.Add(new Balls());

    public static void AddBalls(this List<Balls> balls, int parSize) => balls.Add(new Balls(parSize));

    public static float Cost(this List<Balls> balls) => Mathf.Round(30 * Mathf.Pow(4, balls.Count));

    public static float ReCycle(this List<Balls> balls)
    {
        Balls toShrink = balls[balls.Count - 1];
        if (toShrink.Shrink())
        {
            balls.Remove(toShrink);
            return 30f;
        }
        return toShrink.Cost;
    }

    public static float Cumming(this List<Balls> balls) => balls.Sum(b => b.Fluid.DisCharge());

    public static float Cumming(this List<Balls> balls, float dischargePrecentage) => balls.Sum(b => b.Fluid.DisCharge(dischargePrecentage));

    public static float CumTotal(this List<Balls> balls) => balls.Select(b => b.Fluid.Current).DefaultIfEmpty(0).Sum();

    public static float CumMax(this List<Balls> balls) => balls.Select(b => b.Fluid.Current).DefaultIfEmpty(0).Sum();

    public static string Looks(this Balls parBalls, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} pair of {Settings.MorInch(parBalls.Size)} wide balls, with {Settings.LorGal(parBalls.Fluid.Current)}";
    }

    public static string Looks(this List<Balls> parBalls)
    {
        StringBuilder builder = new StringBuilder();
        foreach (Balls balls in parBalls)
        {
            builder.Append(balls.Looks() + "\n");
        }
        return builder.ToString();
    }
}