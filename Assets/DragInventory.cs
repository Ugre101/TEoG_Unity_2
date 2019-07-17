using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler, IPointerClickHandler
{
    public Item item;
    public int SlotId;
    public Vector2 StartPos;
    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        StartPos = pointerEvent.position;
    }
    public void OnDrag(PointerEventData pointerEvent)
    {
        this.transform.position = pointerEvent.position;
    }
    public void OnEndDrag(PointerEventData pointerEvent)
    {
        this.transform.position = StartPos;
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
    }
    public void UseItem()
    {
        Debug.Log("Using item" + item.name);
        item.Use();
        //amount.text = Item.Amount.ToString();
        if (item.Amount < 1)
        {
            used?.Invoke();
        }
    }
    public void NewItem(Item theitem)
    {
        item = theitem;
       // title.text = item.Title;
        //amount.text = item.Amount.ToString();
    }
    public delegate void Used();
    public static event Used used;
}
