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
    public void OnEnable()
    {
        slots = this.gameObject.GetComponentsInChildren<InventorySlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (eventData.pointerEnter.transform.parent == this.transform)
        {
            hoverblock.SetActive(true);
            RectTransform rt = (RectTransform)eventData.pointerEnter.transform;
            Vector3 vector3 = eventData.pointerEnter.transform.localPosition;
            vector3.x += rt.rect.width;
            Debug.Log(vector3);
          
            hoverblock.transform.localPosition = vector3;
            Item item = eventData.pointerEnter.transform.gameObject.GetComponent<inventoryItem>().Item;
            hovertext.text = item.Title;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //hoverblock.SetActive(false);
    }
}
