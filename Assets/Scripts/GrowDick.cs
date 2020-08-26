namespace EssenceMenuStuff
{
    public class GrowDick : GrowOrgan
    {
        protected override Essence Ess => Player.Essence.Masc;

        protected override float Cost => dick.Cost;

        private Dick dick;

        public void Setup(Dick dick)
        {
            this.dick = dick;
            BaseSetup();
        }

        protected override void DisplayCost() => btnText.text = $"{dick.Size.MorInch()} {Cost}Masc";

        protected override void Grow()
        {
            if (CanAfford)
            {
                Ess.Lose(dick.Grow());
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}