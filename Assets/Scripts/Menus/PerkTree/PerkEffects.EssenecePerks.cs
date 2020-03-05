public static partial class PerkEffects
{
    public static class EssenecePerks
    {
        public static class EssFlow
        {
            public static float ExtraDrain(Perks perks) => perks.HasPerk(PerksTypes.EssenceFlow)
                ? 10f * perks.GetPerkLevel(PerksTypes.EssenceFlow) : 0;

            public static float GetExtraDrained(Perks perks) => perks.HasPerk(PerksTypes.EssenceFlow)
                ? 10f * perks.GetPerkLevel(PerksTypes.EssenceFlow) : 0;
        }

        public static class EssThief
        {
            public static float ExtraDrain(Perks perks) => perks.HasPerk(PerksTypes.EssenceThief)
                ? 5f * perks.GetPerkLevel(PerksTypes.EssenceThief) : 0;

            public static StatMod ImproveCapacity => StatMod.FlatMod(50, typeof(EssThief).Name);
        }

        public static class EssHoarder
        {
            public static StatMod ImproveCapacity => StatMod.FlatMod(300, typeof(EssHoarder).Name);
        }

        public static class EssShaper
        {
            public static float ExtraGive(Perks perks) => perks.HasPerk(PerksTypes.EssenceShaper)
                 ? 5f * perks.GetPerkLevel(PerksTypes.EssenceShaper) : 0;

            public static StatMod ImproveCapacity => StatMod.FlatMod(100, typeof(EssThief).Name);
        }
    }
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/