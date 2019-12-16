public class FemiSlider : EssenceSlider
{
    public override void Init(BasicChar who)
    {
        base.Init(who);
        who.Essence.Femi.EssenceSliderEvent += ChangeFemi;
        basicChar.Femi.ManualUpdate();
    }

    private void OnDisable()
    {
        basicChar.Essence.Femi.EssenceSliderEvent -= ChangeFemi;
    }

    private void ChangeFemi()
    {
        essValue.text = basicChar.Femi.StringAmount;
    }
}