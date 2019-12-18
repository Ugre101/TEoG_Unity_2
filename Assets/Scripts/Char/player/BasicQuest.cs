using UnityEngine;

[System.Serializable]
public class BasicQuest
{
    [SerializeField]
    protected Quests type;

    [SerializeField]
    protected bool completed = false;

    [SerializeField]
    protected int count = 0;

    public BasicQuest(Quests parQuest) => type = parQuest;

    public Quests Type => type;
    public virtual bool Completed => completed;
    public virtual int Count { get => count; set => count = value; }
    public virtual int Tier { get; set; }
    public virtual int TierStep { get; set; }

    public void SetCompleted() => completed = true;
}

[System.Serializable]
public abstract class CountQuest : BasicQuest
{
    public int CountGoal { get; private set; }

    public CountQuest(Quests parQuest, int parCountGoal) : base(parQuest)
    {
        CountGoal = parCountGoal;
    }

    public override int Count
    {
        get => count;
        set
        {
            count = value;
            if (count >= CountGoal) { SetCompleted(); }
        }
    }
}

[System.Serializable]
public abstract class TieredQuest : CountQuest
{
    public TieredQuest(Quests parQuest, int parCountGoal, int parTierStep) : base(parQuest, parCountGoal)
    {
        tierStep = parTierStep;
    }

    private readonly int tierStep;
    public override int TierStep => tierStep;
    public override int Tier => Mathf.FloorToInt(Count / TierStep);
}

public class BanditQuest : BasicQuest
{
    public BanditQuest() : base(Quests.Bandit)
    {
    }
}

public class ElfQuest : TieredQuest
{
    public ElfQuest() : base(Quests.Elfs, 3, 3)
    {
    }
}