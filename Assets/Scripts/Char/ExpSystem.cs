using UnityEngine;

[System.Serializable]
public class ExpSystem
{
    [SerializeField]
    private int level = 0;
    public int Level { get { return level; } set { level = value; } }
    [SerializeField]
    private int exp = 0;

    public int Exp
    {
        get { return exp; }
        set
        {
            exp += value;
            while (exp > MaxExp())
            {
                exp -= MaxExp();
                level++;
                statPoints += 3;
                perkPoints++;
            }
            expChange?.Invoke();
        }
    }
    [SerializeField]
    private int perkPoints = 0;
    public int PerkPoints { get { return perkPoints; } set { perkPoints += value; } }
    [SerializeField]
    private int statPoints = 0;
    public int StatPoints { get { return statPoints; } set { statPoints += value; } }

    public float ExpSlider()
    {
        return (float)Exp / (float)MaxExp();
    }

    public string ExpStatus()
    {
        return $"{Exp}/{MaxExp()}";
    }

    private int MaxExp()
    {
        return (int)Mathf.Round(30f * Mathf.Pow(1.05f, level - 1f));
    }

    public string LevelStatus()
    {
        return $"Level: {level}";
    }

    public delegate void ExpChange();

    public static event ExpChange expChange;

    public void manualExpUpdate()
    {
        expChange();
    }
}