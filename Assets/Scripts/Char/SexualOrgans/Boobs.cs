using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

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