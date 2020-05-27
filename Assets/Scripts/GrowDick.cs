namespace EssenceMenuStuff
{
    public class GrowDick : GrowOrgan
    {
        protected override Essence Ess => player.Essence.Masc;

        protected override float Cost => dick.Cost;

        private Dick dick;

        public void Setup(PlayerMain player, Dick dick)
        {
            this.dick = dick;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(dick.Size)} {Cost}Masc";

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