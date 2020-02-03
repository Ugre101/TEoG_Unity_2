using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "LargeMealRation", menuName = "Item/Edibles/LargeMealRation")]
    public class LargeMealRation : Edibles
    {
        public LargeMealRation() : base(ItemId.LargeMealRation, "Large meal ration")
        {
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}