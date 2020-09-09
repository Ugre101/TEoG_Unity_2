public class CumBar : FluidSliders
{
    private bool _isstatusTextNotNull;

    private void OnEnable() => Setup();

    public override void Setup()
    {
        SexualFluid.FluidSlider += CumChange;
        if (Player.SexualOrgans.HaveBalls()) CumChange();
        _isstatusTextNotNull = statusText != null;
    }

    private void OnDisable() => SexualFluid.FluidSlider -= CumChange;

    private void CumChange()
    {
        slider.value = Player.SexualOrgans.Balls.FluidSlider;
        if (_isstatusTextNotNull) statusText.text = Player.SexualOrgans.Balls.FluidStatus;
    }
}