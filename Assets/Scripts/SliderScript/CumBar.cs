public class CumBar : FluidSliders
{
    private void OnEnable()
    {
        Setup();
    }

    public override void Setup()
    {
        SexualFluid.FluidSlider += CumChange;
        if (Player.SexualOrgans.HaveBalls())
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
        slider.value = Player.SexualOrgans.CumSlider;
        if (statusText != null)
        {
            statusText.text = Settings.LorGal(Player.SexualOrgans.Balls.FluidCurrentTotal() / 1000);
        }
    }
}