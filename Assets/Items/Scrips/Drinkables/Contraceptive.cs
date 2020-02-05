using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "Contraceptive", menuName = "Item/Edibles/Contraceptive")]
    public class Contraceptive : Edibles
    {
        private readonly TempStatMod contraMod = new TempStatMod(10, ModTypes.Flat, "Contraceptive", 168);

        public Contraceptive() : base(ItemId.Contraceptive, "Contraceptive")
        {
        }

        public override string Use(BasicChar user)
        {
            CharStats fert = user.PregnancySystem.Fertility;
            fert.AddTempMod(contraMod);
            return $" {fert.Value}";
        }
    }
}