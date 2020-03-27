using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
    public class BasicHit : BasicSkill
    {
        public override string Action(BasicChar user, BasicChar target)
        {
            float dmg = ValueWithRng(user);
            target.HP.TakeDmg(dmg);
            return $"{user.Identity.FirstName} dealt {dmg}dmg to {target.Identity.FirstName}'s health.";
        }

        public override string HoverDesc(BasicChar user) => $"{Title}\n{Type}\n{ValueWithRng(user)}";

        protected override float Value(BasicChar user) => BaseValue * (user.Stats.Str / 10);
    }
}