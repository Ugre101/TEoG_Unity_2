using UnityEngine;

[CreateAssetMenu(fileName = "BasicHit", menuName = "ScriptableObject/CombatSkills/BasicHit")]
public class BasicHit : BasicSkill
{
    public override string Action(BasicChar user, BasicChar target)
    {
        return base.Action(user, target);
    }

    public override string Text(BasicChar user, BasicChar target)
    {
        return base.Text(user, target);
    }
}