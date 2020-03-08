using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Balls : SexualOrganWithFluid
{
    public Balls() : base(FluidType.Cum)
    {
    }

    public Balls(int size) : base(FluidType.Cum, size)
    {
    }

    public override float Cost => this.GrowCost();
}

public static class BallsExtensions
{
    public static Balls Biggest(this List<Balls> list) => list.Find(so => so.Size == list.BiggestSize());

    public static float GrowCost(this Balls balls) => Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, balls.BaseSize)));

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

    public static float CumMax(this List<Balls> balls) => balls.Select(b => b.Fluid.MaxAmount).DefaultIfEmpty(0).Sum();

    public static string Look(this Balls parBalls, bool capital = true)
        => $"{(capital ? "A" : "a")} pair of {Settings.MorInch(parBalls.Size)} wide balls, with {Settings.LorGal(parBalls.Fluid.Current)}";

    public static string LookWithOutFluid(this Balls parBalls, bool capital = true)
        => $"{(capital ? "A" : "a")} pair of {Settings.MorInch(parBalls.Size)} wide balls";

    public static string Looks(this List<Balls> parBalls, bool fluid = true)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < parBalls.Count; i++)
        {
            Balls balls = parBalls[i];
            if (i == 0)
            {
                builder.Append(fluid ? balls.Look() : balls.LookWithOutFluid());
            }
            else
            {
                builder.Append(fluid ? balls.Look(false) : balls.LookWithOutFluid(false));
            }
            if (i == parBalls.Count - 2)
            {
                builder.Append(" and ");
            }
            else if (i == parBalls.Count - 1)
            {
                builder.Append(".");
            }
            else
            {
                builder.Append(", ");
            }
        }
        return builder.ToString();
    }
}