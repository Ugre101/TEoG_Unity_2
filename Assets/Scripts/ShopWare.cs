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

public abstract class Ware
{
    public int Cost { get; protected set; }
    public string Title { get; protected set; }
    public string Desc { get; protected set; }

    public Ware(int cost, string title, string desc)
    {
        this.Cost = cost;
        this.Title = title;
        this.Desc = desc;
    }
}

public class ItemWare : Ware
{
    public ItemId ItemId { get; protected set; }

    public ItemWare(int cost, string title, string desc, ItemId itemId) : base(cost, title, desc)
    {
        this.ItemId = itemId;
    }
}