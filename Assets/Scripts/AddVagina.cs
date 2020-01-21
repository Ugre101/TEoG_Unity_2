using System.Collections.Generic;

namespace EssenceMenu
{
    public class AddVagina : AddOrgan
    {
        private Essence Femi => player.Essence.Femi;
        private List<Vagina> Vaginas => player.SexualOrgans.Vaginas;

        protected override void DisplayCost() => btnText.text = $"Add vagina: {Vaginas.Cost()}Femi";

        protected override void AddFunc()
        {
            if (Femi.Amount > Vaginas.Cost())
            {
                Femi.Lose(Vaginas.Cost());
                Vaginas.AddVag();
                DisplayCost();
            }
        }
    }
}