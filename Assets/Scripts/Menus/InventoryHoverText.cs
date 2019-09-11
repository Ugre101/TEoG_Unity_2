using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class InventoryHoverText : MonoBehaviour
{
    public GameObject hoverblock;
    public TextMeshProUGUI hovertext;
    public float xDistance = 1f;
    public float yDistance = 1f;
    private InventorySlot[] slots;
    private Item item;
    private DragInventory[] drag;
    private RectTransform Parent;
    private RectTransform hoverRect;
    public void OnEnable()
    {
        slots = this.gameObject.GetComponentsInChildren<InventorySlot>();
        drag = this.gameObject.GetComponentsInChildren<DragInventory>();
        Parent = (RectTransform)this.gameObject.transform;
        hoverRect = (RectTransform)hoverblock.transform;
    }
    public void Hovering(GameObject hoverOver)
    {
        hoverblock.SetActive(true);
        GameObject InventorySlot = hoverOver.transform.parent.gameObject;
        RectTransform rt = (RectTransform)InventorySlot.transform;
        Vector3 vector3 = rt.position;
        if (rt.position.x < Parent.rect.width / 2)
        {
            vector3.x += xDistance;
        }
        else
        {
            vector3.x -= xDistance;
        }
        if (rt.position.y < Parent.rect.height / 2)
        {
            vector3.y += yDistance;
        }else
        {
            vector3.y -= yDistance;
        }
        hoverRect.position = vector3;
        Item item = hoverOver.GetComponentInChildren<DragInventory>().item;
        hovertext.text = item.Title;
    }
    public void StopHovering()
    {
        hoverblock.SetActive(false);
    }
}
