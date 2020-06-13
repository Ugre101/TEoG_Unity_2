using UnityEngine;

public enum SkillType
{
    Physical,
    Magical,
    Seduction
}

namespace SkillsAndSpells
{
    [CreateAssetMenu(fileName = "testSkill", menuName = BasicSkillExtension.MenuName + "testSkill")]
    public abstract class BasicSkill : ScriptableObject
    {
        #region Variables

        // Private serialized so values can't be changed during runtime by accident.
        [SerializeField] private string title = "";

        public string Title => title;

        [SerializeField] private Sprite icon = null;

        public Sprite Icon => icon;

        [SerializeField] private int baseValue = 10;

        public int BaseValue => baseValue;

        [SerializeField] private SkillId id = SkillId.BasicAttack;

        public SkillId Id => id;

        [SerializeField] private SkillType type = SkillType.Magical;

        public SkillType Type => type;
        [SerializeField] private SkillUses skillUses = new SkillUses();
        public SkillUses SkillUses => skillUses;

        [Header("Cooldown")]
        [SerializeField] private bool hasCoolDown = false;

        public bool HasCoolDown => hasCoolDown;

        [SerializeField] private int coolDown = 0;

        public int CoolDown => coolDown;

        [Tooltip("0 means no rng, RNG works by value * random.range(1, 1 + rng). So 1 rng means 2dmg can become 2 or 4. Making it so baseAttack is lowest value possible.")]
        [Range(0, 10)]
        [SerializeField] private float rng = 1;

        protected float RNG => Random.Range(1, 1 + rng);

        protected abstract float Value(BasicChar user);

        public abstract string HoverDesc(BasicChar user);

        protected float ValueWithRng(BasicChar user) => Mathf.FloorToInt(Value(user) * RNG);

        public float AvgValue(BasicChar user) => Mathf.FloorToInt((Value(user) + (Value(user) * (1 + rng))) / 2);

        #endregion Variables

        public abstract string Action(BasicChar user, BasicChar target);
    }

    public static class BasicSkillExtension
    {
        public const string MenuName = "ScriptableObject/CombatSkills/";
    }

    [System.Serializable]
    public class SkillUses
    {
        [SerializeField] private bool battle = true, sex = false, other = false;
        public bool Battle => battle;
        public bool Sex => sex;
        public bool Other => other;
    }
}