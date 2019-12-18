using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class BarMeal : MonoBehaviour, IShopWare
    {
        [SerializeField]
        private TextMeshProUGUI title = null, desc = null;

        [SerializeField]
        private Button btn = null;

        private BuyMeal meal;

        public void Setup(BuyMeal parMeal)
        {
            meal = parMeal;
            title.text = meal.Title;
            desc.text = $"+hp: {meal.Meal.HpGain}, +wp: {meal.Meal.WpGain}, +fat: {meal.Meal.FatGain}";
            Cost = meal.Cost;
        }

        public int Cost { get; private set; }

        public void Buy(PlayerMain player)
        {
            if (player.TryToBuy(Cost))
            {
                player.Body.Fat.GainFlat(meal.Meal.FatGain);
                player.HP.Gain(meal.Meal.HpGain);
                player.WP.Gain(meal.Meal.WpGain);
            }
        }
    }
}