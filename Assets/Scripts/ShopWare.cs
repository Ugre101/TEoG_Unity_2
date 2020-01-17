using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopWare : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title, desc, displayCost;

    protected void SetTexts(string title, string desc, string cost)
    {
        this.title.text = title;
        this.desc.text = desc;
        this.displayCost.text = cost;
    }

    [SerializeField] protected Image frame;

    [SerializeField] protected int Cost;
    [SerializeField] private Button buyBtn;
    public Button BuyBtn => buyBtn;

    public virtual void Buy(BasicChar buyer)
    {
        if (!buyer.Currency.CanAfford(Cost))
        {
            return;
        }
    }

    public virtual void Setup(Ware item, BasicChar buyer)
    {
        BuyBtn.onClick.AddListener(() => Buy(buyer));
    }

    [SerializeField] protected Color affordColor = Color.green, cantAffordColor = Color.red;

    public virtual void FrameCanAfford(BasicChar buyer)
    {
        frame.color = buyer.Currency.CanAfford(Cost) ? affordColor : cantAffordColor;
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
    [SerializeField] private ItemId itemId;
    public ItemId ItemId => itemId;

    public ItemWare(int cost, string title, string desc, ItemId itemId) : base(cost, title, desc)
    {
        this.itemId = itemId;
    }

    public override void OnBuy(BasicChar basicChar)
    {
        basicChar.Inventory.AddItem(ItemId);
    }
}