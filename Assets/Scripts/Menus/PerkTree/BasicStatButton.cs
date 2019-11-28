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
        amount.text = player.Stats.GetStat(stat).baseValue > 0 ? player.Stats.GetStat(stat).baseValue.ToString() : string.Empty;
    }

    public override void Use()
    {
        if (player.ExpSystem.PerkBool())
        {
            taken = true;
            player.Stats.GetStat(stat).baseValue += statAmount;
            amount.text = player.Stats.GetStat(stat).baseValue.ToString();
        }
    }
}