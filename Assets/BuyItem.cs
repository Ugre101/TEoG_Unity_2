public class BuyItem : ShopWare
{
    private ItemWare itemWare;

    public override void Setup(Ware item)
    {
        if (item is ItemWare ware)
        {
            itemWare = ware;
        }
    }

    public override void Buy(BasicChar buyer)
    {
        if (buyer.Currency.TryToBuy(Cost))
        {
            buyer.Inventory.AddItem(itemWare.ItemId);
        }
    }
}