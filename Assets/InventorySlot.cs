using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool Empty = true;
    public int Id;
    public void AddTo(GameObject item)
    {
        Instantiate(item, this.transform);
    }
}
