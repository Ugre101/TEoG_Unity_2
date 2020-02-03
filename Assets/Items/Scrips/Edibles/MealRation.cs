using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "MealRation", menuName = "Item/Edibles/MealRation")]
    public class MealRation : Edibles
    {
        public MealRation() : base(ItemId.MealRation, "Meal ration")
        {
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}