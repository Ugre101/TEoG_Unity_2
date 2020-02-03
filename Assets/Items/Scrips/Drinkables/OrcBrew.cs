using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "OrcBrew", menuName = "Item/Drinkable/OrcBrew")]
    public class OrcBrew : Drinkable
    {
        public OrcBrew() : base(ItemId.OrcBrew, "Orc brew")
        {
        }

        public override string Use(BasicChar user)
        {
            float toHeal = user.HP.Value / 10;
            if (!user.HP.IsMax)
            {
                user.HP.Gain(toHeal);
            }
            return $"Bottoms up!\n You regained {toHeal} health back!";
        }
    }
}