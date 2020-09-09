using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "Minor temp shrink", menuName = BasicSkillExtension.MenuName + "MinorTempShrink")]
    public class MinorTempShrink : BasicSkill
    {
        private static TempStatMod ShrinkMod => new TempStatMod(-0.1f, ModTypes.Precent, nameof(MinorTempShrink), 8);

        public override string Action(BasicChar user, BasicChar target)
        {
            target.Body.Height.AddTempMod(ShrinkMod);
            return $"You shrunk {target.Identity.FirstName} they are now {target.Body.HeightMorInch()} tall.";
        }

        public override string HoverDesc(BasicChar user) => $"{Title}\n{Type}\nShrink target by one tenth, doesn't stack in effect.";

        protected override float Value(BasicChar user) => 0;
    }
}