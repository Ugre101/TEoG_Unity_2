using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "OrcCum", menuName = "Item/Drinkable/OrcCum")]
    public class OrcCum : Drinkable
    {
        public OrcCum() : base(ItemIds.OrcCum, "Orc cum")
        {
        }

        public override string Use(BasicChar user)
        {
            user.RaceSystem.AddRace(Races.Orc, 10);
            user.Essence.Masc.Gain(50);
            return $"After drinking the orc cum, {user.Identity.FirstName} absorbs the manly essence of it.";
        }
    }
}