using System.Collections.Generic;
using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "Hood", menuName = "Item/Armour/Head/Hood")]
    public class Hood : Armour, IHaveStatMods, IHaveHealthMods
    {
        public Hood() : base(ItemIds.Hood, "Hood")
        {
            desc = "template for items, desc itself is where you say what the item does. This item happens to do nothing.";
            StatMods.Add(AssingStatmod.Create(-1, TypeName, ModTypes.Flat, StatTypes.Charm));
            StatMods.Add(AssingStatmod.Create(1, TypeName, ModTypes.Flat, StatTypes.End));
            HealthMods.Add(new HealthMod(5, ModTypes.Flat, TypeName, HealthTypes.Health));
            HealthMods.Add(new HealthMod(5, ModTypes.Flat, TypeName, HealthTypes.WillPower));
            Slots.Add(EquipSlot.Head);
        }

        public List<AssingStatmod> StatMods { get; } = new List<AssingStatmod>();

        public List<HealthMod> HealthMods { get; } = new List<HealthMod>();

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}