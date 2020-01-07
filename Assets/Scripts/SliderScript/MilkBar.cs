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
        if (player.SexualOrgans.Lactating && player.SexualOrgans.Boobs.Count > 0)
        {
            slider.value = player.SexualOrgans.MilkSlider;
            player.SexualOrgans.Boobs[0].Fluid.ManualSlider();
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