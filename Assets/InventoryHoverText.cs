using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class InventoryHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverblock;
    public TextMeshProUGUI hovertext;
    private InventorySlot[] slots;
    private Item item;
    private DragInventory[] drag;
    public void OnEnable()
    {
        slots = this.gameObject.GetComponentsInChildren<InventorySlot>();
        drag = this.gameObject.GetComponentsInChildren<DragInventory>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventorySlot hoverd = eventData.pointerEnter.GetComponent<InventorySlot>();
        if (hoverd != null ? !hoverd.Empty : false)
        {
            hoverblock.SetActive(true);
            RectTransform rt = (RectTransform)eventData.pointerEnter.transform;
            Vector3 vector3 = eventData.pointerEnter.transform.localPosition;
            vector3.x += rt.rect.width;
          //  Debug.Log(vector3);
          
            hoverblock.transform.localPosition = vector3;
            Item item = eventData.pointerEnter.transform.gameObject.GetComponentInChildren<DragInventory>().item;
            hovertext.text = item.Title;
        }else
        {
            hoverblock.SetActive(false);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       hoverblock.SetActive(false);
    }
}
