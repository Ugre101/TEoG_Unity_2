using UnityEngine;

public class BasicStatButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private StatTypes stat = StatTypes.Charm;

    [SerializeField] private int statGainAmount = 1;
    private int BaseValue { get => player.Stats.GetStat(stat).BaseValue; set => player.Stats.GetStat(stat).BaseValue = value; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Taken = BaseValue > 0;
        amount.text = BaseValue > 0 ? BaseValue.ToString() : string.Empty;
    }

    protected override void Use()
    {
        if (player.ExpSystem.PerkBool())
        {
            Taken = true;
            BaseValue += statGainAmount;
            amount.text = BaseValue.ToString();
        }
    }
}