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