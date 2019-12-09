using UnityEngine;

public class MascSlider : EssenceSlider
{
    public override void Init(ThePrey who)
    {
        base.Init(who);
        Essence.EssenceSliderEvent += changeMasc;
        basicChar.Masc.ManualUpdate();
    }

    private void OnDisable()
    {
        Essence.EssenceSliderEvent -= changeMasc;
    }

    private void changeMasc()
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        essValue.text = basicChar.Masc.StringAmount;
    }
}