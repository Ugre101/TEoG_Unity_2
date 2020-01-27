using UnityEngine;

[CreateAssetMenu(fileName = "SkillBook", menuName = "ScriptableObject/CombatSkills/SkillBook")]
public class SkillBook : ScriptableObject
{
    [SerializeField] private SkillDict dict = new SkillDict();

    public SkillDict Dict => dict;
}