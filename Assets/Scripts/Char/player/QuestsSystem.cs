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

    public List<Quest> List { get { return quests; } }

    public void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!quests.Exists(q => q.Type == Quests.Bandit))
                {
                    quests.Add(new BanditQuest());
                }
                break;

            case Quests.Elfs:
                if (!quests.Exists(q => q.Type == Quests.Elfs))
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

    public Quests Type { get { return type; } }

    [SerializeField]
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

    protected int goalCount = 0;

    // do I need to add serizefield to this or should I make a other solution? feels stupid to save general quest data and not just progress.
    protected string desc;

    public string Desc { get { return desc; } }

    [SerializeField]
    private bool completed = false;

    public bool Completed { get { return completed; } }

    [SerializeField]
    protected bool hasTiers = false;

    public bool HasTiers { get { return hasTiers; } }

    [SerializeField]
    private int tier = 0;

    [SerializeField]
    protected int tierStep;

    public int Tier { get { tier = Mathf.FloorToInt(Count / tierStep); return tier; } }
    protected string title;
    public string Title { get { return title; } }
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