namespace Bar
{
    public class BarMeal : ShopWare
    {
        private BuyMeal meal;

        public override void Setup(Ware ware, BasicChar buyer)
        {
            if (ware is BuyMeal meal)
            {
                this.meal = meal;
                this.buyer = buyer;
                Cost = meal.Cost;
                SetTexts(meal.Title, meal.Desc, meal.Cost.ToString());
                BuyBtn.onClick.AddListener(Buy);
                FrameCanAfford();
                buyer.Currency.GoldChanged += FrameCanAfford;
            }
            else { Destroy(gameObject); }
        }

        public override void Buy()
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