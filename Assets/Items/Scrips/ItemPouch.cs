using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pouch", menuName = "Item/Pouch")]
public class ItemPouch : Item
{
    public ItemPouch()
    {
        itemId = ItemId.Pouch;
        type = ItemTypes.Misc;
        title = "Pouch";
    }
    public override void Use()
    {
        base.Use();
    }
}
