using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Boobs : SexualOrganWithFluid
{
    public Boobs() : base(FluidType.Milk)
    {
    }

    public Boobs(int size) : base(FluidType.Milk, size)
    {
    }

    public override float Cost => this.GrowCost();

    public string Looks()
    {
        string boobs = "";
        boobs += $" {Fluid.Current}";
        return boobs;
    }
}

public static class BoobExtensions
{
    public static Boobs Biggest(this List<Boobs> list) => list.Find(so => so.Size == list.BiggestSize());

    public static float GrowCost(this Boobs boobs) => Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, boobs.BaseSize)));

    public static void AddBoobs(this List<Boobs> boobs) => boobs.Add(new Boobs());

    public static void AddBoobs(this List<Boobs> boobs, int parSize) => boobs.Add(new Boobs(parSize));

    public static float Cost(this List<Boobs> boobs) => Mathf.Round(30 * Mathf.Pow(4, boobs.Count));

    public static float ReCycle(this List<Boobs> boobs)
    {
        Boobs toShrink = boobs[boobs.Count - 1];
        if (!toShrink.Shrink()) return toShrink.Cost;
        
        boobs.Remove(toShrink);
        return 30f;
    }

    public static float Milking(this IEnumerable<Boobs> boobs) => boobs.Sum(b => b.Fluid.DisCharge());

    public static float Milking(this IEnumerable<Boobs> boobs, float dischargePrecentage) => boobs.Sum(b => b.Fluid.DisCharge(dischargePrecentage));

    private static string BoobSizeConvertor(this Boobs boob)
    {
        List<string> Bra = new List<string>
        {
            "flat","AA","A","B","C","D","DD", "Large F", "G", "Large G","H", "Large H","I", "Large I","J","Large J",
            "K","Large K","L","Large L","M","Large M","N","Large N","O","Large O","scale-breaking"
        };
        int i = Mathf.Clamp(Mathf.FloorToInt(boob.Size / 2), 0, Bra.Count - 1);
        return Bra[i] + ((i == Bra.Count - 1 || i < 2) ? "" : "-cup");
    }

    public static string Look(this Boobs boob, bool capital = true) => $"{(capital ? "An" : "an")} {boob.BoobSizeConvertor()} chest";

    public static string Looks(this List<Boobs> boobs)
    {
        if (boobs.Count == 0)
        {
            return "A flat chest";
        }
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < boobs
            .Count; i++)
        {
            Boobs b = boobs[i];
            builder.Append(i == 0 ? b.Look() : b.Look(false));
            if (i == boobs.Count - 2)
            {
                builder.Append(" and ");
            }
            else if (i == boobs.Count - 1)
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