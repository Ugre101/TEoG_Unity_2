using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public InventoryItem invItem;
    public Item item;
    public int SlotId;

    private InventoryHandler inventory;
    private InventoryHoverText hoverText;
    private Transform Parent;
    private Image image { get { return GetComponent<Image>(); } }
    private float firstClick;
  
  
    private void Awake()
    {
        hoverText = this.GetComponentInParent<InventoryHoverText>();
        inventory = GetComponentInParent<InventoryHandler>();
     //   spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        Parent = transform.parent;
        transform.position = transform.parent.position;
      
    }
    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        hoverText.StopHovering();
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.transform.SetParent(Parent.parent);
    }
    public void OnDrag(PointerEventData pointerEvent) => this.transform.position = pointerEvent.position;
    public void OnEndDrag(PointerEventData pointerEvent)
    {
        this.transform.SetParent(Parent);
        this.transform.position = Parent.position;
        if (pointerEvent.pointerCurrentRaycast.isValid)
        {
            InventorySlot slot = pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
            if (slot != null ? slot.Empty : false)
            {
                inventory.Move(this.gameObject, SlotId, slot.Id);
                transform.SetParent(pointerEvent.pointerCurrentRaycast.gameObject.transform);
                transform.position = transform.parent.position;
                SlotId = slot.Id;
                Parent = transform.parent;
            }
        }
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        hoverText.Hovering(this.gameObject);
            if (Time.realtimeSinceStartup <= firstClick + 1)
            {
                Debug.Log("dc");
            }else
            {
                firstClick = Time.realtimeSinceStartup;
            }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.Hovering(this.gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.StopHovering();
    }
    public void UseItem()
    {
        Debug.Log("Using item" + item.name);
        item.Use(inventory.player);
        //amount.text = Item.Amount.ToString();
        if (invItem.amount < 1)
        {
            used?.Invoke();
        }
    }
    public void NewItem(InventoryItem Invitem, int slot)
    {
        invItem = Invitem;
            //Invitem.item;
        if (item != null ? item.sprite != null : false)
        {
            image.sprite = item.sprite;
        }
    }
    public delegate void Used();
    public static event Used used;
}
