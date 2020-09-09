using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryItem invItem;

    [SerializeField] private TextMeshProUGUI amountText = null;

    private Item Item { get; set; }
    private int slotId;
    private static BasicChar Player => PlayerMain.Player;

    private InventoryHandler invHandler;
    private InventoryHoverText hoverText;
    private Transform Parent;
    private Image GetImage => GetComponent<Image>();

    private void SetCanvasBlockRaycast(bool value) => GetComponent<CanvasGroup>().blocksRaycasts = value;

    private int Amount
    {
        get
        {
            int amount = invItem.Amount;
            amountText.text = amount.ToString();
            return amount;
        }

        set
        {
            amountText.text = value.ToString();
            invItem.Amount = value;
        }
    }

    private float firstClick;

    private void OnEnable()
    {
        Parent = transform.parent;
        transform.position = Parent.position;
    }

    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        hoverText.StopHovering();
        SetCanvasBlockRaycast(false);
        transform.SetParent(Parent.parent);
    }

    public void OnDrag(PointerEventData pointerEvent) => transform.position = pointerEvent.position;

    public void OnEndDrag(PointerEventData pointerEvent)
    {
        transform.SetParent(Parent);
        transform.position = Parent.position;
        if (pointerEvent.pointerCurrentRaycast.isValid)
        {
            if (pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() is InventorySlot slot && slot.Empty)
            {
                invHandler.Move(slotId, slot.Id);
                transform.SetParent(pointerEvent.pointerCurrentRaycast.gameObject.transform);
                transform.position = transform.parent.position;
                slotId = slot.Id;
                Parent = transform.parent;
                UsedEvent?.Invoke();
            }
            else if (pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<EquipmentSlot>() is EquipmentSlot equipSlot)
            {
                UseItem();
            }
            else
            {
                Debug.Log("Do you want to remove");
                invHandler.Move(slotId);
            }
        }
        SetCanvasBlockRaycast(true);
    }

    private void Hovering() => hoverText.Hovering(Item.Title, Item.FullDesc());

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        Hovering();
        if (Time.realtimeSinceStartup <= firstClick + 1)
            UseItem();
        else
            firstClick = Time.realtimeSinceStartup;
    }

    private bool isHovering = false, hoverTextActive = false;
    private float timeStartedHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        timeStartedHovering = Time.realtimeSinceStartup;
        isHovering = true;
    }

    private void Update()
    {
        if (!isHovering || hoverTextActive || !(timeStartedHovering + 0.8f < Time.realtimeSinceStartup)) return;
        
        Hovering();
        hoverTextActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTextActive = false;
        hoverText.StopHovering();
    }

    private void UseItem()
    {
        switch (Item)
        {
            case IEquip toEquip:
                Player.AutoEquipItem(Item);
                break;
            case IHaveStatMods haveMods:
                haveMods.StatMods.ForEach(m => Player.Stats.GetStat(m.StatTypes).AddMods(m.StatMod));
                break;
        }
        Item.Use(Player);
        //amount.text = Item.Amount.ToString();
        if (!invItem.Reusable) Amount--;

        if (Amount >= 1) return;
        
        UsedEvent?.Invoke();
        hoverText.StopHovering();
    }

    public void NewItem(InventoryHandler inventoryHandler, InventoryItem inventoryItem, Item item, InventoryHoverText inventoryHoverText)
    {
        this.hoverText = inventoryHoverText;
        invItem = inventoryItem;
        invHandler = inventoryHandler;
        slotId = inventoryItem.InvPos;
        this.Item = item;
        _ = Amount;
        //Invitem.item;
        if (item != null && item.Sprite != null)
        {
            GetImage.sprite = item.Sprite;
        }
    }

    public delegate void Used();

    public static event Used UsedEvent;
}