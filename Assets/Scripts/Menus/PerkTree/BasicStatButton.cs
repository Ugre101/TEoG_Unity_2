using UnityEngine;

public class BasicStatButton : PerkTreeBasicBtn
{
    public BasicStatButton()
    {
        Taken = true;
    }

    [Space]
    [SerializeField]
    private StatTypes stat;

    [SerializeField]
    private int statAmount = 1;

    public override void OnEnable()
    {
        base.OnEnable();
        int baseValue = player.Stats.GetStat(stat).BaseValue;
        amount.text = baseValue > 0 ? baseValue.ToString() : string.Empty;
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