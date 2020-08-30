[System.Serializable]
public class EnemyPrefab : BasicChar
{
    public Reward Reward { get; } = new Reward();
    public IsQuest IsQuest { get; } = new IsQuest();

    private bool canTakeToDorm = true;

    private int orgsNeeded = 3;

    public EnemyPrefab(Reward reward, IsQuest isQuest, bool canTakeToDorm, int orgsNeeded, Age age, Body body, ExpSystem expSystem) : base(age, body, expSystem)
    {
        this.Reward = reward;
        this.IsQuest = isQuest;
        this.canTakeToDorm = canTakeToDorm;
        this.orgsNeeded = orgsNeeded;
    }

    public bool CanTake => canTakeToDorm ? SexStats.SessionOrgasm >= orgsNeeded : false;
}