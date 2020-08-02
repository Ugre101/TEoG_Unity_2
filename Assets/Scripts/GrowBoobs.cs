namespace EssenceMenuStuff
{
    public class GrowBoobs : GrowOrgan
    {
        protected override Essence Ess => player.Essence.Femi;

        protected override float Cost => boobs.Cost;

        private Boobs boobs;

        public void Setup(PlayerMain player, Boobs boobs)
        {
            this.boobs = boobs;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{boobs.Size.MorInch()} {boobs.Cost}Femi";

        protected override void Grow()
        {
            if (CanAfford)
            {
                Ess.Lose(boobs.Grow());
                DisplayCost();
                ShowIfCanAfford(); 
            }
        }
    }
}