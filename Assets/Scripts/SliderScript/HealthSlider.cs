public class HealthSlider : BasicSlider
{
    private void changeHealth()
    {
        slider.value = basicChar.HP.Slider();
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.HP.Status();
            if (endSuffix)
            {
                TextMesh.text += suffix;
            }
        }
    }

    private void OnDisable()
    {
        Health.updateSlider -= changeHealth;
    }

    private void OnEnable()
    {
        Health.updateSlider += changeHealth;
        if (basicChar != null)
        {
            basicChar.HP.manualSliderUpdate();
        }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        basicChar.HP.manualSliderUpdate();
    }
}