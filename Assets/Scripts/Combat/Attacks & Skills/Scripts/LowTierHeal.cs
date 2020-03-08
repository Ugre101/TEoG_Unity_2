using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "LowTierHeal", menuName = "ScriptableObject/CombatSkills/LowTierHeal")]
    public class LowTierHeal : BasicSkill
    {
        public override string Action(BasicChar user, BasicChar target)
        {
            float toHeal = (BaseValue + user.Stats.Int) * RNG;
            user.HP.Gain(toHeal);
            return $"{user.Identity.FirstName} heals themself for {toHeal}hp.";
        }
    }
}