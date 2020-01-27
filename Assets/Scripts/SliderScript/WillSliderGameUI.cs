using System.Collections;
using UnityEngine;

public class WillSliderGameUI : BasicSlider
{
    protected override Health health => basicChar.WP;
    private bool Started = false;

    private void OnEnable()
    {
        if (Started)
        {
            health.UpdateSliderEvent += ChangeHealth;
            ChangeHealth();
        }
    }

    private void OnDisable()
    {
        if (Started)
        {
            health.UpdateSliderEvent -= ChangeHealth;
        }
    }

    protected override void Start()
    {
        base.Start();
        basicChar = basicChar != null ? basicChar : PlayerMain.GetPlayer;
        Started = true;
        StartCoroutine("WaitToStart");

    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForEndOfFrame();
        health.UpdateSliderEvent += ChangeHealth;
        ChangeHealth();
    }
}