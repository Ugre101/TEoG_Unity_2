using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "SmallMilkBottle", menuName = "Item/Drinkable/SmallMilkBottle")]
    public class SmallMilkBottle : Misc
    {
        public SmallMilkBottle() : base(ItemIds.SmallMilkBottle, "Small milk bottle")
        {
            desc = "A small bottle of unpasteurized milk.";
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            // TODO milk extra effect/Tfs
            user.Hp.Gain(30);
            user.Wp.Gain(30);
            return base.Use(user);
        }
    }
}