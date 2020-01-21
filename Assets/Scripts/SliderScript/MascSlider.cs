using UnityEngine;

public class MascSlider : EssenceSlider
{
    private Essence Masc => basicChar.Essence.Masc;

    public override void Init(BasicChar who)
    {
        base.Init(who);
        Masc.EssenceSliderEvent += ChangeMasc;
        Masc.ManualUpdate();
    }

    private void OnDisable() => Masc.EssenceSliderEvent -= ChangeMasc;

    private void ChangeMasc()
    {
        Color temp = _image.color;
        temp.a = Masc.Amount > 0 ? 1f : 0.5f;
        _image.color = temp;
        essValue.text = Masc.StringAmount;
    }
}