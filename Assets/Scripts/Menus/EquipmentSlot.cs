using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BasicChar wearer = null;
    [SerializeField] private EquipSlot slot = EquipSlot.Boots;
    [SerializeField] private ItemHolder itemHolder = null;
    [SerializeField] private Image icon = null;
    [SerializeField] private Button btn = null;
    private Item item = null;

    private void OnEnable()
    {
        wearer = wearer != null ? wearer : PlayerMain.Player;
        btn = btn != null ? btn : GetComponent<Button>();
        wearer.EquiptItems.GetSlot(slot).GotItem += HasItem;
        HasItem();
    }

    private void OnDisable() => wearer.EquiptItems.GetSlot(slot).GotItem -= HasItem;

    private void OnDestroy() => wearer.EquiptItems.GetSlot(slot).GotItem -= HasItem; // Probably not relevant

    public void DragItem(Item item)
    {
        wearer.ManualEquipItem(item, slot);
        HasItem();
    }

    private void HasItem()
    {
        EquiptItem equiptItem = wearer.EquiptItems.GetSlot(slot);
        if (equiptItem.HasItem)
        {
            item = itemHolder.GetById(equiptItem.Item);
            icon.gameObject.SetActive(true);
            icon.sprite = item.Sprite;
        }
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }
    }

    private void DeEquip() => wearer.DeEquipItem(slot);

    private float lastClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.unscaledTime < lastClick + 1)
        {
            DeEquip();
        }
        else
        {
            lastClick = Time.unscaledTime;
        }
    }
}