using System.Collections;
using UnityEngine;

public class HealthSliderGameUI : BasicSlider
{
    protected override Health health => basicChar.HP;
    private bool started = false;

    private void OnEnable()
    {
        if (started)
        {
            health.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    private void OnDisable()
    {
        if (started)
        {
            health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    protected override void Start()
    {
        base.Start();
        basicChar = basicChar != null ? basicChar : PlayerMain.GetPlayer;
        started = true;
        StartCoroutine("WaitToStart");
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForEndOfFrame();
        health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}