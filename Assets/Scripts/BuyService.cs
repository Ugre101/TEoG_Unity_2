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
}