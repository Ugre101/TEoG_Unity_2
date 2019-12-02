using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public InventoryItem invItem;

    public Item item;
    public int SlotId;
    public PlayerMain player;

    private InventoryHandler invHandler;
    private InventoryHoverText hoverText;
    private Transform Parent;
    private Image GetImage => GetComponent<Image>();

    private int Amount
    {
        get => invItem != null ? player.Inventory.Items.Find(i => i.invPos == invItem.invPos).amount : 0;
        set => player.Inventory.Items.Find(i => i.invPos == invItem.invPos).amount = value;
    }

    private float firstClick;

    private void Awake()
    {
        hoverText = GetComponentInParent<InventoryHoverText>();
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
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(Parent.parent);
    }

    public void OnDrag(PointerEventData pointerEvent) => transform.position = pointerEvent.position;

    public void OnEndDrag(PointerEventData pointerEvent)
    {
        transform.SetParent(Parent);
        transform.position = Parent.position;
        if (pointerEvent.pointerCurrentRaycast.isValid)
        {
            InventorySlot slot = pointerEvent.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
            if (slot != null ? slot.Empty : false)
            {
                invHandler.Move(SlotId, slot.Id);
                transform.SetParent(pointerEvent.pointerCurrentRaycast.gameObject.transform);
                transform.position = transform.parent.position;
                SlotId = slot.Id;
                Parent = transform.parent;
                UsedEvent?.Invoke();
            }
            else
            {
                Debug.Log("Do you want to remove");
                invHandler.Move(SlotId);
            }
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        hoverText.Hovering(gameObject, pointerEvent.position);
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
                hoverText.Hovering(gameObject, Input.mousePosition);
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
        Debug.Log("Using item" + item.name);
        if (item.Type == ItemTypes.Weapon)
        {
            Weapon weapon = (Weapon)item;
            foreach (StatMods mod in weapon.Mods)
            {
                player.Stats.GetStat(mod.StatType).AddMods(mod);
            }
            // TODO if player has weapong equipt then dequip it.
        }
        item.Use(player);
        //amount.text = Item.Amount.ToString();
        Amount--;
        if (Amount < 1)
        {
            UsedEvent?.Invoke();
            hoverText.StopHovering();
        }
    }

    public void NewItem(InventoryHandler parHandler, InventoryItem parInvitem, int slot)
    {
        invItem = parInvitem;
        invHandler = parHandler;
        player = invHandler.player;
        SlotId = slot;
        //Invitem.item;
        if (item != null ? item.Sprite != null : false)
        {
            GetImage.sprite = item.Sprite;
        }
    }

    public delegate void Used();

    public static event Used UsedEvent;
}