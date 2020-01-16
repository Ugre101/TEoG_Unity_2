using UnityEngine;

[System.Serializable]
public class BasicQuest
{
    [SerializeField] private Quests type;

    [SerializeField] protected bool completed = false;

    public BasicQuest(Quests parQuest) => type = parQuest;

    public Quests Type => type;
    public virtual bool Completed => completed;

    public void SetCompleted() => completed = true;
}

[System.Serializable]
public class CountQuest : BasicQuest
{
    [SerializeField] private int count = 0;

    public int CountGoal { get; private set; }

    public CountQuest(Quests parQuest, int parCountGoal) : base(parQuest)
    {
        CountGoal = parCountGoal;
    }

    public int Count
    {
        get => count;
        set
        {
            count = value;
            if
                (count >= CountGoal)
            {
                SetCompleted();
            }
        }
    }
}

[System.Serializable]
public class TieredQuest : CountQuest
{
    public TieredQuest(Quests parQuest, int parCountGoal, int parTierStep) : base(parQuest, parCountGoal)
    {
        tierStep = parTierStep;
    }

    [SerializeField] private int tierStep;
    public int TierStep => tierStep;
    public int Tier => Mathf.FloorToInt(Count / TierStep);
}

public class BanditQuest : BasicQuest
{
    public BanditQuest() : base(Quests.Bandit)
    {
    }
}

public class ElfQuest : TieredQuest
{
    public ElfQuest() : base(Quests.ElfsHunt, 3, 3)
    {
    }
}