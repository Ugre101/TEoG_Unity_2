namespace EssenceMenu
{
    public class GrowBalls : GrowOrgan
    {
        private Essence Masc => player.Essence.Masc;
        private Balls balls;

        public void Setup(PlayerMain player, Balls balls)
        {
            this.balls = balls;
            BaseSetup(player);
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(balls.Size)} {balls.Cost}Masc";

        protected override void Grow()
        {
            if (Masc.Amount >= balls.Cost)
            {
                balls.Grow();
                DisplayCost();
            }
        }
    }
}