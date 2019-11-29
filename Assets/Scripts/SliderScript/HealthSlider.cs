public class HealthSlider : BasicSlider
{
    private void ChangeHealth()
    {
        slider.value = basicChar.HP.SliderValue;
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.HP.Status+ (endSuffix ? suffix : "");
        }
    }

    private void OnDisable()
    {
        Health.UpdateSliderEvent -= ChangeHealth;
    }

    private void OnEnable()
    {
        Health.UpdateSliderEvent += ChangeHealth;
        if (basicChar != null) { basicChar.HP.ManualSliderUpdate(); }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        basicChar.HP.ManualSliderUpdate();
    }
}