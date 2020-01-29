using UnityEngine;

public class WillSlider : BasicSlider
{
    protected override Health Health => basicChar.WP;

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