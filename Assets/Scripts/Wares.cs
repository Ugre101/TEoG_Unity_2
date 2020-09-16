using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wares : MonoBehaviour
{
    [SerializeField] private Transform container = null;
    public Transform Container => container;

    public Button SellBtn => sellBtn;
    public TextMeshProUGUI SellBtnText => sellBtnText;

    [SerializeField] private Button sellBtn = null;
    [SerializeField] private TextMeshProUGUI sellBtnText = null;
    [SerializeField] private BuyItem buyItem = null;
    [SerializeField] private BuyService service = null;
    [SerializeField] private SellItem sellItem = null;
    [SerializeField] private ItemHolder items = null;

    public void ClearContainer() => container.KillChildren();

    public void BuyItems(BasicChar buyer, IEnumerable<ItemIds> ids)
    {
        foreach (ItemIds id in ids)
        {
            Instantiate(buyItem, container).Setup(buyer, items.GetById(id));
        }
    }

    public void BuyServices(BuyServiceInfo serviceInfo) => Instantiate(service, container).Setup(serviceInfo);

    public void BuyServices(IEnumerable<BuyServiceInfo> serviceInfos)
    {
        foreach (BuyServiceInfo serviceInfo in serviceInfos)
        {
            BuyServices(serviceInfo);
        }
    }

    public void SellItems(BasicChar seller)
    {
        foreach (InventoryItem item in seller.Inventory.Items)
        {
            Instantiate(sellItem, container).Setup(items.GetById(item.Id), item, seller);
        }
    }

    public void SellItems(BasicChar seller, ItemTypes type)
    {
        foreach (InventoryItem item in seller.Inventory.Items.FindAll(i => items.GetById(i.Id).Type == type))
        {
            Instantiate(sellItem, container).Setup(items.GetById(item.Id), item, seller);
        }
    }

    /// <summary>Sell specific items</summary>
    public void SellItem(BasicChar seller, IEnumerable<InventoryItem> inventoryItems)
    {
        foreach (InventoryItem item in inventoryItems)
        {
            Instantiate(sellItem, container).Setup(items.GetById(item.Id), item, seller);
        }
    }
}