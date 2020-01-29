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
    Pouch,

    Potion,
    Stick,
    VirilityBooster,
    VirilityTempBooster,
    FertilityBooster,
    FertilityTempBooster,
    OrcCum,
    SmallStrPotion,
    PotionOfHumanity,
    OrcBrew,
    TrollMilk,
    ElvenHair
}