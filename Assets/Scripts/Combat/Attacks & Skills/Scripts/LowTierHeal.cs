using UnityEngine;

[CreateAssetMenu(fileName = "LowTierHeal", menuName = "ScriptableObject/CombatSkills/LowTierHeal")]
public class LowTierHeal : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float toHeal = (BaseAttack + user.Int) * RNG;
        target.HP.Gain(toHeal);
        Debug.Log(user == target);
        return base.Action(user, target);
    }
}