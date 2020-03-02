using UnityEngine;

public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private PerksTypes perk = PerksTypes.EssenceFlow;

    [SerializeField] private Sprite icon = null;

    public override void Start()
    {
        base.Start();
        rune.sprite = icon != null ? icon : null;
    }

    public override void OnEnable()
    {
        if (player != null)
        {
            Taken = player.Perks.HasPerk(perk);
        }
        base.OnEnable();
    }

    public override void Use()
    {
        if (player.Perks.HasPerk(perk) ? player.Perks.NotMaxLevel(perk, perkInfo.MaxLevel) : true)
        {
            if (player.ExpSystem.PerkBool(perkInfo.PerkCost))
            {
                Taken = true;
                player.GainPerk(perk);
            }
        }
    }
}