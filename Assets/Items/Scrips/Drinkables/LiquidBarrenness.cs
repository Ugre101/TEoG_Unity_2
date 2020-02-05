using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "LiquidBarrenness", menuName = "Item/Drinkable/LiquidBarrenness")]
    public class LiquidBarrenness : Drinkable
    {
        public LiquidBarrenness() : base(ItemId.LiquidBarrenness, "Liquid barrenness")
        {
        }

        public override string Use(BasicChar user)
        {
            CharStats fert = user.PregnancySystem.Fertility;
            fert.BaseValue -= 1;
            return $" {fert.Value}";
        }
    }
}