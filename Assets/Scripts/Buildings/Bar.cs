using System.Collections.Generic;
using UnityEngine;

namespace Bar
{
    public class Bar : MonoBehaviour
    {
        [SerializeField]
        private PlayerMain player = null;
        [SerializeField]
        private Transform container;
        [SerializeField]
        private BarMeal barMealPrefab = null;

        private List<BuyMeal> meals = new List<BuyMeal>();

        // Start is called before the first frame update
        private void Start()
        {
            meals.Add(new BuyMeal(new Meal(3), 3, "Small meal"));
            meals.Add(new BuyMeal(new Meal(5), 5, "Medium meal"));
            meals.Add(new BuyMeal(new Meal(8), 8, "Large meal"));
            container.KillChildren();
            meals.ForEach(m =>
            {
                BarMeal temp = Instantiate(barMealPrefab, container);
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