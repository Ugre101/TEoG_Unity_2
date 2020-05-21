using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyService : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null, desc = null, sellPrice = null;
    [SerializeField] private Button btn = null;

    public void Setup(string title, string desc, float price, UnityAction useAction)
    {
        this.title.text = title;
        this.desc.text = desc;
        sellPrice.text = $"{price}g";
        btn.onClick.AddListener(useAction);
    }

    public void Setup(BuySerciveInfo serciveInfo) => Setup(serciveInfo.Title, serciveInfo.Desc, serciveInfo.Price, serciveInfo.Action);
}

public struct BuySerciveInfo
{
    public BuySerciveInfo(string title, string desc, float price, UnityAction action)
    {
        Title = title;
        Desc = desc;
        Price = price;
        Action = action;
    }

    public string Title { get; }
    public string Desc { get; }
    public float Price { get; }
    public UnityAction Action { get; }
}