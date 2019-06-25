using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMascSlider : EssenceSlider
{
    public void Init(BasicChar whom)
    {
        basicChar = whom;
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
        essValue.text = basicChar.Masc.Amount.ToString();
    }
}
