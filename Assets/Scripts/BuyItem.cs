using UnityEngine;
using UnityEngine.UI;

public class BuyItem : ShopWare
{
    [SerializeField] private Image icon = null;
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
        if (item.Sprite != null)
        {
            icon.sprite = item.Sprite;
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
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