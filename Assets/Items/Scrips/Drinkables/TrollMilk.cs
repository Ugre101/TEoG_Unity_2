using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "TrollMilk", menuName = "Item/Drinkable/TrollMilk")]
    public class TrollMilk : Drinkable
    {
        public TrollMilk() : base(ItemIds.TrollMilk, "Troll milk")
        {
        }

        public override string Use(BasicChar user)
        {
            float toHeal = user.Wp.Value / 10;
            if (!user.Wp.IsMax)
            {
                user.Wp.Gain(toHeal);
            }
            return $"Bottoms up!\nYou gained {toHeal} willpower back!";
        }
    }
}