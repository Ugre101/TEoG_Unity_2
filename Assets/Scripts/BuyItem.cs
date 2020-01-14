public class BuyItem : ShopWare
{
    private Ware itemWare;

    public override void Setup(Ware item, BasicChar buyer)
    {
        itemWare = item;
        title.text = item.Title;
        desc.text = item.Desc;
        Cost = item.Cost;
        displayCost.text = item.Cost.ToString();
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