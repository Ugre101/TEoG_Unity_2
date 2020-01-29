public class HealthSlider : BasicSlider
{
    protected override Health Health => basicChar.HP;

    private void OnDisable()
    {
        if (basicChar != null)
        {
            Health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    private void OnDestroy()
    {
        if (basicChar != null)
        {
            Health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        Health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}