using UnityEngine;

public class BasicStatButton : PerkTreeBasicBtn
{
    public BasicStatButton()
    {
        Taken = true;
    }

    [Space]
    public StatTypes stat;

    [SerializeField]
    private int statAmount = 1;

    public override void OnEnable()
    {
        base.OnEnable();
        amount.text = player.Stats.GetStat(stat).BaseValue > 0 ? player.Stats.GetStat(stat).BaseValue.ToString() : string.Empty;
    }

    public override void Use()
    {
        if (player.ExpSystem.PerkBool())
        {
            Taken = true;
            player.Stats.GetStat(stat).BaseValue += statAmount;
            amount.text = player.Stats.GetStat(stat).BaseValue.ToString();
        }
    }
}