using UnityEngine;

public class WillSlider : BasicSlider
{
    protected override Health health => basicChar.WP;

    private void OnDisable()
    {
        if (basicChar != null)
        {
            health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    private void OnDestroy()
    {
        if (basicChar != null)
        {
            health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    public override void Setup(BasicChar who)
    {
        base.Setup(who);
        health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}