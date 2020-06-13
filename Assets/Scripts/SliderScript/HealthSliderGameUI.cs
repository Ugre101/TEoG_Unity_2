using System.Collections;
using UnityEngine;

public class HealthSliderGameUI : BasicSlider
{
    protected override Health Health => basicChar.HP;
    private bool started = false;

    private void OnEnable()
    {
        if (started)
        {
            Health.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    private void OnDisable()
    {
        if (started)
        {
            Health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    protected override void Start()
    {
        base.Start();
        basicChar = basicChar != null ? basicChar : PlayerHolder.Player;
        started = true;
        StartCoroutine("WaitToStart");
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForEndOfFrame();
        Health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}