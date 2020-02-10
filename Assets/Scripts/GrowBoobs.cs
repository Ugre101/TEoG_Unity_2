namespace EssenceMenu
{
    public class GrowBoobs : GrowOrgan
    {
        private Essence Femi => player.Essence.Femi;
        private Boobs boobs;

        public void Setup(PlayerMain player, Boobs boobs)
        {
            this.boobs = boobs;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(boobs.Size)} {boobs.GrowCost()}Femi";

        protected override void Grow()
        {
            if (Femi.Amount >= boobs.GrowCost())
            {
                boobs.Grow();
                DisplayCost();
            }
        }
    }
}