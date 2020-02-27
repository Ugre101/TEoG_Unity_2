using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "PotionOfHumanity", menuName = "Item/Drinkable/PotionOfHumanity")]
    public class PotionOfHumanity : Drinkable
    {
        public PotionOfHumanity() : base(ItemIds.PotionOfHumanity, "Potion of humanity")
        {
        }

        public override string Use(BasicChar user)
        {
            user.RaceSystem.AddRace(Races.Human, 50);
            return base.Use(user);
        }
    }
}