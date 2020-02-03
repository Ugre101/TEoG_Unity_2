using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "FertilityBooster", menuName = "Item/Drinkable/FertilityBooster")]
    public class FertilityBooster : Drinkable
    {
        public FertilityBooster() : base(ItemId.FertilityBooster, "Fertility booster")
        {
        }

        public override string Use(BasicChar user)
        {
            user.PregnancySystem.Fertility.BaseValue++;
            return base.Use(user);
        }
    }
}