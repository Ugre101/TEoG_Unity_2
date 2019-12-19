using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Template for items, replace all intances of template with prefered name.
/// Also remember to add the item's id to Items.cs in "public enum ItemId".
/// </summary>
[CreateAssetMenu(fileName = "Template", menuName = "Item/Template")]
public class ItemTemplate : Item
{
    public ItemTemplate()
    {
        ItemId = ItemId.Potion;
        Type = ItemTypes.Misc;
        Title = "Template";
        UseName = "Use";
        Desc = "template for items, desc itself is where you say what the item does. This item happens to do nothing.";
    }
    // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }

}
