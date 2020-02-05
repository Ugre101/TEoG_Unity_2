using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "SeedDiluter", menuName = "Item/Drinkable/SeedDiluter")]
    public class SeedDiluter : Drinkable
    {
        public SeedDiluter() : base(ItemId.SeedDiluter, "Seed diluter")
        {
        }

        public override string Use(BasicChar user)
        {
            CharStats viri = user.PregnancySystem.Virility;
            viri.BaseValue -= 1;
            return $" {viri.Value}";
        }
    }
}