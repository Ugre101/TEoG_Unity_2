namespace Bar
{
    public class BarMeal : ShopWare
    {
        private BuyMeal meal;

        public override void Setup(Ware ware)
        {
            meal = ware as BuyMeal;
            title.text = meal.Title;
            Cost = meal.Cost;
            displayCost.text = meal.Cost.ToString();
            desc.text = $"+hp: {meal.Meal.HpGain}, +wp: {meal.Meal.WpGain}, +fat: {meal.Meal.FatGain}";
        }

        public override void Buy(BasicChar buyer)
        {
            if (buyer.Currency.TryToBuy(Cost))
            {
                buyer.Body.Fat.GainFlat(meal.Meal.FatGain);
                buyer.HP.Gain(meal.Meal.HpGain);
                buyer.WP.Gain(meal.Meal.WpGain);
                if (meal.Meal.TempMods.Count > 0)
                {
                    buyer.Stats.AddTempMods(meal.Meal.TempMods);
                }
            }
        }
    }
}