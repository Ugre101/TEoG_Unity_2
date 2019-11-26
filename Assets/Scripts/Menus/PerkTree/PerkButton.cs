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
            taken = player.Perk.HasPerk(perk);
        }
        base.OnEnable();
    }

    public override void Use()
    {
        if (player.Perk.HasPerk(perk) ? player.Perk.NotMaxLevel(perk, perkInfo.MaxLevel) : true)
        {
            if (player.ExpSystem.PerkBool(perkCost))
            {
                taken = true;
                player.Perk.GainPerk(perk);
                RuneOpacity();
            }
        }
    }
}