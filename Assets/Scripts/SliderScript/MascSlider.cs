using UnityEngine;

public class MascSlider : EssenceSlider
{
    public override void Init(BasicChar who)
    {
        base.Init(who);
        who.Essence.Masc.EssenceSliderEvent += changeMasc;
        basicChar.Masc.ManualUpdate();
    }

    private void OnDisable()
    {
        basicChar.Essence.Masc.EssenceSliderEvent -= changeMasc;
    }

    private void changeMasc()
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        essValue.text = basicChar.Masc.StringAmount;
    }
}