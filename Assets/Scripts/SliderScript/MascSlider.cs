using UnityEngine;

public class MascSlider : EssenceSlider
{
    private void OnEnable()
    {
        if (_image == null || basicChar == null || essValue == null)
        {
            this.enabled = false;
        }
        Essence.essenceSlider += changeMasc;
        basicChar.Masc.ManualUpdate();
    }

    private void OnDisable()
    {
        Essence.essenceSlider -= changeMasc;
    }

    private void changeMasc()
    {
        Color temp = _image.color;
        temp.a = 0.5f;
        _image.color = temp;
        essValue.text = basicChar.Masc.StringAmount;
    }
}