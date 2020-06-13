using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "PocketPortal", menuName = "Item/Misc/PocketPortal")]
    public class PocketPortal : Misc
    {
        public PocketPortal() : base(ItemIds.PocketPortal, "Pocket portal")
        {
            desc = "One time use pocket teleport, use it to your home from almost anywhere.";
        }

        // use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.
        public override string Use(BasicChar user)
        {
            HomeCanvas.GetHomeCanvas.EnterHome();
            return base.Use(user);
        }
    }
}