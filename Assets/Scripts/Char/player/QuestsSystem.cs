using System.Collections.Generic;
using UnityEngine;

public enum Quests
{
    Bandit,
    ElfsHunt
}

public static class QuestsSystem
{
    private static List<BasicQuest> basicQuest = new List<BasicQuest>();
    private static List<CountQuest> countQuests = new List<CountQuest>();
    private static List<TieredQuest> tieredQuests = new List<TieredQuest>();
    public static List<BasicQuest> BasicQuests => basicQuest;

    public static BasicQuest GetBasicQuest(Quests quests) => BasicQuests.Find(q => q.Type == quests);

    public static List<CountQuest> CountQuests => countQuests;

    public static CountQuest GetCountQuest(Quests quests) => CountQuests.Find(q => q.Type == quests);

    public static List<TieredQuest> TieredQuests => tieredQuests;

    public static TieredQuest GetTieredQuest(Quests quests) => TieredQuests.Find(q => q.Type == quests);

    public static bool HasQuest(Quests parQuest) => basicQuest.Exists(q => q.Type == parQuest)
        || countQuests.Exists(q => q.Type == parQuest)
        || tieredQuests.Exists(q => q.Type == parQuest);

    public static void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!HasQuest(Quests.Bandit))
                {
                    basicQuest.Add(new BanditQuest());
                    PlayerFlags.BanditMap.Know = true;
                }
                break;

            case Quests.ElfsHunt:
                if (!HasQuest(Quests.ElfsHunt))
                {
                    tieredQuests.Add(new ElfQuest());
                }
                break;

            default:
                break;
        }
        GotQuestEvent?.Invoke();
    }

    public static QuestSave Save => new QuestSave(BasicQuests, CountQuests, TieredQuests);

    public static void Load(QuestSave toLoad)
    {
        basicQuest = toLoad.BasicQuests;
        countQuests = toLoad.CountQuests;
        tieredQuests = toLoad.TieredQuests;
        GotQuestEvent?.Invoke();
    }

    public delegate void GotQuest();

    public static event GotQuest GotQuestEvent;

    public static void WinBattleCheck(BasicChar basicChar)
    {
        if (HasQuest(Quests.ElfsHunt))
        {
            if (basicChar.RaceSystem.CurrentRace() == Races.Elf)
            {
                GetTieredQuest(Quests.ElfsHunt).Count++;
            }
        }
    }
}

[System.Serializable]
public struct QuestSave
{
    [SerializeField] private List<BasicQuest> basicQuests;
    [SerializeField] private List<CountQuest> countQuests;
    [SerializeField] private List<TieredQuest> tieredQuests;

    public QuestSave(List<BasicQuest> basicQuests, List<CountQuest> countQuests, List<TieredQuest> tieredQuests)
    {
        this.basicQuests = basicQuests;
        this.countQuests = countQuests;
        this.tieredQuests = tieredQuests;
    }

    public List<BasicQuest> BasicQuests => basicQuests;
    public List<CountQuest> CountQuests => countQuests;
    public List<TieredQuest> TieredQuests => tieredQuests;
}

public static class QuestDesc
{
    public static string GetDesc(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                return "The bandit gang up north are getting bolder every day, and it's affecting our trade. We would in your debt if you could teach them a lesson.";

            case Quests.ElfsHunt:
                return "The elfs down south,";

            default: return string.Empty;
        }
    }

    public static string QuestReturnTo(Quests quests)
    {
        switch (quests)
        {
            case Quests.Bandit: return "Townhall";
            case Quests.ElfsHunt: return "Townhall";
            default: return string.Empty;
        }
    }
}