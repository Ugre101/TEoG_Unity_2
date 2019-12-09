using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTease", menuName = "ScriptableObject/CombatSkills/BasicTease")]
public class BasicTease : BasicSkill
{
    public override string Action(ThePrey user, ThePrey target)
    {
        float dmg = BaseAttack * (user.Stats.Cha / 10) * RNG;
        target.WP.TakeDmg(dmg);
        return $"{user.firstName} teases {target.firstName}, causing {target.firstName} to lose {dmg} willpower.";
    }
}
