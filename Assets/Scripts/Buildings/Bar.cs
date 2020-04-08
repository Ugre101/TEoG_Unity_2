using System.Collections.Generic;
using UnityEngine;

namespace Bar
{
    public class Bar : Building
    {
        [SerializeField] private Transform container = null;

        [SerializeField] private BarMeal barMealPrefab = null;

        [SerializeField] private RentedRoom roomPrefab = null;

        [SerializeField] private List<BuyMeal> mealsWithBuffs = new List<BuyMeal>();
        private readonly List<RentRoomBasic> rooms = new List<RentRoomBasic>() { new RentRoomBasic() };

        public override void OnEnable()
        {
            base.OnEnable();
        }

        // Start is called before the first frame update
        public void Start()
        {
            container.KillChildren();
            mealsWithBuffs.ForEach(m => Instantiate(barMealPrefab, container).Setup(m, player));
            rooms.ForEach(r => Instantiate(roomPrefab, container).Setup(r, player));
        }
    }

    [System.Serializable]
    public class BuyMeal : Ware
    {
        [SerializeField] private MealWithBuffs meal;
        [SerializeField] private Sprite img = null;

        public BuyMeal(MealWithBuffs parMeal, int parCost, string parTitle) : base(parCost, parTitle, "")
        {
            meal = parMeal;
        }

        public Meal Meal => meal;
        public Sprite Img => img;
    }

    [System.Serializable]
    public class RentRoomBasic : Ware
    {
        public RentRoomBasic() : base(8, "Basic room", "Sleep for 8hours and wake up fully restored.")
        {
        }

        private string Source => this.GetType().Name;

        public virtual void Sleep(BasicChar basicChar)
        {
            DateSystem.PassHour(8);
            basicChar.HP.AddTempMod(TempHealthMod.CreateFlatHealth(5, Source, 12));
            basicChar.WP.AddTempMod(TempHealthMod.CreateFlatWill(5, Source, 12));
        }
    }
}