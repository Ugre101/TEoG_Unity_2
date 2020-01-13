using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopWare : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title, desc, displayCost;

    [SerializeField] protected Image frame;

    [field: SerializeField] public Button BuyBtn { get; protected set; }

    [SerializeField] protected int Cost;

    public virtual void Buy(BasicChar buyer)
    {
        if (!buyer.Currency.CanAfford(Cost))
        {
            return;
        }
    }

    public virtual void Setup(Ware item)
    {
        
    }

    public virtual void FrameCanAfford(BasicChar buyer)
    {
        frame.color = buyer.Currency.CanAfford(Cost) ? Color.green : Color.red;
    }
}

[System.Serializable]
public abstract class Ware
{
    [field: SerializeField] public int Cost { get; protected set; }
    [field: SerializeField] public string Title { get; protected set; }

    [field: TextArea]
    [field: SerializeField] public string Desc { get; protected set; }

    public Ware(int cost, string title, string desc)
    {
        this.Cost = cost;
        this.Title = title;
        this.Desc = desc;
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