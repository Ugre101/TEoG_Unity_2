using UnityEngine;

public class VorePerkButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private VorePerkInfo perkInfo = null;

    private int PerkLevel => player.Vore.Perks.GetPerkLevel(perkInfo.Perk);

    private void SetRuntSprite()
    {
        if (perkInfo.Icon != null)
        {
            rune.sprite = perkInfo.Icon;
        }
    }

    private bool started = false;

    protected override void Start()
    {
        if (perkInfo != null)
        {
            base.Start();
            SetRuntSprite();
            started = true;
            OnEnable();
        }
        else
        {
            gameObject.SetActive(false); // if null set inactive
        }
    }

    protected override void OnEnable()
    {
        if (started)
        {
            Taken = player.Vore.Perks.HasPerk(perkInfo.Perk);
            amount.text = PerkLevel.ToString();
            base.OnEnable();
        }
    }

    protected override void Use()
    {
        if (perkInfo.Unlocked(player))
        {
            if (player.Vore.Perks.HasPerk(perkInfo.Perk) ? player.Vore.Perks.NotMaxLevel(perkInfo.Perk, perkInfo.MaxLevel) : true)
            {
                if (player.Vore.VoreExp.PerkBool(perkInfo.PerkCost))
                {
                    Taken = true;
                    player.GainPerk(perkInfo.Perk);
                    amount.text = PerkLevel.ToString();
                }
            }
        }
    }

    protected override void Hovering()
    {
        if (perkInfo.Unlocked(player))
        {
            PerkTreeHoverText.Hovering(perkInfo.Title, perkInfo.Info, perkInfo.Effects);
        }
        else
        {
            PerkTreeHoverText.Hovering(perkInfo.Title, perkInfo.Info, perkInfo.Effects, perkInfo.MissingReqs(player));
        }
    }
}