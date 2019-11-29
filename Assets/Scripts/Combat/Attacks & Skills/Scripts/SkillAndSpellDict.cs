using System.Collections.Generic;
using UnityEngine;

public enum SkillId
{
    BasicAttack,
    BasicTease,
    LowTierHeal,
    LowTierResolve
}

[System.Serializable]
public class SkillDict
{
    //  [SerializeField]
    //  private List<UserSkill> skills = new List<UserSkill>();

    //  public List<UserSkill> Skills => skills;
    [SerializeField]
    private List<BasicSkill> skills = new List<BasicSkill>();

    public List<BasicSkill> Skills => skills;

    public UserSkill Match(SkillId parId)
    {
        if (skills.Exists(s => s.Id == parId))
        {
            // Error
        }
        return new UserSkill(Skills.Find(s => s.Id == parId));
    }

    public List<UserSkill> OwnedSkills(List<Skill> skills)
    {
        List<UserSkill> toReturn = new List<UserSkill>();

        foreach (BasicSkill basicSkill in Skills)
        {
            foreach (Skill skill in skills)
            {
                if (basicSkill.Id == skill.Id)
                {
                    toReturn.Add(new UserSkill(basicSkill));
                }
            }
        }
        return toReturn;
    }
}