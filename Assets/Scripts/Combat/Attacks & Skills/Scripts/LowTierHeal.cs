using UnityEngine;

[CreateAssetMenu(fileName = "LowTierHeal", menuName = "ScriptableObject/CombatSkills/LowTierHeal")]
public class LowTierHeal : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float toHeal = (BaseAttack + user.Int) * RNG;
        user.HP.Gain(toHeal);
        return $"{user.firstName} heals themself for {toHeal}hp.";
    }
}