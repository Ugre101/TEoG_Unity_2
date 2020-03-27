using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "BasicTease", menuName = "ScriptableObject/CombatSkills/BasicTease")]
    public class BasicTease : BasicSkill
    {
        public override string Action(BasicChar user, BasicChar target)
        {
            float dmg = ValueWithRng(user);
            target.WP.TakeDmg(dmg);
            return $"{user.Identity.FirstName} teases {target.Identity.FirstName}, causing {target.Identity.FirstName} to lose {dmg} willpower.";
        }

        public override string HoverDesc(BasicChar user) => $"{Title}\n{Type}\n{ValueWithRng(user)}";

        protected override float Value(BasicChar user) => BaseValue * (user.Stats.Cha / 10);
    }
}