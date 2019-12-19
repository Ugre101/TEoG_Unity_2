using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IShopWare
{
    Button BuyBtn { get; }
    int Cost { get; }

    void Buy(BasicChar buyer);
}

public abstract class ShopWare : MonoBehaviour, IShopWare
{
    [SerializeField]
    protected TextMeshProUGUI title, desc, displayCost;

    [SerializeField] protected Image frame;

    [field: SerializeField] public Button BuyBtn { get; private set; }

    public int Cost { get; protected set; }

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

    public virtual void CanAfford(BasicChar buyer)
    {
        frame.color = buyer.Currency.CanAfford(Cost) ? Color.green : Color.red;
    }
}

public abstract class Ware
{
    public int Cost { get; protected set; }
    public string Title { get; protected set; }
    public string Desc { get; protected set; }
}