public static partial class PerkEffects
{
    public static class Bully
    {
        public static float HealthReGainReduction(Perks perks) => PerkFloatEffect(perks, PerksTypes.Bully, 0.1f);
    }

    public static class Gluttony
    {
        public static float ExtraFatBurn(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;

        public static int ExtraRecovery(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 3 * perks.GetPerkLevel(PerksTypes.Gluttony)
            : 0;

        public static float ExtraStarvationPenalty(Perks perks) => perks.HasPerk(PerksTypes.Gluttony)
            ? 0.1f * perks.GetPerkLevel(PerksTypes.Gluttony)
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
        public static StatMod CharmMod => new StatMod(5f, nameof(Seductress), ModTypes.Flat);
    }

    public static class StoneSkin
    {
    }

    public static class Delicate
    {
        public static StatMod CharmModFlat = new StatMod(5, nameof(Delicate), ModTypes.Flat);
        public static StatMod CharmModPre = new StatMod(0.1f, nameof(Delicate), ModTypes.Precent);

        public static float ExtraSensitive(Perks perks) => perks.HasPerk(PerksTypes.Delicate)
            ? 0.2f * perks.GetPerkLevel(PerksTypes.Delicate)
            : 0;
    }

    public static class HealtyBody
    {
        public static HealthMod HealthFlat => new HealthMod(20, ModTypes.Flat, nameof(HealtyBody), HealthTypes.Health);
        public static HealthMod HealthPre => new HealthMod(0.1f, ModTypes.Precent, nameof(HealtyBody), HealthTypes.Health);
        public static HealthMod RecoveryFlat => new HealthMod(3, ModTypes.Flat, nameof(HealtyBody), HealthTypes.Health);
        public static HealthMod RecoveryPre => new HealthMod(0.1f, ModTypes.Precent, nameof(HealtyBody), HealthTypes.Health);
    }

    public static class Thug
    {
        public static StatMod StrFlat => new StatMod(5, nameof(Thug), ModTypes.Flat);

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