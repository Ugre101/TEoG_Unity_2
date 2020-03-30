public class ChangeCombatFontSize : ChangeFontSize
{
    protected override float CurrSize => Settings.CombatLogFontSize;

    protected override float DownSized => Settings.CombatLogFontSizeDown;

    protected override float UpSized => Settings.CombatLogFontSizeUp;
}