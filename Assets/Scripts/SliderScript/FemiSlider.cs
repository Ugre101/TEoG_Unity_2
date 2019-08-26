using UnityEngine;

public class FemiSlider : EssenceSlider
{
    public override void Init(BasicChar who)
    {
        base.Init(who);
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