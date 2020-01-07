using UnityEngine;

[CreateAssetMenu(fileName = "Perk info", menuName = "Perks/Perk info")]
public class PerkInfo : ScriptableObject
{
    [SerializeField]
    [TextArea]
    private string perkInfo = "";

    public string Info => perkInfo;

    [SerializeField]
    [TextArea]
    private string perkEffects = "";

    public string Effects => perkEffects;

    [SerializeField]
    private int maxLevel = 1;

    public int MaxLevel => maxLevel;
}

public static class PerkEffects
{
    public static class Gluttony
    {
        public static float ExtraFatBurn(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;

        public static float ExtraRecovery(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.2f * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;

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
        public static StatMod CharmMod => new StatMod(5f, StatTypes.Charm, typeof(Seductress).Name, ModTypes.Flat);
    }
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/