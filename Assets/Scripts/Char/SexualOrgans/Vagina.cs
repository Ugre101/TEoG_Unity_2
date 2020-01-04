using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Vagina : SexualOrgan
{
    public Vagina() : base()
    {
    }

    public Vagina(int parBase) : base(parBase)
    {
    }

    [SerializeField]
    private Womb womb = new Womb();

    public Womb Womb => womb;
}

public static class VaginaExtensions
{
    public static void AddVag(this List<Vagina> vaginas) => vaginas.Add(new Vagina());

    public static void AddVag(this List<Vagina> vaginas, int parSize) => vaginas.Add(new Vagina(parSize));

    public static float Cost(this List<Vagina> vaginas) => Mathf.Round(30 * Mathf.Pow(4, vaginas.Count));

    public static float ReCycle(this List<Vagina> vaginas)
    {
        Vagina toShrink = vaginas[vaginas.Count - 1];
        if (toShrink.Shrink())
        {
            vaginas.Remove(toShrink);
            return 30f;
        }
        return toShrink.Cost;
    }

    public static bool EmptyWomb(this List<Vagina> vaginas) => vaginas.Exists(v => !v.Womb.HasFetus);

    public static string Looks(this Vagina vag, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} {vag.Race}";
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