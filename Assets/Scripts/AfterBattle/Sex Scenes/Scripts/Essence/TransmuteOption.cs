public class TransmuteOption : SexOptions
{
    public override bool HaveOption(BasicChar player) => EssenceExtension.CanTransmuteEssence(player);

    public override string ToggleOption() => $"Transmute essence: { EssenceExtension.ToggleTransmuteOption}";
}