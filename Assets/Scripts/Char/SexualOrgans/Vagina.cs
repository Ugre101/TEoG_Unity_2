using UnityEngine;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public class Vagina : SexualOrgan
{
    [SerializeField]
    private Womb womb = new Womb();

    public Womb Womb => womb;
}

public static class VaginaExtensions
{
    public static void AddVag(this List<Vagina> vaginas)
    {
        vaginas.Add(new Vagina());
    }

    public static float Cost(this List<Vagina> vaginas)
    {
        return Mathf.Round(30 * Mathf.Pow(4, vaginas.Count));
    }

    public static float ReCycle(this List<Vagina> vaginas)
    {
        Vagina toShrink = vaginas[vaginas.Count - 1];
        if (toShrink.Shrink())
        {
            vaginas.Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }
    public static string Looks(this Vagina vag, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} ";
    }
    public static string Looks(this List<Vagina> parVags)
    {
        StringBuilder builder = new StringBuilder();
        foreach (Vagina vag in parVags)
        {
            builder.Append(vag.Looks() + "\n");
        }
        return builder.ToString();
    }
}