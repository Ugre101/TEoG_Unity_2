using UnityEngine;

[CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
public class BasicHit : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float dmg = BaseAttack * (user.Stats.Str / 10) * RNG;
        target.HP.TakeDmg(dmg);
        return $"{user.Identity.FirstName} dealt {dmg}dmg to {target.Identity.FirstName}'s health.";
    }

    public override string Text(BasicChar user, BasicChar target)
    {
        return base.Text(user, target);
    }
}