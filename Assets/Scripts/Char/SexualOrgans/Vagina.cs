using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Vagina : SexualOrganWithFluid
{
    public Vagina() : base(FluidType.VaginaFluids)
    {
    }

    public Vagina(int parBase) : base(FluidType.VaginaFluids, parBase)
    {
    }

    public override float Cost => this.GrowCost();

    [SerializeField] private Womb womb = new Womb();

    public Womb Womb => womb;
}

public static class VaginaExtensions
{
    public static Vagina Biggest(this List<Vagina> list) => list.Find(so => so.Size == list.BiggestSize());

    public static float GrowCost(this Vagina vagina) => Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, vagina.BaseSize)));

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

    public static string Look(this Vagina vag, bool capital = true) => $"{(capital ? "A" : "a")} {vag.VagSizeConventor()} {vag.Race}";

}