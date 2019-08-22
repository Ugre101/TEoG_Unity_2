using UnityEngine;

public class FemiSlider : EssenceSlider
{
    private void OnEnable()
    {
        Essence.essenceSlider += changeFemi;
        basicChar.Femi.ManualUpdate();
    }

    private void OnDisable()
    {
        Essence.essenceSlider -= changeFemi;
    }

    private void changeFemi()
    {
        essValue.text = basicChar.Femi.StringAmount;
    }
}