using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopWare : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title, desc, displayCost;
    protected BasicChar buyer;

    protected void SetTexts(string title, string desc, string cost)
    {
        this.title.text = title;
        this.desc.text = desc;
        this.displayCost.text = cost;
    }

    [SerializeField] protected Image frame = null;

    [SerializeField] protected int Cost;
    [SerializeField] private Button buyBtn = null;
    public Button BuyBtn => buyBtn;

    public virtual void Buy()
    {
        if (!buyer.Currency.CanAfford(Cost))
        {
            return;
        }
    }

    public virtual void Setup(Ware item, BasicChar buyer)
    {
        BuyBtn.onClick.AddListener(Buy);
    }

    [SerializeField] protected Color affordColor = Color.green, cantAffordColor = Color.red;

    public virtual void FrameCanAfford(float gold)
    {
        if (buyer != null)
        {
            frame.color = gold >= Cost ? affordColor : cantAffordColor;
        }
    }
}

[System.Serializable]
public abstract class Ware
{
    [SerializeField] private int cost;
    [SerializeField] private string title;

    [TextArea]
    [SerializeField] private string desc;

    public int Cost => cost;
    public string Title => title;
    public string Desc => desc;

    public Ware(int cost, string title, string desc)
    {
        this.cost = cost;
        this.title = title;
        this.desc = desc;
    }

    public virtual void OnBuy(BasicChar basicChar)
    {
    }
}

[System.Serializable]
public class ItemWare : Ware
{
    [SerializeField] private ItemIds itemId;
    public ItemIds ItemId => itemId;

    public ItemWare(int cost, string title, string desc, ItemIds itemId) : base(cost, title, desc)
    {
        this.itemId = itemId;
    }

    public override void OnBuy(BasicChar basicChar)
    {
        basicChar.Inventory.AddItem(ItemId);
    }
}