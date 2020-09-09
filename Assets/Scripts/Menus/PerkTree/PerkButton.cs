﻿using UnityEngine;

public class PerkButton : PerkTreeBasicBtn
{
    [Space] [SerializeField] private PerkInfo perkInfo = null;
    [SerializeField] private DrawLineBetweenObejcts drawLine = null;
    private int PerkLevel => Player.Perks.GetPerkLevel(perkInfo.Perk);

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
        if (!started) return;
        Taken = Player.Perks.HasPerk(perkInfo.Perk);
        amount.text = PerkLevel.ToString();
        base.OnEnable();
    }

    protected override void Use()
    {
        if (!perkInfo.Unlocked(Player) ||
            (Player.Perks.HasPerk(perkInfo.Perk) && !Player.Perks.NotMaxLevel(perkInfo.Perk, perkInfo.MaxLevel)) ||
            !Player.ExpSystem.PerkBool(perkInfo.PerkCost)) return;
        Taken = true;
        Player.GainPerk(perkInfo.Perk);
        amount.text = PerkLevel.ToString();
    }

    protected override void Hovering()
    {
        if (perkInfo.Unlocked(Player))
        {
            PerkTreeHoverText.Hovering(perkInfo.Title, perkInfo.Info, perkInfo.Effects);
        }
        else
        {
            PerkTreeHoverText.Hovering(perkInfo.Title, perkInfo.Info, perkInfo.Effects, perkInfo.MissingReqs(Player));
        }
    }
}