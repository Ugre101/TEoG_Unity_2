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

    public static string Looks(this Dick parDick, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} {Settings.MorInch(parDick.Size)} long {parDick.Race} dick.";
    }

    public static string Looks(this List<Dick> parDicks)
    {
        StringBuilder builder = new StringBuilder();
        foreach (Dick dick in parDicks)
        {
            builder.Append(dick.Looks() + "\n");
        }
        return builder.ToString();
    }
}