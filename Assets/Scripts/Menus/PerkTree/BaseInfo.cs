﻿using UnityEngine;

public abstract class BaseInfo : ScriptableObject
{
    [SerializeField] private Sprite icon = null;
    public Sprite Icon => icon;

    [SerializeField] protected string perkTitle = "";
    public string Title => perkTitle;

    [TextArea]
    [SerializeField] protected string perkInfo = "";

    public string Info => perkInfo;

    [TextArea]
    [SerializeField] protected string perkEffects = "";

    public virtual string Effects => "Costs: " + perkCost.ToString() + " points" + "\tMax level: " + MaxLevel + "\n" + perkEffects;

    [SerializeField] protected int maxLevel = 1, perkCost = 1;

    public int MaxLevel => maxLevel;
    public int PerkCost => perkCost;
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/
