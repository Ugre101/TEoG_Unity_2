using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTease", menuName = "ScriptableObject/CombatSkills/BasicTease")]
public class BasicTease : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float dmg = BaseAttack * (user.Stats.Charm / 10) * RNG;
        target.WP.TakeDmg(dmg);
        return $"{user.firstName} teases {target.firstName}, causing {target.firstName} to lose {dmg} willpower.";
    }
}
