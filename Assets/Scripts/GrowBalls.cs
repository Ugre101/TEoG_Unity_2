namespace EssenceMenuStuff
{
    public class GrowBalls : GrowOrgan
    {
        protected override Essence Ess => Player.Essence.Masc;

        protected override float Cost => balls.Cost;

        private Balls balls;

        public void Setup(Balls balls)
        {
            this.balls = balls;
            BaseSetup();
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