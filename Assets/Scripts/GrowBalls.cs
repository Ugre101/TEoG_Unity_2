namespace EssenceMenuStuff
{
    public class GrowBalls : GrowOrgan
    {
        protected override Essence Ess => player.Essence.Masc;

        protected override float Cost => balls.Cost;

        private Balls balls;

        public void Setup(PlayerMain player, Balls balls)
        {
            this.balls = balls;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{balls.Size.MorInch()} {Cost}Masc";

        protected override void Grow()
        {
            if (CanAfford)
            {
                Ess.Lose(balls.Grow());
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}