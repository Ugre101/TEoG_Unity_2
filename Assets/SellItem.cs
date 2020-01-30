using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null, amount = null, desc = null, sellPrice = null;
    [SerializeField] private Button btn = null;
    private InventoryItem inventoryItem;
    private Item item;
    private BasicChar basicChar;

    public void Setup(Item item, InventoryItem inventoryItem, BasicChar seller)
    {
        this.item = item;
        this.inventoryItem = inventoryItem;
        this.basicChar = seller;
        title.text = item.Title;
        ShowAmount();
        desc.text = item.Desc;
        sellPrice.text = item.SellValue.ToString() + "g";
        btn.onClick.AddListener(Sell);
    }

    private void ShowAmount() => amount.text = inventoryItem.Amount.ToString();

    private void Sell()
    {
        if (inventoryItem.Amount > 0)
        {
            basicChar.Currency.Gold += item.SellValue;
            inventoryItem.Amount--;
            if (inventoryItem.Amount < 1)
            {
                basicChar.Inventory.Clean();
                Destroy(gameObject);
            }
            else
            {
                ShowAmount();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}