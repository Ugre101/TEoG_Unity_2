using UnityEngine;

/// <summary> Template for items, replace all intances of template with prefered name.
/// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
[CreateAssetMenu(fileName = "Stick", menuName = "Item/Weapon/Wooden stick")]
public class WoodenStick : Weapon
{
    public WoodenStick() : base(ItemIds.WoodenStick, "Wooden stick")
    {
        desc = "Stick for items, desc itself is where you say what the item does. This item happens to do nothing.";
        StatMods.Add(AssingStatmod.Create(2f, TypeName, ModTypes.Flat, StatTypes.Str));
        Slots.Add(EquipSlot.LeftHand);
        Slots.Add(EquipSlot.RightHand);
    }

    // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}