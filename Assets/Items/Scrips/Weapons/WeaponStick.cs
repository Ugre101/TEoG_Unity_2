using UnityEngine;

/// <summary>
/// Template for items, replace all intances of template with prefered name.
/// Also remember to add the item's id to Items.cs in "public enum ItemId".
/// </summary>
[CreateAssetMenu(fileName = "Stick", menuName = "Item/Weapon/Stick")]
public class WeaponStick : Weapon
{
    public WeaponStick()
    {
        itemId = ItemId.Stick;
        title = "Stick";
        desc = "Stick for items, desc itself is where you say what the item does. This item happens to do nothing.";
        StatMods mod1 = new StatMods(2f, StatTypes.Str, StatsModType.Flat);
        mods.Add(mod1);
    }

    // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}