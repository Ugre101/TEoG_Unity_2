using System;

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
