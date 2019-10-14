using UnityEngine;

public enum Skills
{
    BasicAttack,
    BasicTease
}

public enum SkillType
{
    Physical,
    Magical,
    Seduction
}
[CreateAssetMenu(fileName = "testSkill", menuName = "ScriptableObject/CombatSkills/testSkill")]
public class BasicSkill : ScriptableObject
{
    #region Variables
    // Private serialized so values can't be changed during runtime by accident.
    [SerializeField]
    private string title;

    public string Title => title;

    [SerializeField]
    private int baseAttack;

    public int BaseAttack => baseAttack;

    [SerializeField]
    private Skills skill;

    public Skills Skill => skill;

    [SerializeField]
    private SkillType type;

    public SkillType Type => type;
    #endregion
    public virtual float Attack(BasicChar user)
    {
        float dmg = baseAttack; // + user something
        return dmg;
    }

    public virtual string Text(BasicChar user, BasicChar target)
    {
        string text = $"{user.firstName} attacks {target.firstName}.";
        return text;
    }
}