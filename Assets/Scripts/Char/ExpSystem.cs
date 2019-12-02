using UnityEngine;

[System.Serializable]
public class ExpSystem
{
    [SerializeField]
    private int level = 0;

    // TODO decide if set level should be public or not, problem is setting level of enemies will be harder
    // if it isn't
    public int Level { get => level; set { level = value; } }

    [SerializeField]
    private int exp = 0;

    public int Exp
    {
        get => exp;
        set
        {
            exp += Mathf.Max(0, value);
            while (exp > MaxExp)
            {
                exp -= MaxExp;
                level++;
                statPoints += 3;
                perkPoints++;
            }
            ExpChangeEvent?.Invoke();
        }
    }

    [SerializeField]
    private int perkPoints = 0;

    /// <summary>
    /// Show amount of parkpoints, note you can only add extra perkpoints not remove. This is to avoid
    /// getting negative amount of points. All 
    /// </summary>
    public int PerkPoints { get => perkPoints; set { perkPoints += Mathf.Max(0,value); } }

    public bool PerkBool(int parCost = 1)
    {
        if (perkPoints >= parCost)
        {
            perkPoints -= parCost;
            return true;
        }
        return false;
    }

    [SerializeField]
    private int statPoints = 0;

    public int StatPoints => statPoints;

    public bool StatBool
    {
        get
        {
            if (statPoints > 0)
            {
                statPoints--;
                return true;
            }
            return false;
        }
    }

    public float ExpSlider => Exp / (float)MaxExp;

    public string ExpStatus => $"{Exp}/{MaxExp}";

    private int MaxExp => Mathf.RoundToInt(30f * Mathf.Pow(1.05f, level - 1f));

    public string LevelStatus => $"Level: {level}";

    public delegate void ExpChange();

    public static event ExpChange ExpChangeEvent;

    public void ManualExpUpdate() => ExpChangeEvent?.Invoke();
}