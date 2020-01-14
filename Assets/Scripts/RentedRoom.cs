namespace Bar
{
    public class RentedRoom : ShopWare
    {
        private RentRoomBasic room;

        public override void Buy(BasicChar renter)
        {
            if (renter.Currency.TryToBuy(Cost))
            {
                room.Sleep(renter);
            }
        }

        public override void Setup(Ware item, BasicChar buyer)
        {
            if (item is RentRoomBasic room)
            {
                this.room = room;
                Cost = room.Cost;
                title.text = room.Title;
                desc.text = room.Desc;
                displayCost.text = Cost.ToString();
                BuyBtn.onClick.AddListener(() => Buy(buyer));
                buyer.Currency.GoldChanged += delegate { FrameCanAfford(buyer); };
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}