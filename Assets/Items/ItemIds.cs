/* 
 * Using ids instead of having the item itself being saved in inventory, this is to slim save data and
 * to make it so I can buff and nerf items without braking saves.
 */

/// <summary>
/// Id of item
/// </summary>
public enum ItemId
{
    Pouch,
    Potion,
    Stick
}

