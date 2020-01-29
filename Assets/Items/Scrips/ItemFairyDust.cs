using UnityEngine;

namespace ItemScripts
{
    public class ItemFairyDust : Edibles
    {
        public ItemFairyDust() : base(ItemId.Potion, "Fairy dust")
        {
            useName = "Lick";
        }

        public override string Use(BasicChar user)
        {
            user.RaceSystem.AddRace(Races.Fairy, 5);
            BodyStat height = user.Body.Height;
            if (height.Value > 100)
            {
                height.LosePrecent(0.05f);
            }
            else
            {
                float toLose = Mathf.Clamp(5, 0, height.Value - 20);
                height.LoseFlat(toLose);
            }
            return "Inhaling the fairy dust you see the world grow before you, or maybe it's you who became shorter?";
        }
    }
}