namespace EssenceMenu
{
    public class GrowVagina : GrowOrgan
    {
        protected override Essence Ess => player.Essence.Femi;

        protected override float Cost => vagina.Cost;

        private Vagina vagina;

        public void Setup(PlayerMain player, Vagina vagina)
        {
            this.vagina = vagina;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(vagina.Size)} {Cost}Femi";

        protected override void Grow()
        {
            if (CanAfford)
            {
                Ess.Lose(vagina.Grow());
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}