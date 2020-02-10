public class BuyItem : ShopWare
{
    private Item item;

    public void Setup(BasicChar buyer, Item item)
    {
        this.item = item;
        this.buyer = buyer;
        title.text = item.Title;
        desc.text = item.Desc;
        Cost = item.Value;
        displayCost.text = item.Value.ToString();
        FrameCanAfford();
        buyer.Currency.GoldChanged += FrameCanAfford;
        BuyBtn.onClick.AddListener(Buy);
    }

    private void OnDestroy()
    {
        if (buyer != null)
        {
            buyer.Currency.GoldChanged -= FrameCanAfford;
        }
        BuyBtn.onClick.RemoveAllListeners();
    }

    public override void Buy()
    {
        if (buyer.Currency.TryToBuy(Cost))
        {
            buyer.Inventory.AddItem(item.ItemId);
        }
    }
}