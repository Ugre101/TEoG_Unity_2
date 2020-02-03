using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "ElvenHair", menuName = "Item/Drinkable/ElvenHair")]
    public class ElvenHair : Misc
    {
        public ElvenHair() : base(ItemId.ElvenHair, "Elven hair")
        {
            useName = "Sniff";
        }

        public override string Use(BasicChar user)
        {
            user.RaceSystem.AddRace(Races.Elf, 20);
            return $"Inhaling";
        }
    }
}