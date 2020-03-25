using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : BaseInfo
{
    [SerializeField] private PerksTypes perk = PerksTypes.Delicate;
    public PerksTypes Perk => perk;
    [SerializeField] private bool needCharStat = false;
    public bool NeedCharStat => needCharStat;
    [SerializeField] private List<NeededCharStat> neededCharStats = new List<NeededCharStat>();
    public List<NeededCharStat> NeededCharStats => neededCharStats;
    [SerializeField] private bool needOtherPerks = false;
    public bool NeedOtherPerks => needOtherPerks;
    [SerializeField] private List<NeededPerk> neededPerks = new List<NeededPerk>();
    private List<NeededPerk> NeededPerks => neededPerks;

    public bool Unlocked(BasicChar basicChar)
    {
        if (NeedOtherPerks)
        {
            foreach (NeededPerk perks in NeededPerks)
            {
                if (!basicChar.Perks.List.Exists(p => p.Type == perks.Perk))
                {
                    return false;
                }
                else if (basicChar.Perks.List.Find(p => p.Type == perks.Perk).Level < perks.Amount)
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
        return true;
    }

    public string MissingReqs(BasicChar basicChar)
    {
        StringBuilder sb = new StringBuilder();
        if (NeedOtherPerks)
        {
            foreach (NeededPerk perks in NeededPerks.Where(perks => !basicChar.Perks.List.Exists(p => p.Type == perks.Perk)).Select(perks => perks))
            {
                if (perks.Amount == 1)
                {
                    sb.Append($"Need: {perks.Perk}");
                }
                else
                {
                    sb.Append($"Need: {perks.Amount} {perks.Perk}");
                }
            }
            foreach (NeededPerk perks in NeededPerks.Where(perks => basicChar.Perks.List.Exists(p => p.Type == perks.Perk)).Select(perks => perks).Where(perks
                  => basicChar.Perks.List.Find(p => p.Type == perks.Perk).Level < perks.Amount).Select(perks => perks))
            {
                sb.Append($"Need: {perks.Amount} {perks.Perk}");
            }
        }
        if (NeedCharStat)
        {
            foreach (NeededCharStat charStat in NeededCharStats.Where(charStat => basicChar.Stats.GetStat(charStat.Stat).BaseValue < charStat.Amount).Select(charStat => charStat))
            {
                sb.Append("Need: " + charStat.Stat.ToString() + charStat.Amount);
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

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/