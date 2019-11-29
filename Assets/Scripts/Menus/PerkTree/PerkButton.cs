using UnityEngine;

public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    public PerksTypes perk;

    public Sprite icon;

    [SerializeField]
    private int perkCost = 1;

    public override void Start()
    {
        base.Start();
        if (icon != null)
        {
            rune.sprite = icon;
        }
    }

    public override void OnEnable()
    {
        if (player != null)
        {
            taken = player.Perks.HasPerk(perk);
        }
        base.OnEnable();
    }

    public override void Use()
    {
        if (player.Perks.HasPerk(perk) ? player.Perks.NotMaxLevel(perk, perkInfo.MaxLevel) : true)
        {
            if (player.ExpSystem.PerkBool(perkCost))
            {
                taken = true;
                player.Perks.GainPerk(perk);
                RuneOpacity();
            }
        }
    }
}