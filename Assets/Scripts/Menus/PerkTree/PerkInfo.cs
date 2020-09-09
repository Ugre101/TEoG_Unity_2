using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : BaseInfo
{
    [SerializeField] private PerksTypes perk = PerksTypes.Delicate;
    public PerksTypes Perk => perk;
    [SerializeField] private bool needCharStat = false;
    private bool NeedCharStat => needCharStat;
    [SerializeField] private List<NeededCharStat> neededCharStats = new List<NeededCharStat>();
    private IEnumerable<NeededCharStat> NeededCharStats => neededCharStats;
    [SerializeField] private bool needOtherPerks = false;
    private bool NeedOtherPerks => needOtherPerks;
    [SerializeField] private List<NeededPerk> neededPerks = new List<NeededPerk>();
    private IEnumerable<NeededPerk> NeededPerks => neededPerks;

    [FormerlySerializedAs("isExcluvsive")] [SerializeField]
    private bool isExclusive = false;

    private bool IsExclusive => isExclusive;
    [SerializeField] private List<PerksTypes> exclusiveWith = new List<PerksTypes>();
    private IEnumerable<PerksTypes> ExclusiveWith => exclusiveWith;
    public override string Effects => base.Effects + GetExclusiveInfo();

    private string GetExclusiveInfo()
    {
        if (!IsExclusive) return string.Empty;
        StringBuilder sb = new StringBuilder("\n");
        foreach (PerksTypes perksTypes in ExclusiveWith)
        {
            sb.Append($"* Exclusive with {perksTypes}\n");
        }

        return sb.ToString();
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

        if (!NeedCharStat) return !IsExclusive || ExclusiveWith.All(perks => !basicChar.Perks.HasPerk(perks));

        if (NeededCharStats.Any(charStat => basicChar.Stats.GetStat(charStat.Stat).BaseValue < charStat.Amount)
        ) return false;

        return !IsExclusive || ExclusiveWith.All(perks => !basicChar.Perks.HasPerk(perks));
    }

    public string MissingReqs(BasicChar basicChar)
    {
        StringBuilder sb = new StringBuilder();
        if (NeedOtherPerks)
        {
            List<Perk> myPerks = basicChar.Perks.List;
            foreach (NeededPerk perks in NeededPerks.Where(perks => !myPerks.Exists(p => p.Type == perks.Perk))
                .Select(perks => perks))
            {
                sb.Append(perks.Amount == 1 ? $"Need: {perks.Perk}\n" : $"Need: {perks.Amount} {perks.Perk}\n");
            }

            foreach (NeededPerk perks in NeededPerks.Where(perks => myPerks.Exists(p => p.Type == perks.Perk))
                .Select(perks => perks).Where(perks => myPerks.Find(p => p.Type == perks.Perk).Level < perks.Amount)
                .Select(perks => perks))
            {
                sb.Append($"Need: {perks.Amount} {perks.Perk}\n");
            }
        }

        if (NeedCharStat)
        {
            foreach (NeededCharStat charStat in NeededCharStats
                .Where(charStat => basicChar.Stats.GetStat(charStat.Stat).BaseValue < charStat.Amount)
                .Select(charStat => charStat))
            {
                sb.Append("Need: " + charStat.Stat.ToString() + charStat.Amount + "\n");
            }
        }

        if (!IsExclusive) return sb.ToString();
        foreach (PerksTypes perks in ExclusiveWith)
        {
            if (basicChar.Perks.HasPerk(perks))
            {
                sb.Append($"Is exclusive with {perks}\n");
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