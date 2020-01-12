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

        public override void Setup(Ware item)
        {
            if (item is RentRoomBasic room)
            {
                this.room = room;
                Cost = room.Cost;
                title.text = room.Title;
                desc.text = room.Desc;
                displayCost.text = Cost.ToString();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}