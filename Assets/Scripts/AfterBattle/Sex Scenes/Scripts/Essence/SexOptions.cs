public abstract class SexOptions
{
    public virtual bool HaveOption(PlayerMain player) => true;
    public virtual string ToggleOption() => "option: " + false.ToString();
}
