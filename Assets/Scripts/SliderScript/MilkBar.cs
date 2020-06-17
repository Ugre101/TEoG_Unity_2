public class MilkBar : FluidSliders
{
    private void OnEnable()
    {
        if (Player != null)
        {
            Setup();
        }
    }

    private void OnDisable()
    {
        SexualFluid.FluidSlider -= MilkChange;
    }

    public override void Setup()
    {
        SexualFluid.FluidSlider += MilkChange;
        if (Player.SexualOrgans.Boobs.Lactating && Player.SexualOrgans.HaveBoobs())
        {
            slider.value = Player.SexualOrgans.Boobs.FluidSlider;
            MilkChange();
        }
    }

    private void MilkChange()
    {
        slider.value = Player.SexualOrgans.Boobs.FluidSlider;
        if (statusText != null)
        {
            statusText.text = Player.SexualOrgans.Boobs.FluidStatus;
        }
    }
}