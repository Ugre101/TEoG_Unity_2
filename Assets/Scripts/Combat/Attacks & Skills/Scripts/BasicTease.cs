using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTease", menuName = "ScriptableObject/CombatSkills/BasicTease")]
public class BasicTease : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        float dmg = BaseAttack * (user.Stats.Cha / 10) * RNG;
        target.WP.TakeDmg(dmg);
        return $"{user.Identity.FirstName} teases {target.Identity.FirstName}, causing {target.Identity.FirstName} to lose {dmg} willpower.";
    }
}
