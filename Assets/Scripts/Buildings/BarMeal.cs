using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class BarMeal : ShopWare
    {
        [SerializeField] private Image icon = null;
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
                FrameCanAfford(buyer.Currency.Gold);
                buyer.Currency.GoldChanged += FrameCanAfford;
                if (meal.Img != null)
                {
                    icon.sprite = meal.Img;
                }
                else
                {
                    icon.gameObject.SetActive(false);
                }
            }
            else { Destroy(gameObject); }
        }

        protected override void Buy()
        {
            if (buyer.Currency.TryToBuy(Cost))
            {
                buyer.Eat(meal.Meal);
            }
        }
    }
}