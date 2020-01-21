using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddBalls : AddOrgan
    {
        private Essence Masc => player.Essence.Masc;
        private List<Balls> Balls => player.SexualOrgans.Balls;

        protected override void DisplayCost() => btnText.text = $"Add balls: {Balls.Cost()}";

        protected override void AddFunc()
        {
            if (Masc.Amount > Balls.Cost())
            {
                Masc.Lose(Balls.Cost());
                Balls.AddBalls();
                DisplayCost();
            }
        }
    }
}