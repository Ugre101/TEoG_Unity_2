using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryHandler : MonoBehaviour
{
    public GameObject ItemPrefab;
    public playerMain player;
    //  public List<Item> Items;
    public Items items;

    public GameObject SlotsHolder;
    public GameObject SlotPrefab;
    public int AmountOfSlots = 40;
    private InventorySlot[] Slots;

    private void Awake()
    {
        DragInventory.used += UpdateInventory;
    }
    private void OnEnable()
    {
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
        foreach(InventorySlot slot in Slots)
        {
            if (!slot.Empty)
            {
                slot.Clean();
            }
        }
        player.Inventory.Items.RemoveAll(i => i.amount < 1);
        foreach(InventoryItem item in player.Inventory.Items)
        {
            GameObject toAdd = ItemPrefab;
            DragInventory inventorySlot = toAdd.GetComponent<DragInventory>();
            inventorySlot.item = items.items.Find(i => i.Id == item.id);
            inventorySlot.NewItem(item,item.invPos);
            void OutOf()
            {
                UpdateInventory();
            }
            DragInventory.used += OutOf;
            Slots[item.invPos].AddTo(toAdd);
        }
    }
    public void Move(GameObject item,int startSlot, int EndSlot)
    {
        if (Slots[EndSlot].Empty)
        {
            player.Inventory.Items.Find(i => i.invPos == startSlot).invPos = EndSlot;
            //UpdateInventory();
            Slots[EndSlot].Empty = false;
            Slots[startSlot].Empty = true;
        }
    }
}
