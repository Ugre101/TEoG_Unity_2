using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour, IDragHandler,IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    public InventoryItem invItem;
    public Item item;
    public int SlotId;

    private InventoryHandler inventory;
    private void OnEnable()
    {
        inventory = GetComponentInParent<InventoryHandler>();
        this.transform.position = this.transform.parent.position;
    }

    public void OnDrag(PointerEventData pointerEvent)
    {
        this.transform.position = pointerEvent.position;
    }
    public void OnEndDrag(PointerEventData pointerEvent)
    {
        InventorySlot slot = pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
        if (slot != null)
        {
            inventory.Move(this.gameObject, SlotId, slot.Id);
        }
        else
        {
            this.transform.position = this.transform.parent.position;
        }
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
    }
    public void UseItem()
    {
        Debug.Log("Using item" + item.name);
        item.Use();
        //amount.text = Item.Amount.ToString();
        if (invItem.amount < 1)
        {
            used?.Invoke();
        }
    }
    public void NewItem(InventoryItem Invitem, int slot)
    {
        invItem = Invitem;
        item = Invitem.item;
    }
    public delegate void Used();
    public static event Used used;
}
