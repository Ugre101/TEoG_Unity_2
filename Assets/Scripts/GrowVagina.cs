namespace EssenceMenuStuff
{
    public class GrowVagina : GrowOrgan
    {
        protected override Essence Ess => Player.Essence.Femi;

        protected override float Cost => vagina.Cost;

        private Vagina vagina;

        public void Setup(Vagina vagina)
        {
            this.vagina = vagina;
            BaseSetup();
        }

        protected override void DisplayCost() => btnText.text = $"{vagina.Size.MorInch()} {Cost}Femi";

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