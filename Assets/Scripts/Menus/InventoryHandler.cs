using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private DragInventory ItemPrefab = null;
    [SerializeField] private InventorySlot SlotPrefab = null;
    [SerializeField] private InventoryHoverText inventoryHoverText = null;
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private ItemHolder items = null;
    [SerializeField] private GameObject SlotsHolder = null;
    [SerializeField] private Button sortAll = null, sortEatDrink = null, sortMisc = null;
    [SerializeField] private PromptYesNo yesNo = null;
    [SerializeField] private int AmountOfSlots = 40;

    private List<InventoryItem> Items => player.Inventory.Items;

    //  public List<Item> Items;
    private InventorySlot[] Slots;

    private Color selected = new Color(0.5f, 0.5f, 0.5f, 1f), notSelected = new Color(0, 0, 0, 1);

    private void Awake() => DragInventory.UsedEvent += UpdateInventory;

    private void OnEnable()
    {
        ToggleButtons(sortAll);
        int SlotCount = SlotsHolder.transform.childCount;
        if (SlotCount < AmountOfSlots)
        {
            for (int i = SlotCount; i < AmountOfSlots; i++)
            {
                Instantiate(SlotPrefab, SlotsHolder.transform).SetId(i);
            }
            Slots = SlotsHolder.GetComponentsInChildren<InventorySlot>();
        }
        UpdateInventory();
    }

    private void Start()
    {
        sortAll.onClick.AddListener(() => { UpdateInventory(); ToggleButtons(sortAll); });
        sortEatDrink.onClick.AddListener(() => { UpdateInventory(ItemTypes.Consumables); ToggleButtons(sortEatDrink); });
        sortMisc.onClick.AddListener(() => { UpdateInventory(ItemTypes.Misc); ToggleButtons(sortMisc); });
        player.EquiptItems.GetAll.ForEach(e => e.GotItem += UpdateInventory);
    }

    public void UpdateInventory()
    {
        Items.RemoveAll(i => i.Amount < 1);
        ShowInventory(Items);
    }

    public void UpdateInventory(ItemTypes parType)
    {
        Items.RemoveAll(i => i.Amount < 1);
        List<InventoryItem> sorted = (from item in items.ItemsDict
                                      join invItem in Items
                                      on item.ItemId equals invItem.Id
                                      where item.Type == parType
                                      select invItem).ToList();
        ShowInventory(sorted);
    }

    private void ShowInventory(List<InventoryItem> inventoryItems)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (!slot.Empty)
            {
                slot.Clean();
            }
        }
        inventoryItems.ForEach(i =>
            Instantiate(ItemPrefab, Slots[i.InvPos].transform).NewItem(this, i, items.GetById(i.Id), inventoryHoverText));
    }

    public void ToggleButtons(Button selectedBtn)
    {
        sortAll.image.color = sortAll.name == selectedBtn.name ? selected : notSelected;
        sortEatDrink.image.color = sortEatDrink.name == selectedBtn.name ? selected : notSelected;
        sortMisc.image.color = sortMisc.name == selectedBtn.name ? selected : notSelected;
    }

    public void Move(int startSlot, int EndSlot)
    {
        if (Slots[EndSlot].Empty && !Items.ExistByPos(EndSlot))
        {
            Items.FindByPos(startSlot).InvPos = EndSlot;
            //UpdateInventory();
        }
    }

    public void Move(int startSlot)
    {
        if (Items.ExistByPos(startSlot))
        {
            InventoryItem inv = Items.FindByPos(startSlot);
            Instantiate(yesNo, transform).Setup(() => RemoveItem(inv), "Do you want to delete item?");
        }
    }

    private void RemoveItem(InventoryItem itemToRemove)
    {
        player.Inventory.Items.Remove(itemToRemove);
        UpdateInventory();
    }
}