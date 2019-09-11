using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventoryItem : MonoBehaviour
{
    public Item Item;

    public Button Use;
    public TextMeshProUGUI title, amount;

    public void UseItem()
    {
        Debug.Log("Using item" + Item.name);
    //    Item.Use();
        //   amount.text = Item.Amount.ToString();
    }

    public void NewItem(Item item)
    {
        Item = item;
        title.text = item.Title;
        //amount.text = item.Amount.ToString();
    }

    public delegate void Used();

    public static event Used used;
}