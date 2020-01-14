namespace Bar
{
    public class BarMeal : ShopWare
    {
        private BuyMeal meal;

        public override void Setup(Ware ware, BasicChar buyer)
        {
            meal = ware as BuyMeal;
            title.text = meal.Title;
            Cost = meal.Cost;
            displayCost.text = meal.Cost.ToString();
            desc.text = $"+hp: {meal.Meal.HpGain}, +wp: {meal.Meal.WpGain}, +fat: {meal.Meal.FatGain}";
            BuyBtn.onClick.AddListener(() => Buy(buyer));
            buyer.Currency.GoldChanged += delegate { FrameCanAfford(buyer); };
        }

        public override void Buy(BasicChar buyer)
        {
            if (buyer.Currency.TryToBuy(Cost))
            {
                buyer.Body.Fat.GainFlat(meal.Meal.FatGain);
                buyer.HP.Gain(meal.Meal.HpGain);
                buyer.WP.Gain(meal.Meal.WpGain);
                if (meal.Meal is MealWithBuffs hasBuffs)
                {
                    if (hasBuffs.TempMods.Count > 0)
                    {
                        buyer.Stats.AddTempMods(hasBuffs.TempMods);
                    }
                    if (hasBuffs.TempHealthMods.Count > 0)
                    {
                        hasBuffs.TempHealthMods.ForEach(m =>
                        {
                            if (m.HealthType == HealthTypes.Health)
                            {
                                buyer.HP.AddTempMod(m);
                            }
                            else if (m.HealthType == HealthTypes.WillPower)
                            {
                                buyer.WP.AddTempMod(m);
                            }
                        });
                    }
                }
            }
        }
    }
}