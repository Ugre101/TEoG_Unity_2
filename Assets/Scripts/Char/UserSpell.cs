[System.Serializable]
public class Skill
{
    public Skill(SkillId parId)
    {
        id = parId;
    }

    [UnityEngine.SerializeField]
    private SkillId id;

    public SkillId Id => id;
}

[System.Serializable]
public class UserSkill
{
    public UserSkill(BasicSkill basicSkill) => skill = basicSkill;

    public BasicSkill skill;

    public int TurnsLeft { get; private set; } = 0;

    public float CoolDownPercent => skill.CoolDown != 0 ? TurnsLeft / (float)skill.CoolDown : 1;

    public bool Ready => skill.HasCoolDown ? TurnsLeft < 1 : true;

    public void StartCoolDown() => TurnsLeft = skill.CoolDown;

    public void RefreshCoolDown(int n = 1) => TurnsLeft -= n;

    public void ResetCoolDown() => TurnsLeft = 0;
}