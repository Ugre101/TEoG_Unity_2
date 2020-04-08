using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Anal : SexualOrganWithFluid
{
    public Anal() : base(FluidType.Scat)
    {
    }

    public Anal(int size) : base(FluidType.Scat, size)
    {
    }

    public override float Cost => throw new NotImplementedException();
}

public static class AnalExtension
{
    public static Anal Biggest(this List<Anal> list) => list.Find(so => so.Size == list.BiggestSize());

    public static float GrowCost(this Anal anals) => Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, anals.BaseSize)));

    public static void AddAnal(this List<Anal> anals) => anals.Add(new Anal());

    public static void AddAnal(this List<Anal> anals, int parSize) => anals.Add(new Anal(parSize));

    public static float Cost(this List<Anal> anal) => Mathf.Round(30 * Mathf.Pow(4, anal.Count));

    public static string Defecate(this Anal anal) => $"";

    public static string Look(this Anal anal, bool capaital = true) => $"";

    public static string LookWithFluid(this Anal anal, bool capital = true) => $"";

    public static string Looks(this List<Anal> anals, bool withFluid = false)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < anals.Count; i++)
        {
            Anal anal = anals[i];
            if (i == 0)
            {
                sb.Append(withFluid ? anal.Look() : anal.LookWithFluid());
            }
            else
            {
                sb.Append(withFluid ? anal.Look(false) : anal.LookWithFluid(false));
            }
            if (i == anals.Count - 2)
            {
                sb.Append(" and ");
            }
            else if (i == anals.Count - 1)
            {
                sb.Append(".");
            }
            else
            {
                sb.Append(", ");
            }
        }
        return $"";
    }
}