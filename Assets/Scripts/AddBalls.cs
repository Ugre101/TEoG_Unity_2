using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddBalls : AddOrgan
    {
        private List<Balls> Balls => player.SexualOrgans.Balls;

        protected override Essence Ess => player.Essence.Masc;

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