using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Dick : SexualOrgan
{
    public Dick() : base()
    {
    }

    public Dick(int size) : base(size)
    {
    }
}

public static class DickExtensions
{
    public static void AddDick(this List<Dick> dicks) => dicks.Add(new Dick());

    public static void AddDick(this List<Dick> dicks, int parSize) => dicks.Add(new Dick(parSize));

    public static float Cost(this List<Dick> dicks) => Mathf.Round(30 * Mathf.Pow(4, dicks.Count));

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

    public static string Look(this Dick parDick, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} {Settings.MorInch(parDick.Size)} long {parDick.Race} dick";
    }

    public static string Looks(this List<Dick> parDicks)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < parDicks.Count; i++)
        {
            Dick dick = parDicks[i];
            if (i == 0)
            {
                builder.Append(dick.Look());
            }
            else
            {
                builder.Append(dick.Look(false));
            }
            if (i == parDicks.Count - 2)
            {
                builder.Append(" and ");
            }
            else if (i == parDicks.Count - 1)
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