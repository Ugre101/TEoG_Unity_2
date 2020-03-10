using UnityEngine;

namespace ItemScripts
{
    /// <summary> Template for items, replace all intances of template with prefered name.
    /// Also remember to add the item's id to Items.cs in "public enum ItemId". </summary>
    [CreateAssetMenu(fileName = "MaleContraceptive", menuName = "Item/Edibles/MaleContraceptive")]
    public class MaleContraceptive : Edibles
    {
        private readonly TempStatMod contraMod = new TempStatMod(-10, ModTypes.Flat, "MaleContraceptive", 168);

        public MaleContraceptive() : base(ItemIds.MaleContraceptive, "Male contraceptive")
        {
        }

        public override string Use(BasicChar user)
        {
            CharStats viri = user.PregnancySystem.Virility;
            viri.AddTempMod(contraMod);
            return $" {viri.MaxValue}";
        }
    }
}