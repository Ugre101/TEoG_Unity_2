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
    [Range(0f,0.5f)]
    public float xDistance = 0.123f;
    [Range(0f,0.5f)]
    public float yDistance = 0.123f;
    private InventorySlot[] slots;
    private Item item;
    private DragInventory[] drag;
    private RectTransform Parent;
    private RectTransform hoverRect;
    public void OnEnable()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        drag = GetComponentsInChildren<DragInventory>();
        Parent = (RectTransform)transform;
        hoverRect = (RectTransform)hoverblock.transform;
    }
    public void Hovering(GameObject hoverOver)
    {
        hoverblock.SetActive(true);
        GameObject InventorySlot = hoverOver.transform.parent.gameObject;
        RectTransform rt = (RectTransform)InventorySlot.transform;
        Vector3 vector3 = rt.position;
        if (rt.position.x < Screen.width / 2)
        {
            vector3.x += Screen.width * xDistance; // xDistance;
        }
        else
        {
            vector3.x -= Screen.width * xDistance;
        }
        if (rt.position.y < Screen.height / 2)
        {
            vector3.y += Screen.height * yDistance;
        }
        else
        {
            vector3.y -= Screen.height * yDistance;
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
