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
                buyer.Eat(meal.Meal);
            }
        }
    }
}