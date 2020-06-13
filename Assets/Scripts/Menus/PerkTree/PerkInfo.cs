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
    [SerializeField] private bool isExcluvsive = false;
    public bool IsExcluvsive => isExcluvsive;
    [SerializeField] private List<PerksTypes> exclusiveWith = new List<PerksTypes>();
    public List<PerksTypes> ExclusiveWith => exclusiveWith;
    public override string Effects => base.Effects + GetExclusiveInfo();

    private string GetExclusiveInfo()
    {
        if (IsExcluvsive)
        {
            StringBuilder sb = new StringBuilder("\n");
            foreach (PerksTypes perksTypes in ExclusiveWith)
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