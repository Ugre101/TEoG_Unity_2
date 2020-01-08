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

    public static string VagSizeConventor(this Vagina vag)
    {
        List<string> deepth = new List<string>()
        {
        };
        if (deepth.Count > 0)
        {
            int i = Mathf.Clamp(Mathf.FloorToInt(vag.Size / 2), 0, deepth.Count - 1);
            return deepth[i];
        }
        return vag.Size.ToString();
    }

    public static string Look(this Vagina vag, bool capital = true)
    {
        return $"{(capital ? "A" : "a")} {vag.VagSizeConventor()} {vag.Race}";
    }

    public static string Looks(this List<Vagina> parVags)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < parVags.Count; i++)
        {
            Vagina vag = parVags[i];
            if (i == 0)
            {
                builder.Append(vag.Look());
            }
            else
            {
                builder.Append(vag.Look(false));
            }
            if (i == parVags.Count - 2)
            {
                builder.Append(" and ");
            }
            else if (i == parVags.Count - 1)
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