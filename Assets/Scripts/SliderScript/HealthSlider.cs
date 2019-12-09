using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        basicChar.HP.ManualSliderUpdate();
    }

    private IEnumerator LetTheGameStart()
    {
        yield return new WaitForEndOfFrame();
        if (basicChar != null)
        {
            basicChar.HP.UpdateSliderEvent += ChangeHealth;
            basicChar.HP.ManualSliderUpdate();
        }
    }

    private void OnEnable()
    {
        _ = StartCoroutine(LetTheGameStart());
    }

    public override void Setup(ThePrey who)
    {
        base.Setup(who);
        basicChar.HP.ManualSliderUpdate();
        // Remove first to ensure no duplicate subs
        basicChar.HP.UpdateSliderEvent -= ChangeHealth;
        basicChar.HP.UpdateSliderEvent += ChangeHealth;
    }
}