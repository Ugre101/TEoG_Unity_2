using System.Collections.Generic;
using UnityEngine;

namespace Bar
{
    public class Bar : Building
    {
        [SerializeField]
        private Transform container = null;

        [SerializeField]
        private BarMeal barMealPrefab = null;

        [SerializeField]
        private RentedRoom roomPrefab = null;

        private readonly List<BuyMeal> meals = new List<BuyMeal>() { new BuyMeal(new Meal(3), 3, "Small meal"), new BuyMeal(new Meal(5), 5, "Medium meal"), new BuyMeal(new MealWithBuffs(8, new List<TempStatMod>() { new TempStatMod(1, StatTypes.Str, ModTypes.Flat, "Large meal", 12) }, new List<TempHealthMod>() { new TempHealthMod(10, ModTypes.Flat, HealthTypes.Health, "Large meal", 12) }), 8, "Large meal") };
        private readonly List<RentRoomBasic> rooms = new List<RentRoomBasic>() { new RentRoomBasic() };

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            container.KillChildren();
            meals.ForEach(m =>
            {
                BarMeal temp = Instantiate(barMealPrefab, container);
                temp.Setup(m);
                temp.BuyBtn.onClick.AddListener(() => temp.Buy(player));
            });
            rooms.ForEach(r =>
            {
                RentedRoom temp = Instantiate(roomPrefab, container);
                temp.Setup(r);
                temp.BuyBtn.onClick.AddListener(() => temp.Buy(player));
            });
        }
    }

    [System.Serializable]
    public class BuyMeal : Ware
    {
        public BuyMeal(Meal parMeal, int parCost, string parTitle)
        {
            Meal = parMeal;
            Cost = parCost;
            Title = parTitle;
        }

        public Meal Meal { get; private set; }
        [field: SerializeField] public Sprite Img { get; private set; }
    }

    public class RentRoomBasic : Ware
    {
        public RentRoomBasic()
        {
            Title = "Basic room";
            Desc = "Sleep for 8hours and wake up fully restored.";
            Cost = 8;
        }

        public virtual void Sleep(BasicChar basicChar)
        {
            DateSystem.PassHour(8);
            basicChar.HP.FullGain();
            basicChar.WP.FullGain();
            // TODO temp bonus
        }
    }
}