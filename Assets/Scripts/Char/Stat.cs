using UnityEngine;

[System.Serializable]
public abstract class Stat
{
    [SerializeField] protected int baseValue;
    public virtual int BaseValue { get => baseValue; set { baseValue = value; IsDirty = true; } }
    protected int lastValue;
    protected bool isDirty = true;
    protected virtual bool IsDirty { get => isDirty; set { isDirty = value; } }

    public virtual int Value
    {
        get
        {
            if (IsDirty)
            {
                lastValue = CalcValue;
                IsDirty = false;
            }
            return lastValue;
        }
    }

    protected virtual int CalcValue => Mathf.FloorToInt(baseValue);
}