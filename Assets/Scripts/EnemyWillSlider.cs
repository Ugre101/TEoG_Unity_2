public class EnemyWillSlider : BasicSlider
{
    public void Init(BasicChar enemy)
    {
        basicChar = enemy;
        Health.updateSlider += changeWill;
        basicChar.WP.manualSliderUpdate();
    }

    private void OnDisable()
    {
        Health.updateSlider -= changeWill;
    }

    private void changeWill()
    {
        slider.value = basicChar.WP.Slider();
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.WP.Status();
        }
    }
}