using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "WeaponTemplate", menuName = "Item/Weapon/WeaponTemplate")]
    public class WeaponTemplate : Weapon
    {
        public WeaponTemplate() : base(ItemIds.WoodenStick, "WeaponTemplate")
        {
            desc = "template for items, desc itself is where you say what the item does. This item happens to do nothing.";
            AssingStatmod assingMod1 = new AssingStatmod(new StatMod(2, typeof(WeaponTemplate).Name, ModTypes.Flat), StatTypes.Charm);
            StatMods.Add(assingMod1);
            Slots.Add(EquipSlot.LeftHand);
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}