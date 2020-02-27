using System.Collections.Generic;
using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "ArmourTemplate", menuName = "Item/Armour/ArmourTemplate")]
    public class ArmourTemplate : Armour, IHaveStatMods, IHaveHealthMods
    {
        public ArmourTemplate() : base(ItemIds.WoodenStick, "ArmourTemplate")
        {
            desc = "template for items, desc itself is where you say what the item does. This item happens to do nothing.";
            StatMods.Add(AssingStatmod.Create(2, TypeName, ModTypes.Flat, StatTypes.Charm));
            HealthMods.Add(new HealthMod(2, ModTypes.Flat, TypeName, HealthTypes.Health));
            Slots.Add(EquipSlot.LeftHand);
        }

        // Delete and remove interface if item doesn't have relevant mod
        public List<AssingStatmod> StatMods { get; } = new List<AssingStatmod>();

        public List<HealthMod> HealthMods { get; } = new List<HealthMod>();

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}