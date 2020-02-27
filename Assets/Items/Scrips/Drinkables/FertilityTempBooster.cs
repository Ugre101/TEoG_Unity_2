using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "FertilityTempBooster", menuName = "Item/Drinkable/FertilityTempBooster")]
    public class FertilityTempBooster : Drinkable
    {
        public FertilityTempBooster() : base(ItemIds.FertilityTempBooster, "Fertility week booster")
        {
        }

        public override string Use(BasicChar user)
        {
            TempStatMod tempMod = new TempStatMod(10, ModTypes.Flat, TypeName, 168);
            user.PregnancySystem.Fertility.AddTempMod(tempMod);
            return base.Use(user);
        }
    }
}