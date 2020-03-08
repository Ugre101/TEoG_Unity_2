using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "Minor temp shrink", menuName = BasicSkillExtension.MenuName + "MinorTempShrink")]
    public class MinorTempShrink : BasicSkill
    {
        private TempStatMod ShrinkMod => new TempStatMod(-0.1f, ModTypes.Precent, typeof(MinorTempShrink).Name, 8);

        public override string Action(BasicChar user, BasicChar target)
        {
            target.Body.Height.AddTempMod(ShrinkMod);
            return $"You shrunk {target.Identity.FirstName} they are now {Settings.MorInch(target.Body.Height.Value)} tall.";
        }
    }
}