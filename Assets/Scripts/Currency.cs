using UnityEngine;

[System.Serializable]
public class Currency
{
    [SerializeField]
    private float gold;

    public float Gold
    {
        get => Mathf.FloorToInt(gold);
        set
        {
            gold = Mathf.Max(0, value);
            GoldChanged?.Invoke();
        }
    }
    /// <summary> Checks if you can afford it. </summary>
    public bool CanAfford(int cost) => cost <= Gold;

    /// <summary> Check if you can afford it and if you do then buy it. </summary>
    public bool TryToBuy(int cost)
    {
        if (CanAfford(cost))
        {
            Gold -= cost;
            return true;
        }
        return false;
    }
    public delegate void GoldChange();

    public event GoldChange GoldChanged;
}