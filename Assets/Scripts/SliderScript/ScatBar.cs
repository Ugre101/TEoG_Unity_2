public class ScatBar : FluidSliders
{
    private void OnEnable() => Setup();

    public override void Setup()
    {
        if (Settings.Scat)
        {
            SexualFluid.FluidSlider += ScatChange;
            ScatChange();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable() => SexualFluid.FluidSlider -= ScatChange;

    private void ScatChange()
    {
        slider.value = Player.SexualOrgans.Anals.FluidSlider;
        if (statusText != null)
        {
            statusText.text = Player.SexualOrgans.Anals.FluidStatus;
        }
    }
}