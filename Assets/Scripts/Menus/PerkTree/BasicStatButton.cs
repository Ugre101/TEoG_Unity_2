using UnityEngine;

public class BasicStatButton : PerkTreeBasicBtn
{
    [Space]
    public StatType stat;

    [SerializeField]
    private int statAmount = 1;

    public override void OnEnable()
    {
        base.OnEnable();
        amount.text = player.Stats.GetStat(stat)._baseValue > 0 ? player.Stats.GetStat(stat)._baseValue.ToString() : string.Empty;
    }

    public override void Use()
    {
        if (player.PerkBool)
        {
            taken = true;
            player.Stats.GetStat(stat)._baseValue += statAmount;
            amount.text = player.Stats.GetStat(stat)._baseValue.ToString();
        }
    }
}