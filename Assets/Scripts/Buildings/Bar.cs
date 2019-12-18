using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class Bar : MonoBehaviour
    {
        public PlayerMain player;
        public Button rest, small, medium, large;

        [SerializeField]
        private BarMeal barMealPrefab = null;

        [SerializeField]
        private List<BuyMeal> meals;

        // Start is called before the first frame update
        private void Start()
        {
            meals.Add(new BuyMeal(new Meal(3), 3, "Small meal"));
            meals.Add(new BuyMeal(new Meal(5), 5, "Medium meal"));
            meals.Add(new BuyMeal(new Meal(8), 8, "Large meal"));
            meals.ForEach(m =>
            {
                BarMeal temp = Instantiate(barMealPrefab, transform);
                temp.Setup(m);
            });
        }
    }

    [System.Serializable]
    public class BuyMeal
    {
        public BuyMeal(Meal parMeal, int parCost, string parTitle)
        {
            Meal = parMeal;
            Cost = parCost;
            Title = parTitle;
        }

        public string Title { get; private set; } = "Meal";
        public int Cost { get; private set; }
        public Meal Meal { get; private set; }
        [field: SerializeField] public Sprite img { get; private set; }
    }
}