using UnityEngine;

public class InventoryHoverText : BasicMenuHoverText
{
    public void OnEnable() => StopHovering();

    public override void Hovering(GameObject hoverOver, Vector2 mousePos)
    {
        base.Hovering(hoverOver, mousePos);
        Item item = hoverOver.GetComponentInChildren<DragInventory>().item;
        if (item != null)
        {
            string desc = $"{item.Title}\n\n{item.Desc}";
            hovertext.text = desc;
        }
        else
        {
            hovertext.text = $"error I guess?";
        }
    }
}