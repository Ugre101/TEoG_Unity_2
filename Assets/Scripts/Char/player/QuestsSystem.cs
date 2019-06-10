using System.Collections.Generic;
using UnityEngine;

public enum Quests
{
    Bandit,
    Elfs
}

[System.Serializable]
public class QuestsSystem
{
    [SerializeField]
    private List<Quest> quests = new List<Quest>();
    public List<Quest> Quest { get { return quests; } }
    public void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!quests.Exists(q => q.type == Quests.Bandit))
                {
                    quests.Add(new BanditQuest());
                }
                break;
            case Quests.Elfs:
                if (!quests.Exists(q => q.type == Quests.Elfs))
                {
                    quests.Add(new ElfQuest());
                }
                break;
        }
    }
}

[System.Serializable]
public class Quest
{
    public Quests type;
    private int count = 0;

    public int Count { get { return count; } }

    /// <summary>
    /// Call when succesfuly completing quest condition, returns a bool if you completed quest or not.
    /// </summary>
    /// <param name="toStep"></param>
    /// <returns></returns>
    public bool DidQuest(int toStep)
    {
        count += toStep;
        if (goalCount <= count)
        {
            completed = true;
        }
        return completed;
    }

    public int goalCount = 0;
    public string desc;
    private bool completed = false;
    public bool Completed { get { return completed; } }
}

public class TierQuest : Quest
{
    private int tier = 0;
    public int tierStep;
    public int Tier { get { tier = Mathf.FloorToInt(Count / tierStep); return tier; } }
}

public class BanditQuest : Quest
{
    public BanditQuest()
    {
        goalCount = 1;
        desc = "Beat banditlord";
        type = Quests.Bandit;
    }
}

public class ElfQuest : TierQuest
{
    public ElfQuest()
    {
        goalCount = 3;
        desc = "Beat elfs";
        tierStep = 3;
        type = Quests.Elfs;
    }
}