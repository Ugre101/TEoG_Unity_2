using UnityEngine;

[System.Serializable]
public abstract class IntStat
{
    [SerializeField] protected int baseValue;
    public virtual int BaseValue { get => baseValue; set { baseValue = value; IsDirty = true; } }
    protected int lastValue;
    protected bool isDirty = true;
    protected virtual bool IsDirty { get => isDirty; set => isDirty = value; }

    public virtual int MaxValue
    {
        get
        {
            if (IsDirty)
            {
                lastValue = GetCalcValue();
                IsDirty = false;
            }
            return lastValue;
        }
    }

    protected abstract int GetCalcValue();
}
