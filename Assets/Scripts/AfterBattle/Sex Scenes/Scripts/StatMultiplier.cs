using UnityEngine;

[System.Serializable]
public class StatMultiplier
{
    [Header("Stat * multiplier; so 0.1f multi with 20str = 2f multi")]
    [SerializeField] private StatTypes stat;

    [SerializeField] private float multiplier = 0.1f;

    public StatMultiplier(StatTypes stat, float multiplier)
    {
        this.stat = stat;
        this.multiplier = multiplier;
    }

    public StatTypes Stat { get => stat; }
    public float Multiplier { get => multiplier; }
}