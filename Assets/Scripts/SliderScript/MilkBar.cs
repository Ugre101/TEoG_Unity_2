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
        if (Player.SexualOrgans.Lactating && Player.SexualOrgans.HaveBoobs())
        {
            slider.value = Player.SexualOrgans.MilkSlider;
            MilkChange();
        }
    }

    private void MilkChange()
    {
        slider.value = Player.SexualOrgans.MilkSlider;
        if (statusText != null)
        {
            statusText.text = Settings.LorGal(Player.SexualOrgans.Boobs.FluidCurrentTotal() / 1000);
        }
    }
}