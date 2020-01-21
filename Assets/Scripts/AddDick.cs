using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddDick : AddOrgan
    {
        private Essence Masc => player.Essence.Masc;
        private List<Dick> Dicks => player.SexualOrgans.Dicks;

        protected override void AddFunc()
        {
            if (Masc.Amount > Dicks.Cost())
            {
                Masc.Lose(Dicks.Cost());
                Dicks.AddDick();
                DisplayCost();
            }
        }

        protected override void DisplayCost() => btnText.text = $"Add dick: {Dicks.Cost()}";
    }
}