using System.Collections.Generic;
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

    public override float Cost => this.GrowCost();
}

public static class DickExtensions
{
    public static Dick Biggest(this DickContainer dick) => dick.List.Find(so => so.Size == dick.List.BiggestSize());

    public static float GrowCost(this Dick dick) => Mathf.Ceil(Mathf.Min(2000, 30 * Mathf.Pow(1.05f, dick.BaseSize)));

    public static string Look(this Dick parDick, bool capital = true)
        => $"{(capital ? "A" : "a")} {Settings.MorInch(parDick.Size)} long {parDick.Race} dick";

}