/*
 * Using ids instead of having the item itself being saved in inventory, this is to slim save data and
 * to make it so I can buff and nerf items without braking saves.
 */

/// <summary>
/// Id of item
/// </summary>
public enum ItemId
{
    // Remember to always add new ones last so old enums doesn't brake in saves, they are saved as int's; pouch = 0 etc...
    Pouch, // Done

    Potion,
    Stick, // Done
    VirilityBooster, // Done
    VirilityTempBooster, // Done
    FertilityBooster, // Done
    FertilityTempBooster, // Done
    OrcCum, // Done
    SmallStrPotion,
    PotionOfHumanity, // Done
    OrcBrew, // Done
    TrollMilk, // Done
    ElvenHair,
    SmallPouch, // Done
    LargePouch, // Done
    FairyDust, // Done
    BovineMilk,
    MilkJug,
    FertilityIdol,
    phallusRock,
    SuccubusMilk,
    IncubusSemen,
    InfernalMilk,
    Milker500,
    SmallMilkBottle, // Done
    MilkBottle,
    LargeMilkBottle,
    PocketPortal,
    SmallMealRation, // Done
    MealRation,
    LargeMealRation,
    Contraceptive, // Done
    LiquidBarrenness, // Done
    SeedDiluter, // Done
    MaleContraceptive
}