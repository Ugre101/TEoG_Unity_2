using System.Collections.Generic;

namespace EssenceMenuStuff
{
    public class AddBalls : AddOrgan
    {
        private List<Balls> Balls => Player.SexualOrgans.Balls;

        protected override Essence Ess => Player.Essence.Masc;

        protected override float Cost => Balls.Cost();

        protected override void DisplayCost() => btnText.text = $"Add balls: {Cost}";

        protected override void AddFunc()
        {
            if (CanAfford)
            {
                Ess.Lose(Cost);
                Balls.AddBalls();
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}