public class WillSlider : BasicSlider
{
    private void ChangeWill()
    {
        slider.value = basicChar.WP.SliderValue;
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.WP.Status+ (endSuffix ? suffix : "");
        }
    }

    private void OnDisable()
    {
        Health.UpdateSliderEvent -= ChangeWill;
    }

    private void OnEnable()
    {
        Health.UpdateSliderEvent += ChangeWill;
        if (basicChar != null) { basicChar.WP.ManualSliderUpdate(); }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        basicChar.WP.ManualSliderUpdate();
    }
}