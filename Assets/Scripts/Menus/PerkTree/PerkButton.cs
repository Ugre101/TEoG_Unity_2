using UnityEngine;
using UnityEngine.EventSystems;

public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    [SerializeField] private PerkInfo perkInfo = null;

    private void SetRuntSprite()
    {
        if (perkInfo != null && perkInfo.Icon != null)
        {
            rune.sprite = perkInfo.Icon;
        }
    }

    protected override void Start()
    {
        base.Start();
        SetRuntSprite();
    }

    protected override void OnEnable()
    {
        if (perkInfo == null)
        {
            gameObject.SetActive(false);
        }
        if (player != null)
        {
            Taken = player.Perks.HasPerk(perkInfo.Perk);
        }
        base.OnEnable();
    }

    protected override void Use()
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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        PerkTreeHoverText.Hovering(perkInfo.Info);
    }
}