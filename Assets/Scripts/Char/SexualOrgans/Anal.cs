using System;
using System.Collections.Generic;

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
    public static string Defecate(this Anal anal) => $"";

    public static string Look(this Anal anal) => $"";

    public static string Looks(this List<Anal> anals) => $"";
}