﻿namespace EssenceMenu
{
    public class GrowDick : GrowOrgan
    {
        private Essence Masc => player.Essence.Masc;
        private Dick dick;

        public void Setup(PlayerMain player, Dick dick)
        {
            BaseSetup(player);
            this.dick = dick;
        }

        protected override void DisplayCost() => btnText.text = $"{Settings.MorInch(dick.Size)} {dick.Cost}Masc";

        protected override void Grow()
        {
            if (Masc.Amount >= dick.Cost)
            {
                dick.Grow();
                DisplayCost();
            }
        }
    }
}