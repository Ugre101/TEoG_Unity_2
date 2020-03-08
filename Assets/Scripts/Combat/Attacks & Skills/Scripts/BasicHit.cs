using UnityEngine;

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
    public class BasicHit : BasicSkill
    {
        public override string Action(BasicChar user, BasicChar target)
        {
            float dmg = BaseValue * (user.Stats.Str / 10) * RNG;
            target.HP.TakeDmg(dmg);
            return $"{user.Identity.FirstName} dealt {dmg}dmg to {target.Identity.FirstName}'s health.";
        }
    }
}