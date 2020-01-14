using UnityEngine;

public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField]
    private PerksTypes perk = PerksTypes.EssenceFlow;

    [SerializeField]
    private Sprite icon = null;

    [SerializeField]
    private int perkCost = 1;

    public override void Start()
    {
        base.Start();
        Taken = true;
        if (icon != null)
        {
            rune.sprite = icon;
        }
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
            if (player.ExpSystem.PerkBool(perkCost))
            {
                Taken = true;
                player.GainPerk(perk);
                RuneOpacity();
            }
        }
    }
}