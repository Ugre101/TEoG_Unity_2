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
    private string title = "";

    public string Title => title;

    [SerializeField]
    private Sprite icon = null;

    public Sprite Icon => icon;

    [SerializeField]
    private int baseAttack = 10;

    public int BaseAttack => baseAttack;

    [SerializeField]
    private Skills skill = Skills.BasicAttack;

    public Skills Skill => skill;

    [SerializeField]
    private SkillType type = SkillType.Magical;

    public SkillType Type => type;

    [Header("Cooldown")]
    [SerializeField]
    private bool hasCoolDown = false;

    public bool HasCoolDown => hasCoolDown;

    [SerializeField]
    private int coolDown = 0;

    public int CoolDown => coolDown;    

    [Tooltip("0 means no rng, RNG works by value * random.range(1, 1 + rng). So 1 rng means 2dmg can become 2 or 4. Making it so baseAttack is lowest value possible.")]
    [Range(0, 10)]
    [SerializeField]
    private float rng = 1;

    protected float RNG => Random.Range(1, 1 + rng);

    public float AvgValue => (baseAttack + (baseAttack * (1 + rng))) / 2;

    #endregion Variables

    public virtual string Action(BasicChar user, BasicChar target)
    {
        float dmg = baseAttack * RNG; // + user something
        return $"{user.firstName} action {target.firstName} for {dmg}.";
    }

    public virtual string Text(BasicChar user, BasicChar target)
    {
        string text = $"{user.firstName} attacks {target.firstName}.";
        return text;
    }
}