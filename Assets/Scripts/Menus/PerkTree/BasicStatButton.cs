using UnityEngine;
using UnityEngine.EventSystems;

public class BasicStatButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private StatInfo statInfo = null;

    private CharStats GetStat => player.Stats.GetStat(statInfo.Stat);
    private int BaseValue { get => GetStat.BaseValue; set => GetStat.BaseValue = value; }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (statInfo == null)
        {
            gameObject.SetActive(false);
        }
        Taken = BaseValue > 0;
        amount.text = BaseValue > 0 ? BaseValue.ToString() : string.Empty;
    }

    protected override void Use()
    {
        if (player.ExpSystem.PerkBool())
        {
            Taken = true;
            BaseValue++;
            amount.text = BaseValue.ToString();
        }
    }

    protected override void Hovering() => PerkTreeHoverText.Hovering(statInfo.Info, statInfo.Effects);
}