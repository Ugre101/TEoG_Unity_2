using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private DragInventory ItemPrefab;

    [SerializeField]
    private InventorySlot SlotPrefab;

    public playerMain player;

    //  public List<Item> Items;
    public Items items;

    public GameObject SlotsHolder;

    public int AmountOfSlots = 40;
    private InventorySlot[] Slots;

    private void Awake() => DragInventory.used += UpdateInventory;

    private void OnEnable()
    {
        if (SlotsHolder.transform.childCount < AmountOfSlots)
        {
            for (int i = SlotsHolder.transform.childCount; i < AmountOfSlots; i++)
            {
                InventorySlot slot = Instantiate(SlotPrefab, SlotsHolder.transform);
                slot.Id = i;
            }
            Slots = SlotsHolder.GetComponentsInChildren<InventorySlot>();
        }
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        foreach (InventorySlot slot in Slots)
        {
            if (!slot.Empty)
            {
                slot.Clean();
            }
        }
        player.Inventory.Items.RemoveAll(i => i.amount < 1);
        foreach (InventoryItem item in player.Inventory.Items)
        {
            DragInventory dragInv = ItemPrefab;
            dragInv.item = items.items.Find(i => i.Id == item.id);
            dragInv.NewItem(this, item, item.invPos);
            Slots[item.invPos].AddTo(dragInv);
        }
    }

    public void Move(GameObject item, int startSlot, int EndSlot)
    {
        if (Slots[EndSlot].Empty)
        {
            player.Inventory.Items.Find(i => i.invPos == startSlot).invPos = EndSlot;
            //UpdateInventory();
            Slots[EndSlot].Empty = false;
            Slots[startSlot].Empty = true;
        }
    }

    public void Move(GameObject item, int startSlot)
    {
        //  player.Inventory.Items.Find(i => i.invPos == startSlot);
        //UpdateInventory();
        //  Slots[startSlot].Empty = true;
        Debug.Log("Remove item?");
    }
}