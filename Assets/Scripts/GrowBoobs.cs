namespace EssenceMenu
{
    public class GrowBoobs : GrowOrgan
    {
        private Essence Femi => player.Essence.Femi;
        private Boobs boobs;

        public void Setup(PlayerMain player, Boobs boobs)
        {
            BaseSetup(player);
            this.boobs = boobs;
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(boobs.Size)} {boobs.Cost}Femi";

        protected override void Grow()
        {
            if (Femi.Amount >= boobs.Cost)
            {
                boobs.Grow();
                DisplayCost();
            }
        }
    }
}