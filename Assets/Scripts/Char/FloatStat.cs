using UnityEngine;

[System.Serializable]
public abstract class FloatStat
{
    [SerializeField] protected float baseValue;
    public virtual float BaseValue { get => baseValue; set { baseValue = value; IsDirty = true; } }
    protected float lastValue;
    protected bool isDirty = true;
    protected virtual bool IsDirty { get => isDirty; set => isDirty = value; }

    public virtual float Value
    {
        get
        {
            if (IsDirty)
            {
                lastValue = CalcValue();
                IsDirty = false;
            }
            return lastValue;
        }
    }

    protected abstract float CalcValue();
}