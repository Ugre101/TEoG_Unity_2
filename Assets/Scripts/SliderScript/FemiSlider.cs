using UnityEngine;

public class FemiSlider : EssenceSlider
{
    public override void Init(ThePrey who)
    {
        base.Init(who);
        Essence.EssenceSliderEvent += changeFemi;
        basicChar.Femi.ManualUpdate();
    }

    private void OnDisable()
    {
        Essence.EssenceSliderEvent -= changeFemi;
    }

    private void changeFemi()
    {
        essValue.text = basicChar.Femi.StringAmount;
    }
}