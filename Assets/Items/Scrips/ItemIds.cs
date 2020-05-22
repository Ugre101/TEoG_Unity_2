/*
 * Using ids instead of having the item itself being saved in inventory, this is to slim save data and
 * to make it so I can buff and nerf items without braking saves.
 */

/// <summary>
/// Id of item
/// </summary>
public enum ItemIds // Not sure about enum long term seems hard to struture, but I don't know what else to use
{
    // Remember to always add new ones last so old enums doesn't brake in saves, they are saved as int's; pouch = 0 etc...

    #region Don't touch unless necessary

    Pouch, // Done

    Potion,
    WoodenStick, // Done
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
    MaleContraceptive,
    SpikedClub,

    #endregion Don't touch unless necessary

    WoodenWarHammer,
    Hood,
    Wood,
}