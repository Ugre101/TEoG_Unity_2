using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    // public InventoryItem item;
    public bool Empty = true;

    public int Id;

    public void AddTo(DragInventory item)
    {
        DragInventory drag = Instantiate(item, transform);
        drag.SlotId = Id;
        Empty = false;
    }

    public void Clean()
    {
        Empty = true;
        transform.KillChildren();
    }
}