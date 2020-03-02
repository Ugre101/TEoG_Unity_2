using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [TextArea]
    [SerializeField] private string perkInfo = "";

    public string Info => perkInfo;

    [TextArea]
    [SerializeField] private string perkEffects = "";

    public string Effects => perkEffects;

    [SerializeField] private int maxLevel = 1, perkCost = 1;

    public int MaxLevel => maxLevel;
    public int PerkCost => perkCost;
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
        return true;
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



public static class PerkEffects
{
    public static class Gluttony
    {
        public static float ExtraFatBurn(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;

        public static HealthMod ExtraRecovery(HealthTypes type) => new HealthMod(3, ModTypes.Flat, typeof(Gluttony).Name, type);

        public static float ExtraStarvationPenalty(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;
    }

    public static class EssenceFlow
    {
        public static float ExtraDrain(Perks perks) => perks.HasPerk(PerksTypes.EssenceFlow)
            ? 5f * perks.GetPerkLevel(PerksTypes.EssenceFlow)
            : 0;

        public static float GetExtraDrained(Perks perks) => perks.HasPerk(PerksTypes.EssenceFlow)
            ? 3f * perks.GetPerkLevel(PerksTypes.EssenceFlow)
            : 0;
    }

    public static class LowMetabolism
    {
        public static float LowerBurn(Perks perks)
        {
            return perks.HasPerk(PerksTypes.LowMetabolism)
                ? 0.5f * perks.GetPerkLevel(PerksTypes.LowMetabolism)
                : 0;
        }
    }

    public static class Seductress
    {
        public static StatMod CharmMod => new StatMod(5f, typeof(Seductress).Name, ModTypes.Flat);
    }

    public static class StoneSkin
    {
    }

    public static class Delicate
    {
        public static StatMod CharmModFlat = new StatMod(5, typeof(Delicate).Name, ModTypes.Flat);
        public static StatMod CharmModPre = new StatMod(0.1f, typeof(Delicate).Name, ModTypes.Precent);

        public static float ExtraSensitive(Perks perks) => perks.HasPerk(PerksTypes.Delicate)
            ? 0.2f * perks.GetPerkLevel(PerksTypes.Delicate)
            : 0;
    }

    public static class HealtyBody
    {
        public static HealthMod HealthFlat => new HealthMod(20, ModTypes.Flat, typeof(HealtyBody).Name, HealthTypes.Health);
        public static HealthMod HealthPre => new HealthMod(0.1f, ModTypes.Precent, typeof(HealtyBody).Name, HealthTypes.Health);
        public static HealthMod RecoveryFlat => new HealthMod(3, ModTypes.Flat, typeof(HealtyBody).Name, HealthTypes.Health);
        public static HealthMod RecoveryPre => new HealthMod(0.1f, ModTypes.Precent, typeof(HealtyBody).Name, HealthTypes.Health);
    }

    public static class Thug
    {
        public static StatMod StrFlat => new StatMod(5, typeof(Thug).Name, ModTypes.Flat);

        public static float AfterbattleHealPenalty(Perks perks) => perks.HasPerk(PerksTypes.Thug)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Thug)
            : 0;
    }

    public static class Greedy
    {
        public static float ExtraGold(Perks perks) => perks.HasPerk(PerksTypes.Greedy)
            ? 0.05f * perks.GetPerkLevel(PerksTypes.Greedy)
            : 0f;

        public static float Discount(Perks perks) => perks.HasPerk(PerksTypes.Greedy)
            ? 0.02f * perks.GetPerkLevel(PerksTypes.Greedy)
            : 0f;
    }
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/