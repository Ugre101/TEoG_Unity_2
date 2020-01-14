using UnityEngine;

public class FemiSlider : EssenceSlider
{
    public override void Init(BasicChar who)
    {
        base.Init(who);
        who.Essence.Femi.EssenceSliderEvent += ChangeFemi;
        basicChar.Essence.Femi.ManualUpdate();
    }

    private void OnDisable()
    {
        basicChar.Essence.Femi.EssenceSliderEvent -= ChangeFemi;
    }

    private void ChangeFemi()
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        essValue.text = basicChar.Essence.Femi.StringAmount;
    }
}