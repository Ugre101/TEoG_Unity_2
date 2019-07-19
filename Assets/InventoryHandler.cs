using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
                GameObject SlotToAdd = SlotPrefab;
                InventorySlot slot = SlotToAdd.GetComponent<InventorySlot>();
                slot.Id = i;
                Instantiate(SlotToAdd, SlotsHolder.transform);
            }
            Slots = SlotsHolder.GetComponentsInChildren<InventorySlot>();
        }
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        if (player.Inventory.Items.Exists(i => i.amount < 1))
        {
            foreach (InventoryItem item in player.Inventory.Items.FindAll(i => i.amount < 1))
            {
                player.Inventory.Items.Remove(item);
            }
        }
        if (player.Inventory.Items.Exists(i => i.invPos == -1))
        {
            foreach (InventoryItem invItem in player.Inventory.Items.FindAll(i => i.invPos == -1))
            {
                int i = 0;
                while (!Slots[i].Empty)
                {
                    i++;
                    if (i > AmountOfSlots)
                    {
                        Debug.Log("inv error");
                        return;
                    }
                }
                invItem.invPos = i;
                InventoryItem item = player.Inventory.Items[i];
                GameObject toAdd = ItemPrefab;
                DragInventory inventorySlot = toAdd.GetComponent<DragInventory>();
                inventorySlot.NewItem(item, i);
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
    public void Move(GameObject item,int startSlot, int EndSlot)
    {
        if (Slots[EndSlot].Empty)
        {
            player.Inventory.Items.Find(i => i.invPos == startSlot).invPos = EndSlot;
            UpdateInventory();
          //  Slots[EndSlot].AddTo(item);
          //  Slots[startSlot].Clean();
        }
    }
}
