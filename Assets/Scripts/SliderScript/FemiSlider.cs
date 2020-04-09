using UnityEngine;

public class FemiSlider : EssenceSlider
{
    protected override Essence Ess => basicChar.Essence.Femi;

    protected override void ChangeEss()
    {
        Color temp = _image.color;
        temp.a = Ess.Amount > 0 ? 1f : 0.5f;
        _image.color = temp;
        essValue.text = Ess.StringAmount;
    }
}