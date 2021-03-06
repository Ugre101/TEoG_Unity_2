﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/

[CreateAssetMenu(fileName = "Vore perk info", menuName = "Perks/Vore perk info")]
public class VorePerkInfo : BaseInfo
{
    [SerializeField] private VorePerks perk = VorePerks.Compression;
    public VorePerks Perk => perk;
    [SerializeField] private bool needCharStat = false;
    public bool NeedCharStat => needCharStat;
    [SerializeField] private List<NeededCharStat> neededCharStats = new List<NeededCharStat>();
    public List<NeededCharStat> NeededCharStats => neededCharStats;
    [SerializeField] private bool needOtherPerks = false;
    public bool NeedOtherPerks => needOtherPerks;
    [SerializeField] private List<NeededPerk> neededPerks = new List<NeededPerk>();
    private List<NeededPerk> NeededPerks => neededPerks;
    [SerializeField] private bool isExcluvsive = false;
    public bool IsExcluvsive => isExcluvsive;
    [SerializeField] private List<VorePerks> exclusiveWith = new List<VorePerks>();
    public List<VorePerks> ExclusiveWith => exclusiveWith;
    public override string Effects => base.Effects + GetExclusiveInfo();

    private string GetExclusiveInfo()
    {
        if (IsExcluvsive)
        {
            StringBuilder sb = new StringBuilder("\n");
            foreach (VorePerks perksTypes in ExclusiveWith)
            {
                sb.Append($"* Exclusive with {perksTypes}\n");
            }
            return sb.ToString();
        }
        return string.Empty;
    }

    public bool Unlocked(BasicChar basicChar)
    {
        if (NeedOtherPerks)
        {
            foreach (NeededPerk perks in NeededPerks)
            {
                List<Perk> list = basicChar.Perks.List;
                if (!list.Exists(p => p.Type == perks.Perk))
                {
                    return false;
                }
                else if (list.Find(p => p.Type == perks.Perk).Level < perks.Amount)
                {
                    return false;
                }
            }
        }
        if (NeedCharStat)
        {
            foreach (NeededCharStat charStat in NeededCharStats)
            {
                if (basicChar.Stats.GetStat(charStat.Stat).BaseValue < charStat.Amount)
                {
                    return false;
                }
            }
        }
        if (IsExcluvsive)
        {
            foreach (PerksTypes perks in ExclusiveWith)
            {
                if (basicChar.Perks.HasPerk(perks))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public string MissingReqs(BasicChar basicChar)
    {
        StringBuilder sb = new StringBuilder();
        if (NeedOtherPerks)
        {
            List<Perk> myPerks = basicChar.Perks.List;
            foreach (NeededPerk perks in NeededPerks.Where(perks => !myPerks.Exists(p => p.Type == perks.Perk)).Select(perks => perks))
            {
                if (perks.Amount == 1)
                {
                    sb.Append($"Need: {perks.Perk}\n");
                }
                else
                {
                    sb.Append($"Need: {perks.Amount} {perks.Perk}\n");
                }
            }
            foreach (NeededPerk perks in NeededPerks.Where(perks => myPerks.Exists(p => p.Type == perks.Perk)).Select(perks => perks).Where(perks => myPerks.Find(p => p.Type == perks.Perk).Level < perks.Amount).Select(perks => perks))
            {
                sb.Append($"Need: {perks.Amount} {perks.Perk}\n");
            }
        }
        if (NeedCharStat)
        {
            foreach (NeededCharStat charStat in NeededCharStats.Where(charStat => basicChar.Stats.GetStat(charStat.Stat).BaseValue < charStat.Amount).Select(charStat => charStat))
            {
                sb.Append("Need: " + charStat.Stat.ToString() + charStat.Amount + "\n");
            }
        }
        if (IsExcluvsive)
        {
            foreach (PerksTypes perks in ExclusiveWith)
            {
                if (basicChar.Perks.HasPerk(perks))
                {
                    sb.Append($"Is excluvsive with {perks}\n");
                }
            }
        }
        return sb.ToString();
    }

    [System.Serializable]
    public class NeededCharStat
    {
        [SerializeField] private int amount = 0;
        [SerializeField] private StatTypes stat = StatTypes.Charm;
        public int Amount => amount;
        public StatTypes Stat => stat;
    }

    [System.Serializable]
    public class NeededPerk
    {
        [SerializeField] private int amount = 1;
        [SerializeField] private PerksTypes perksTypes = PerksTypes.Delicate;
        public int Amount => amount;
        public PerksTypes Perk => perksTypes;
    }
}