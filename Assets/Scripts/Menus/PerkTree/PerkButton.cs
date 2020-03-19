using UnityEngine;

public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private PerkInfo perkInfo = null;

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
            Taken = player.Perks.HasPerk(perkInfo.Perk);
            base.OnEnable();
        }
    }

    protected override void Use()
    {
        if (perkInfo.Unlocked(player))
        {
            if (player.Perks.HasPerk(perkInfo.Perk) ? player.Perks.NotMaxLevel(perkInfo.Perk, perkInfo.MaxLevel) : true)
            {
                if (player.ExpSystem.PerkBool(perkInfo.PerkCost))
                {
                    Taken = true;
                    player.GainPerk(perkInfo.Perk);
                }
            }
        }
    }

    protected override void Hovering()
    {
        if (perkInfo.Unlocked(player))
        {
            PerkTreeHoverText.Hovering(perkInfo.Info, perkInfo.Effects);
        }
        else
        {
            PerkTreeHoverText.Hovering(perkInfo.Info, perkInfo.Effects, perkInfo.MissingReqs(player));
        }
    }
}