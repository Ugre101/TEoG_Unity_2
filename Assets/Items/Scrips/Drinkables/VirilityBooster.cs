using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "VirilityBooster", menuName = "Item/Drinkable/VirilityBooster")]
    public class VirilityBooster : Drinkable
    {
        public VirilityBooster() : base(ItemIds.VirilityBooster, "Virility booster")
        {
        }

        public override string Use(BasicChar user)
        {
            user.PregnancySystem.Virility.BaseValue++;
            return base.Use(user);
        }
    }
}