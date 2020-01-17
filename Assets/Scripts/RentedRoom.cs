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
                SetTexts(room.Title, room.Desc, Cost.ToString());
                BuyBtn.onClick.AddListener(() => Buy(buyer));
                FrameCanAfford(buyer);
                buyer.Currency.GoldChanged += delegate { FrameCanAfford(buyer); };
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}