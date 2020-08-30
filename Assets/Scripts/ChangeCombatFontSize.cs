public class ChangeCombatFontSize : ChangeFontSize
{
    protected override LogFontSize CurrFont => Settings.CombatLogFont;
}