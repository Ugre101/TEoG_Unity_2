using System.Collections.Generic;
using UnityEngine;


public class InventoryHandler : MonoBehaviour
{
    public GameObject ItemPrefab;
    public playerMain player;
    public List<Item> Items;

    public GameObject SlotsHolder;
    public GameObject SlotPrefab;
    public int AmountOfSlots = 40;
    private InventorySlot[] Slots;
    private void OnEnable()
    {
        Debug.Log(SlotsHolder.transform.childCount< AmountOfSlots);
        if (SlotsHolder.transform.childCount < AmountOfSlots)
        {
            for (int i = SlotsHolder.transform.childCount; i < AmountOfSlots; i++)
            {
                Instantiate(SlotPrefab, SlotsHolder.transform);
            }
            Slots = SlotsHolder.GetComponentsInChildren<InventorySlot>();
        }
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        foreach (Item item in player.Inventory.Items.FindAll(i => i.Amount < 1))
        {
            player.Inventory.Items.Remove(item);
        }
        for (int i = 0; i < player.Inventory.Items.Count; i++)
        {
            Item item = player.Inventory.Items[i];
            GameObject toAdd = ItemPrefab;
            DragInventory inventorySlot = toAdd.GetComponent<DragInventory>();
            inventorySlot.NewItem(item);
            void OutOf()
            {
                UpdateInventory();
            }
            DragInventory.used += OutOf;
            Slots[i].Id = i;
            Slots[i].AddTo(toAdd);
        }
    }
}
