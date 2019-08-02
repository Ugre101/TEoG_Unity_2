using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
   // public InventoryItem item;
    public bool Empty = true;
    public int Id;
    public void AddTo(GameObject item)
    {
        DragInventory drag = item.GetComponent<DragInventory>();
        drag.SlotId = Id;
        Instantiate(item, this.transform);
        Empty = false;
    }
    public void Clean()
    {
        Empty = true;
        if (this.transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }
}
