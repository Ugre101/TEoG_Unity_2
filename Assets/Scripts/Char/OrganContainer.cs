[System.Serializable]
public abstract class OrganContainer
{
    protected OrganContainer() => Save.LoadEvent += ReBind;

    public abstract void ReBind();

    public void UnBind() => Save.LoadEvent -= ReBind;

    public abstract void AddNew();

    public abstract void AddNew(int baseSize = 1);

    public abstract string Looks { get; }

    public abstract float ReCycle();

    public abstract float AddCost { get; }

    public abstract float BiggestSizeValue { get; }
    public string BiggestSizeMorInch => BiggestSizeValue.MorInch();

    public delegate void OrganChanged();

    public event OrganChanged Change;

    public void InvokeOrganChange() => Change?.Invoke();
}
