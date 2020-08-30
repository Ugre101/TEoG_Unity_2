using UnityEngine;

[System.Serializable]
public class ExpSystem
{
    public ExpSystem()
    {
    }

    public ExpSystem(int startLevel) => level = startLevel;

    [SerializeField] private int level = 1;

    public int Level => level;

    [SerializeField] private int exp = 0;

    public int Exp => exp;

    public void GainExp(int value)
    {
        exp += Mathf.Max(0, value);
        while (exp > MaxExp)
        {
            exp -= MaxExp;
            level++;
            PerkPoints += 3;
        }
        ExpChangeEvent?.Invoke();
    }

    [SerializeField] private int perkPoints = 0;

    public int PerkPoints
    {
        get => perkPoints; private set
        {
            perkPoints = value;
            PerkPointsChange?.Invoke();
        }
    }

    /// <summary> Show amount of parkpoints, note you can only add extra perkpoints not remove. This is to avoid getting negative amount of points. All </summary>

    public bool PerkBool(int parCost = 1)
    {
        if (PerkPoints >= parCost)
        {
            PerkPoints -= parCost;
            return true;
        }
        return false;
    }

    public float ExpSlider => Exp / (float)MaxExp;

    public string ExpStatus => $"{Exp}/{MaxExp}";

    private int MaxExp => Mathf.RoundToInt(30f * Mathf.Pow(1.05f, Mathf.Max(0, level - 1f)));

    public string LevelStatus => $"Level: {Level}";

    public delegate void ExpChange();

    public event ExpChange ExpChangeEvent;

    public event ExpChange PerkPointsChange;

    public void ManualExpUpdate() => ExpChangeEvent?.Invoke();
}