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


    public static float Cumming(this List<Balls> balls) => balls.Sum(b => b.Fluid.DisCharge());

    public static float Cumming(this List<Balls> balls, float dischargePrecentage) => balls.Sum(b => b.Fluid.DisCharge(dischargePrecentage));

    public static string Look(this Balls parBalls, bool capital = true)
        => $"{(capital ? "A" : "a")} pair of {parBalls.Size.MorInch()} wide balls, with {parBalls.Fluid.Current.LorGal()}";

    public static string LookWithOutFluid(this Balls parBalls, bool capital = true)
        => $"{(capital ? "A" : "a")} pair of {parBalls.Size.MorInch()} wide balls";

}