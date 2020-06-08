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
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceThief, 5f);

            public static StatMod ImproveCapacity => StatMod.FlatMod(50, typeof(EssThief).Name);
        }

        public static class EssBandit
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceBandit, 15f);

            public static StatMod ImproveCapacity => StatMod.FlatMod(200, typeof(EssThief).Name);
        }

        public static class EssHoarder
        {
            public static StatMod ImproveCapacity => StatMod.FlatMod(300, typeof(EssHoarder).Name);
        }

        public static class EssShaper
        {
            public static float ExtraGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceShaper, 5f);

            public static StatMod ImproveCapacity => StatMod.FlatMod(100, typeof(EssThief).Name);
        }

        public static class EssMascVacuum
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.MasculineVacuum, 15f);
        }

        public static class EssFemiVacuum
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.FemenineVacuum, 15f);
        }

        public static class EssMascFlow
        {
            public static float EssGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.MasculineFlow, 15f);

            public static float EssGiveBonus(Perks perks) => PerkFloatEffect(perks, PerksTypes.MasculineFlow, 5f);
        }

        public static class EssFemiFlow
        {
            public static float EssGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.FemenineFlow, 15f);

            public static float EssGiveBonus(Perks perks) => PerkFloatEffect(perks, PerksTypes.FemenineFlow, 5f);
        }
    }

    private static float PerkFloatEffect(Perks perks, PerksTypes type, float multiplier) => perks.HasPerk(type) ? multiplier * perks.GetPerkLevel(type) : 0;
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/