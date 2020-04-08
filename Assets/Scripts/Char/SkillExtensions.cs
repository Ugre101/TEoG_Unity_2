using System.Collections.Generic;
using System.Linq;

public static class SkillExtensions
{
    public static List<Skill> CombatSkills(this List<Skill> skills, SkillDict dict) => skills.Where(s => dict.Match(s.Id).skill.SkillUses.Battle).Select(s => s).ToList();

    public static List<Skill> SexSkills(this List<Skill> skills, SkillDict dict) => skills.Where(s => dict.Match(s.Id).skill.SkillUses.Sex).Select(s => s).ToList();

    public static List<Skill> OtherSkills(this List<Skill> skills, SkillDict dict) => skills.Where(s => dict.Match(s.Id).skill.SkillUses.Other).Select(s => s).ToList();
}