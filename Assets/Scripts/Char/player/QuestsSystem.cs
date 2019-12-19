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

public static class QuestDesc
{
    public static string GetDesc(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                return "Bandit";

            case Quests.Elfs:
                return "Elf hunt";
            default:
                return string.Empty;
        }
    }
}