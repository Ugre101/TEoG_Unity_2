using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddBoobs : AddOrgan
    {
        private Essence Femi => player.Essence.Femi;
        private List<Boobs> Boobs => player.SexualOrgans.Boobs;

        protected override void AddFunc()
        {
            if (Femi.Amount > Boobs.Cost())
            {
                Femi.Lose(Boobs.Cost());
                Boobs.AddBoobs();
                DisplayCost();
            }
        }

        protected override void DisplayCost() => btnText.text = $"Add boobs: {Boobs.Cost()}Femi";
    }
}