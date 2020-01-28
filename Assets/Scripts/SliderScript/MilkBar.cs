public class MilkBar : FluidSliders
{
    private void OnEnable()
    {
        if (player != null)
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
        if (player.SexualOrgans.Lactating && player.SexualOrgans.HaveBoobs())
        {
            slider.value = player.SexualOrgans.MilkSlider;
            MilkChange();
        }
    }

    private void MilkChange()
    {
        slider.value = player.SexualOrgans.MilkSlider;
        if (statusText != null)
        {
            statusText.text = Settings.LorGal(player.SexualOrgans.Boobs.MilkTotal() / 1000);
        }
    }
}