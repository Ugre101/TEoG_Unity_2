[System.Serializable]
public abstract class OrganWithFluidContainer : OrganContainer
{
    public abstract float FluidSlider { get; }
    public abstract string FluidStatus { get; }
    public abstract string LooksWithOutFluids { get; }
}
