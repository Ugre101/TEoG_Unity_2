using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopWare : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title, desc, displayCost;

    [SerializeField] protected Image frame;

    public Button BuyBtn => buyBtn;

    [SerializeField] protected int Cost;
    [SerializeField] private Button buyBtn;

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

    public virtual void FrameCanAfford(BasicChar buyer)
    {
        frame.color = buyer.Currency.CanAfford(Cost) ? Color.green : Color.red;
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
    [field: SerializeField] public ItemId ItemId { get; protected set; }

    public ItemWare(int cost, string title, string desc, ItemId itemId) : base(cost, title, desc)
    {
        this.ItemId = itemId;
    }

    public override void OnBuy(BasicChar basicChar)
    {
        basicChar.Inventory.AddItem(ItemId);
    }
}