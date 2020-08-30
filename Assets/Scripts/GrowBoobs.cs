namespace EssenceMenuStuff
{
    public class GrowBoobs : GrowOrgan
    {
        protected override Essence Ess => Player.Essence.Femi;

        protected override float Cost => boobs.Cost;

        private Boobs boobs;

        public void Setup(Boobs boobs)
        {
            this.boobs = boobs;
            BaseSetup();
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