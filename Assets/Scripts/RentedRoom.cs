namespace Bar
{
    public class RentedRoom : ShopWare
    {
        private RentRoomBasic room;

        public override void Buy()
        {
            if (buyer.Currency.TryToBuy(Cost))
            {
                room.Sleep(buyer);
            }
        }

        public override void Setup(Ware item, BasicChar buyer)
        {
            if (item is RentRoomBasic room)
            {
                this.room = room;
                Cost = room.Cost;
                SetTexts(room.Title, room.Desc, Cost.ToString());
                BuyBtn.onClick.AddListener(Buy);
                FrameCanAfford();
                buyer.Currency.GoldChanged += FrameCanAfford;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}