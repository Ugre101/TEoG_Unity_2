using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wares : MonoBehaviour
{
    [SerializeField] private Transform container = null;
    public Transform Container => container;
    [SerializeField] private Button sellBtn = null;
    [SerializeField] private TextMeshProUGUI sellBtnText = null;
    [SerializeField] private BuyItem buyItem = null;
    [SerializeField] private BuyService service = null;
    [SerializeField] private SellItem sellItem = null;
    [SerializeField] private ItemHolder items = null;

    public void BuyItems(BasicChar buyer, List<ItemIds> ids)
    {
        foreach (ItemIds id in ids)
        {
            Instantiate(buyItem, container).Setup(buyer, items.GetById(id));
        }
    }

    public void BuyServices(BasicChar buyer)
    {
    }

    public void SellItems(BasicChar seller)
    {
        foreach (InventoryItem item in seller.Inventory.Items)
        {
            Instantiate(sellItem, container).Setup(items.GetById(item.Id), item, seller);
        }
    }
}