using System.Collections.Generic;

namespace EssenceMenuStuff
{
    public class AddDick : AddOrgan
    {
        protected override Essence Ess => player.Essence.Masc;

        protected override float Cost => Dicks.Cost();

        private List<Dick> Dicks => player.SexualOrgans.Dicks;

        protected override void AddFunc()
        {
            if (CanAfford)
            {
                Ess.Lose(Cost);
                Dicks.AddDick();
                DisplayCost();
                ShowIfCanAfford();
            }
        }

        protected override void DisplayCost() => btnText.text = $"Add dick: {Cost}";
    }
}