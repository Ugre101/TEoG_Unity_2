using System.Collections.Generic;
using UnityEngine;


public class InventoryHandler : MonoBehaviour
{
    public GameObject ItemPrefab;
    public playerMain player;
    public GameObject ItemContainer;
    public List<Item> Items;
    private void OnEnable()
    {
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        foreach (Transform child in ItemContainer.transform)
        {
            Destroy(child.transform.gameObject);
        }
        foreach (Item item in player.Inventory.Items.FindAll(i => i.Amount < 1))
        {
            player.Inventory.Items.Remove(item);
        }
        foreach (Item item in player.Inventory.Items)
        {
            GameObject toAdd = ItemPrefab;
            DragInventory inventorySlot = toAdd.GetComponent<DragInventory>();
            inventorySlot.NewItem(item);
            void OutOf()
            {
               UpdateInventory();
            }
            DragInventory.used += OutOf;
            Instantiate(toAdd, ItemContainer.transform);
            
        }
    }
}
