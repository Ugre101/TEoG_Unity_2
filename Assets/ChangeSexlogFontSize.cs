public class ChangeSexlogFontSize : ChangeFontSize
{
    protected override float CurrSize => Settings.SexlogFontSize;

    protected override float DownSized => Settings.SexlogFontSizeDown;

    protected override float UpSized => Settings.SexlogFontSizeUp;
}