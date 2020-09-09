public class ScatBar : FluidSliders
{
    private bool _isstatusTextNotNull;


    private void OnEnable() => Setup();

    public override void Setup()
    {
        if (Settings.Scat)
        {
            SexualFluid.FluidSlider += ScatChange;
            ScatChange();
        }
        else
            gameObject.SetActive(false);

        _isstatusTextNotNull = statusText != null;
    }

    private void OnDisable() => SexualFluid.FluidSlider -= ScatChange;

    private void ScatChange()
    {
        slider.value = Player.SexualOrgans.Anals.FluidSlider;
        if (_isstatusTextNotNull) statusText.text = Player.SexualOrgans.Anals.FluidStatus;
    }
}