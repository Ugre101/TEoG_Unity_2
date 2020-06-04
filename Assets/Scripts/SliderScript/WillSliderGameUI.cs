using System.Collections;
using UnityEngine;

public class WillSliderGameUI : BasicSlider
{
    protected override Health Health => basicChar.WP;
    private bool Started = false;

    private void OnEnable()
    {
        if (Started)
        {
            Health.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    private void OnDisable()
    {
        if (Started)
        {
            Health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    protected override void Start()
    {
        base.Start();
        basicChar = basicChar != null ? basicChar : PlayerHolder.Player;
        Started = true;
        StartCoroutine("WaitToStart");

    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForEndOfFrame();
        Health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}