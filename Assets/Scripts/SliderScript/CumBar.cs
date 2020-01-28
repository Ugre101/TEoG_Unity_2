public class CumBar : FluidSliders
{
    private void OnEnable()
    {
        Setup();
    }

    public override void Setup()
    {
        SexualFluid.FluidSlider += CumChange;
        if (player.SexualOrgans.HaveBalls())
        {
            CumChange();
        }
    }

    private void OnDisable()
    {
        SexualFluid.FluidSlider -= CumChange;
    }

    private void CumChange()
    {
        slider.value = player.SexualOrgans.CumSlider;
        if (statusText != null)
        {
            statusText.text = Settings.LorGal(player.SexualOrgans.Balls.CumTotal() / 1000);
        }
    }
}