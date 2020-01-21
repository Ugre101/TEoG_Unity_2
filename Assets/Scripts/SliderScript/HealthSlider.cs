public class HealthSlider : BasicSlider
{
    private void ChangeHealth()
    {
        slider.value = basicChar.HP.SliderValue;
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.HP.Status + (endSuffix ? suffix : "");
        }
    }

    private void OnDisable()
    {
        if (basicChar != null)
        {
            basicChar.HP.UpdateSliderEvent -= ChangeHealth;
        }
    }

    private void OnEnable()
    {
        if (basicChar != null)
        {
            basicChar.HP.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    protected override void Start()
    {
        base.Start();
        if (basicChar != null)
        {
            basicChar.HP.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        ChangeHealth();
        // Remove first to ensure no duplicate subs
        basicChar.HP.UpdateSliderEvent -= ChangeHealth;
        basicChar.HP.UpdateSliderEvent += ChangeHealth;
    }
}