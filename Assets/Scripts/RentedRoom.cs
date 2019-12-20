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
            room = item as RentRoomBasic;
            title.text = room.Title;
            desc.text = room.Desc;
            Cost = room.Cost;
        }
    }
}