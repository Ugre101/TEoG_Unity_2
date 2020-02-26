using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryItem invItem;

    [SerializeField] private TextMeshProUGUI amountText = null;

    public Item Item { get; private set; }
    private int SlotId;
    private PlayerMain Player => PlayerMain.GetPlayer;

    private InventoryHandler invHandler;
    private InventoryHoverText hoverText;
    private Transform Parent;
    private Image GetImage => GetComponent<Image>();

    private bool CanvasBlockRaycast
    {
        set => GetComponent<CanvasGroup>().blocksRaycasts = value;
    }

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
        CanvasBlockRaycast = false;
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
                invHandler.Move(SlotId, slot.Id);
                transform.SetParent(pointerEvent.pointerCurrentRaycast.gameObject.transform);
                transform.position = transform.parent.position;
                SlotId = slot.Id;
                Parent = transform.parent;
                UsedEvent?.Invoke();
            }else if (pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<EquipmentSlot>() is EquipmentSlot equipSlot)
            {
                equipSlot.DragItem(Item);
            }
            else
            {
                Debug.Log("Do you want to remove");
                invHandler.Move(SlotId);
            }
        }
        CanvasBlockRaycast = true;
    }

    private void Hovering() => hoverText.Hovering(Item.Title, Item.Desc);

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        Hovering();
        if (Time.realtimeSinceStartup <= firstClick + 1)
        {
            UseItem();
        }
        else
        {
            firstClick = Time.realtimeSinceStartup;
        }
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
        if (isHovering && !hoverTextActive)
        {
            if (timeStartedHovering + 0.8f < Time.realtimeSinceStartup)
            {
                Hovering();
                hoverTextActive = true;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTextActive = false;
        hoverText.StopHovering();
    }

    public void UseItem()
    {
        Debug.Log("Using item" + Item.name);
        if (Item is IEquip toEquip)
        {
            Debug.Log("Equipable!");
            Player.AutoEquipItem(Item);
        }
        else if (Item is IHaveStatMods haveMods)
        {
            haveMods.Mods.ForEach(m => Player.Stats.GetStat(m.StatTypes).AddMods(m.StatMod));
            Debug.Log("Has statmod!");
            // TODO if player has weapong equipt then dequip it.
        }
        Item.Use(Player);
        //amount.text = Item.Amount.ToString();
        if (!invItem.Reusable)
        {
            Amount--;
        }
        if (Amount < 1)
        {
            UsedEvent?.Invoke();
            hoverText.StopHovering();
        }
    }

    public void NewItem(InventoryHandler inventoryHandler, InventoryItem inventoryItem, Item item, InventoryHoverText inventoryHoverText)
    {
        this.hoverText = inventoryHoverText;
        invItem = inventoryItem;
        invHandler = inventoryHandler;
        SlotId = inventoryItem.InvPos;
        this.Item = item;
        _ = Amount;
        //Invitem.item;
        if (item != null ? item.Sprite != null : false)
        {
            GetImage.sprite = item.Sprite;
        }
    }

    public delegate void Used();

    public static event Used UsedEvent;
}