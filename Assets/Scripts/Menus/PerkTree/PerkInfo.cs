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
    [SerializeField] private List<PerksTypes> neededPerks = new List<PerksTypes>();
    private List<PerksTypes> NeededPerks => neededPerks;

    public bool Unlocked(BasicChar basicChar)
    {
        if (NeedOtherPerks)
        {
            foreach (PerksTypes perks in NeededPerks)
            {
                if (!basicChar.Perks.List.Exists(p => p.Type == perks))
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
            foreach (PerksTypes perks in NeededPerks.Where(perks => !basicChar.Perks.List.Exists(p => p.Type == perks)).Select(perks => perks))
            {
                sb.Append("Need: " + perks.ToString());
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
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/