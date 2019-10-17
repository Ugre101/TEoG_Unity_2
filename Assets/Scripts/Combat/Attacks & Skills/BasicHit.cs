using UnityEngine;

[CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
public class BasicHit : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float dmg = BaseAttack * (user.Str / 10) * RNG;
        target.HP.TakeDmg(dmg);
        return $"{user.firstName} dealt {dmg}dmg to {target.firstName}'s health.";
    }

    public override string Text(BasicChar user, BasicChar target)
    {
        return base.Text(user, target);
    }
}