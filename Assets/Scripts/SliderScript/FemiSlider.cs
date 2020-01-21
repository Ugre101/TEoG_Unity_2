using UnityEngine;

public class FemiSlider : EssenceSlider
{
    private Essence Femi => basicChar.Essence.Femi;

    public override void Init(BasicChar who)
    {
        base.Init(who);
        Femi.EssenceSliderEvent += ChangeFemi;
        Femi.ManualUpdate();
    }

    private void OnDisable() => Femi.EssenceSliderEvent -= ChangeFemi;

    private void ChangeFemi()
    {
        Color temp = _image.color;
        temp.a = Femi.Amount > 0 ? 1f : 0.5f;
        _image.color = temp;
        essValue.text = Femi.StringAmount;
    }
}