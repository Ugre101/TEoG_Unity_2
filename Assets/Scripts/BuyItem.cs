public class BuyItem : ShopWare
{
    private Ware itemWare;

    public void Setup(Ware ware, BasicChar buyer, Item item)
    {
        itemWare = ware;
        title.text = item.Title;
        desc.text = item.Desc;
        Cost = ware.Cost;
        displayCost.text = ware.Cost.ToString();
        FrameCanAfford(buyer);
        buyer.Currency.GoldChanged += delegate { FrameCanAfford(buyer); };
        BuyBtn.onClick.AddListener(() => Buy(buyer));
    }

    public override void Buy(BasicChar buyer)
    {
        if (buyer.Currency.TryToBuy(Cost))
        {
            itemWare.OnBuy(buyer);
        }
    }
}