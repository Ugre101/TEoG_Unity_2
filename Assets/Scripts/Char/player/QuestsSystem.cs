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
    private List<BasicQuest> quests = new List<BasicQuest>();

    public List<BasicQuest> List => quests;

    public bool HasQuest(Quests parQuest) => quests.Exists(q => q.Type == parQuest);

    public BasicQuest GetQuest(Quests parQuest) => List.Find(q => q.Type == parQuest);

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