using UnityEngine;

[CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
public class BasicHit : BasicSkill
{
    public override string Action(ThePrey user, ThePrey target)
    {
        float dmg = BaseAttack * (user.Stats.Str / 10) * RNG;
        target.HP.TakeDmg(dmg);
        return $"{user.firstName} dealt {dmg}dmg to {target.firstName}'s health.";
    }

    public override string Text(ThePrey user, ThePrey target)
    {
        return base.Text(user, target);
    }
}