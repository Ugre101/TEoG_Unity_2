using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFemiSlider : EssenceSlider
{
    public void Init(BasicChar whom)
    {
        basicChar = whom;
        Essence.essenceSlider += changeFemi;
        basicChar.Femi.ManualUpdate();
    }

    private void OnDisable()
    {
        Essence.essenceSlider -= changeFemi;
    }

    private void changeFemi()
    {
        essValue.text = basicChar.Femi.Amount.ToString();
    }
}
