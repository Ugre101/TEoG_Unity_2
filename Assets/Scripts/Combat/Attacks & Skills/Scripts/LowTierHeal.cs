using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "LowTierHeal", menuName = "ScriptableObject/CombatSkills/LowTierHeal")]
    public class LowTierHeal : BasicSkill
    {
        public override string Action(BasicChar user, BasicChar target)
        {
            float toHeal = ValueWithRng(user);
            user.Hp.Gain(toHeal);
            return $"{user.Identity.FirstName} heals themself for {toHeal}hp.";
        }

        public override string HoverDesc(BasicChar user) => $"{Title}\n{Type}\n{ValueWithRng(user)}";

        protected override float Value(BasicChar user) => BaseValue + user.Stats.Int;
    }
}