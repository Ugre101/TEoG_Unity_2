using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private BasicChar wearer = null;
    [SerializeField] private EquipSlot slot;
    [SerializeField] private ItemHolder itemHolder = null;
    [SerializeField] private Image icon = null;
    private Item item = null;

    private void OnEnable()
    {
        wearer = wearer != null ? wearer : PlayerMain.GetPlayer;
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
        Debug.Log(equiptItem.HasItem);
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
}