using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "MilkJug", menuName = "Item/Drinkable/MilkJug")]
    public class MilkJug : Misc
    {
        public MilkJug() : base(ItemIds.SmallMilkBottle, "Small milk bottle")
        {
            desc = "A jug of unpasteurized milk.";
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            // TODO milk extra effect/Tfs
            user.HP.Gain(70);
            user.WP.Gain(70);
            return base.Use(user);
        }
    }
}