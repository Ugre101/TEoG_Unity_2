using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddBoobs : AddOrgan
    {
        protected override Essence Ess => player.Essence.Femi;

        protected override float Cost => Boobs.Cost();

        private List<Boobs> Boobs => player.SexualOrgans.Boobs;

        protected override void AddFunc()
        {
            if (CanAfford)
            {
                Ess.Lose(Cost);
                Boobs.AddBoobs();
                DisplayCost();
                ShowIfCanAfford();
            }
        }

        protected override void DisplayCost() => btnText.text = $"Add boobs: {Cost}Femi";
    }
}