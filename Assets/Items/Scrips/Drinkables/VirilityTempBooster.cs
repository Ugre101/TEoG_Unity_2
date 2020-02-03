using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "VirilityTempBooster", menuName = "Item/Drinkable/VirilityTempBooster")]
    public class VirilityTempBooster : Drinkable
    {
        public VirilityTempBooster() : base(ItemId.VirilityTempBooster, "Virility week booster")
        {
        }

        public override string Use(BasicChar user)
        {
            TempStatMod tempMod = new TempStatMod(10, ModTypes.Flat, typeof(VirilityTempBooster).Name, 168);
            user.PregnancySystem.Virility.AddTempMod(tempMod);
            return base.Use(user);
        }
    }
}