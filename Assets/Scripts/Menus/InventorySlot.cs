using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public void SetId(int parId) => Id = parId;

    // public InventoryItem item;
    public bool Empty => transform.childCount == 0;

    public int Id { get; private set; }

    public void Clean() => transform.KillChildren();
}