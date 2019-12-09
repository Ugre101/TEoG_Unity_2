using System.Collections;
using UnityEngine;

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

    private IEnumerator LetTheGameStart()
    {
        yield return new WaitForEndOfFrame();
        if (basicChar != null)
        {
            basicChar.WP.UpdateSliderEvent += ChangeWill;
            basicChar.WP.ManualSliderUpdate();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(LetTheGameStart());
    }

    public override void Setup(ThePrey who)
    {
        base.Setup(who);
        basicChar.WP.ManualSliderUpdate();
        // unsub first to ensure no duplicate subs
        basicChar.WP.UpdateSliderEvent -= ChangeWill;
        basicChar.WP.UpdateSliderEvent += ChangeWill;
    }
}