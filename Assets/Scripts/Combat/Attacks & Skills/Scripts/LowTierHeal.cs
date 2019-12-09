using UnityEngine;

[CreateAssetMenu(fileName = "LowTierHeal", menuName = "ScriptableObject/CombatSkills/LowTierHeal")]
public class LowTierHeal : BasicSkill
{
    public override string Action(ThePrey user, ThePrey target)
    {
        float toHeal = (BaseAttack + user.Stats.Int) * RNG;
        user.HP.Gain(toHeal);
        return $"{user.firstName} heals themself for {toHeal}hp.";
    }
}