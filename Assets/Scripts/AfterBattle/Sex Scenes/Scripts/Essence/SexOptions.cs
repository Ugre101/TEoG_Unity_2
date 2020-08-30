public abstract class SexOptions
{
    public virtual bool HaveOption(BasicChar player) => true;
    public virtual string ToggleOption() => "option: " + false.ToString();
}
