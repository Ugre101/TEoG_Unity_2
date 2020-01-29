using UnityEngine;

/// <summary>
/// Template for items, replace all intances of template with prefered name.
/// Also remember to add the item's id to Items.cs in "public enum ItemId".
/// </summary>
[CreateAssetMenu(fileName = "Template", menuName = "Item/SmallRation")]
public class ItemSmallRation : Edibles
{
    public ItemSmallRation() : base(ItemId.Potion, "Small ration")
    {
    }

    // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}