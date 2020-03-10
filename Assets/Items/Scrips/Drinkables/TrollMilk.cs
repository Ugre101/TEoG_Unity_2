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
            float toHeal = user.WP.MaxValue / 10;
            if (!user.WP.IsMax)
            {
                user.WP.Gain(toHeal);
            }
            return $"Bottoms up!\nYou gained {toHeal} willpower back!";
        }
    }
}