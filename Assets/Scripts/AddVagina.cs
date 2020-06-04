using System.Collections.Generic;

namespace EssenceMenuStuff
{
    public class AddVagina : AddOrgan
    {
        private List<Vagina> Vaginas => Player.SexualOrgans.Vaginas;

        protected override Essence Ess => Player.Essence.Femi;

        protected override float Cost => Vaginas.Cost();

        protected override void DisplayCost() => btnText.text = $"Add vagina: {Cost}Femi";

        protected override void AddFunc()
        {
            if (CanAfford)
            {
                Ess.Lose(Cost);
                Vaginas.AddVag();
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}