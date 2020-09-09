public static partial class PerkEffects
{
    public static class EssenecePerks
    {
        public static class EssFlow
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceFlow, 10f);

            public static float GetExtraDrained(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceFlow, 10f);
        }

        public static class EssThief
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceThief, 5f);

            public static int ImproveCapacity(Perks perks) => PerkIntEffect(perks, PerksTypes.EssenceThief, 50);
        }

        public static class EssBandit
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceBandit, 15f);

            public static int ImproveCapacity(Perks perks) => PerkIntEffect(perks, PerksTypes.EssenceBandit, 200);
        }

        public static class EssHoarder
        {
            public static int ImproveCapacity(Perks perks) => PerkIntEffect(perks, PerksTypes.EssenceHoarder, 300);
        }

        public static class EssShaper
        {
            public static float ExtraGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceShaper, 5f);

            public static int ImproveCapacity(Perks perks) => PerkIntEffect(perks, PerksTypes.EssenceShaper, 100);
            public static float TransmuteAmount(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceTransformer, 30f);

        }

        public static class EssTransformer
        {
            public static float ExtraGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceTransformer, 15f);
            public static float TransmuteAmount(Perks perks) => PerkFloatEffect(perks, PerksTypes.EssenceTransformer, 100f);
        }

        public static class EssMascVacuum
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.MasculineVacuum, 15f);
        }

        public static class EssFemiVacuum
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.FemenineVacuum, 15f);
        }

        public static class EssHemiVacuum
        {
            public static float ExtraDrain(Perks perks) => PerkFloatEffect(perks, PerksTypes.HermaphroditeVacuum, 30f);
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

        public static class EssHemiFlow
        {
            public static float EssGive(Perks perks) => PerkFloatEffect(perks, PerksTypes.HermaphroditeFlow, 30f);

            public static float EssGiveBonus(Perks perks) => PerkFloatEffect(perks, PerksTypes.HermaphroditeFlow, 10f);

        }
    }

    private static float PerkFloatEffect(Perks perks, PerksTypes type, float multiplier) => perks.HasPerk(type) ? multiplier * perks.GetPerkLevel(type) : 0;

    private static int PerkIntEffect(Perks perks, PerksTypes type, int multiplier) => perks.HasPerk(type) ? multiplier * perks.GetPerkLevel(type) : 0;
}

/*
 *store perk info in a ScriptableObject so that it's consistent
 *also store perk effect values so that if perks need nerfs/buffs in future I should only need to change them here instead
 *of having to find all refernces.
*/