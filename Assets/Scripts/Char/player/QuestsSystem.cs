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

    public List<Quest> List => quests;

    public bool HasQuest(Quests parQuest) => quests.Exists(q => q.Type == parQuest);

    public Quest GetQuest(Quests parQuest) => List.Find(q => q.Type == parQuest);

    public void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!HasQuest(Quests.Bandit))
                {
                    quests.Add(new BanditQuest());
                }
                break;

            case Quests.Elfs:
                if (!HasQuest(Quests.Elfs))
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
    [SerializeField]
    protected Quests type;

    public Quests Type => type;

    [SerializeField]
    private int count = 0;

    public int Count => count;

    /// <summary> Call when succesfuly completing quest condition, returns a bool if you completed quest or not. </summary>
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

    protected int goalCount = 0;

    // TODO get rid of desc from quest itself I don't want to save desc.
    protected string desc;

    public string Desc => desc;

    [SerializeField]
    private bool completed = false;

    public bool Completed => completed;

    [SerializeField]
    protected bool hasTiers = false;

    public bool HasTiers => hasTiers;

    [SerializeField]
    private int tier = 0;

    [SerializeField]
    protected int tierStep;

    public int Tier { get { tier = Mathf.FloorToInt(Count / tierStep); return tier; } }

    [SerializeField]
    protected string title;

    public string Title => title;
}

public class BanditQuest : Quest
{
    public BanditQuest()
    {
        title = "Bandit lord";
        goalCount = 1;
        desc = new QuestDesc(Quests.Bandit).Desc;
        type = Quests.Bandit;
    }
}

public class ElfQuest : Quest
{
    public ElfQuest()
    {
        title = "Elf hunt";
        goalCount = 3;
        desc = new QuestDesc(Quests.Elfs).Desc;
        hasTiers = true;
        tierStep = 3;
        type = Quests.Elfs;
    }
}

public class QuestDesc
{
    private string desc;
    public string Desc { get { return desc; } }

    public QuestDesc(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                desc = "Bandit";
                break;

            case Quests.Elfs:
                desc = "Elf hunt";
                break;
        }
    }
}