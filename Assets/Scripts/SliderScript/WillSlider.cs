public class WillSlider : BasicSlider
{
    private void ChangeWill()
    {
        slider.value = basicChar.WP.SliderValue;
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.WP.Status + (endSuffix ? suffix : "");
        }
    }

    private void OnDisable()
    {
        if (basicChar != null)
        {
            basicChar.WP.UpdateSliderEvent -= ChangeWill;
        }
    }

    private void OnEnable()
    {
        if (basicChar != null)
        {
            basicChar.WP.UpdateSliderEvent += ChangeWill;
            ChangeWill();
        }
    }

    protected override void Start()
    {
        base.Start();
        if (basicChar != null)
        {
            basicChar.WP.UpdateSliderEvent += ChangeWill;
            ChangeWill();
        }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        ChangeWill();
        // unsub first to ensure no duplicate subs
        basicChar.WP.UpdateSliderEvent -= ChangeWill;
        basicChar.WP.UpdateSliderEvent += ChangeWill;
    }
}