public class TempEffect : BaseEffect
{
    private DisplayMod mod;

    public void Setup(DisplayMod parMod, GameUIHoverText hoverText)
    {
        this.hoverText = hoverText;
        mod = parMod;
        DisplayTimeLeft();
        DateSystem.NewHourEvent += DisplayTimeLeft;
    }

    private void OnDestroy() => DateSystem.NewHourEvent -= DisplayTimeLeft;

    private void DisplayTimeLeft() => text.text = $"{mod.Duration}";

    protected override void Hovering() => hoverText.Hovering(mod.Source, mod.Desc());
}