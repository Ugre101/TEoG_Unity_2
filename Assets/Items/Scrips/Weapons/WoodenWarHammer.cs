using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "WoodenWarHammer", menuName = "Item/Weapon/WoodenWarHammer")]
    public class WoodenWarHammer : Weapon
    {
        public WoodenWarHammer() : base(ItemIds.WoodenWarHammer, "Wooden war hammer")
        {
            desc = "template for items, desc itself is where you say what the item does. This item happens to do nothing.";
            StatMods.Add(AssingStatmod.Create(4f, TypeName, ModTypes.Flat, StatTypes.Str));
            StatMods.Add(AssingStatmod.Create(-2f, TypeName, ModTypes.Flat, StatTypes.Dex));
            Slots.Add(EquipSlot.LeftHand);
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}