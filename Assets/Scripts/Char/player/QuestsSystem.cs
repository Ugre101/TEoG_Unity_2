using System.Collections.Generic;
using UnityEngine;

public enum Quests
{
    Bandit,
    ElfsHunt
}

public static class QuestsSystem
{
    public static List<BasicQuest> BasicQuests { get; private set; } = new List<BasicQuest>();

    public static BasicQuest GetBasicQuest(Quests quests) => BasicQuests.Find(q => q.Type == quests);

    public static List<CountQuest> CountQuests { get; private set; } = new List<CountQuest>();

    public static CountQuest GetCountQuest(Quests quests) => CountQuests.Find(q => q.Type == quests);

    public static List<TieredQuest> TieredQuests { get; private set; } = new List<TieredQuest>();

    public static TieredQuest GetTieredQuest(Quests quests) => TieredQuests.Find(q => q.Type == quests);

    public static bool HasQuest(Quests parQuest) => BasicQuests.Exists(q => q.Type == parQuest)
        || CountQuests.Exists(q => q.Type == parQuest)
        || TieredQuests.Exists(q => q.Type == parQuest);

    public static bool QuestIsCompleted(Quests quests)
    {
        if (HasQuest(quests))
        {
            if (BasicQuests.Exists(q => q.Type == quests))
            {
                return GetBasicQuest(quests).Completed;
            }
            else if (CountQuests.Exists(q => q.Type == quests))
            {
                return GetCountQuest(quests).Completed;
            }
            else if (TieredQuests.Exists(q => q.Type == quests))
            {
                return GetTieredQuest(quests).Completed;
            }
        }
        return false;
    }

    public static void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!HasQuest(Quests.Bandit))
                {
                    BasicQuests.Add(new BanditQuest());
                    PlayerFlags.BanditMap.Know = true;
                }
                break;

            case Quests.ElfsHunt:
                if (!HasQuest(Quests.ElfsHunt))
                {
                    TieredQuests.Add(new ElfQuest());
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
        BasicQuests = toLoad.BasicQuests;
        CountQuests = toLoad.CountQuests;
        TieredQuests = toLoad.TieredQuests;
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

    public static void ProgressQuests(Quests quest, int count = 1)
    {
        switch (quest)
        {
            case Quests.Bandit:
                if (HasQuest(quest))
                {
                    GetBasicQuest(quest).SetCompleted();
                }
                break;

            case Quests.ElfsHunt:
                if (HasQuest(quest))
                {
                    GetTieredQuest(quest).Count += count;
                }
                break;
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

public static class QuestReward
{
    public static string GetReward(Quests quests)
    {
        switch (quests)
        {
            case Quests.Bandit:
                return BanditLordReward(PlayerMain.GetPlayer);

            case Quests.ElfsHunt:
                return ElfHuntReward(PlayerMain.GetPlayer);

            default:
                return "";
        }
    }

    private static string ElfHuntReward(PlayerMain player)
    {
        TieredQuest quest = QuestsSystem.GetTieredQuest(Quests.ElfsHunt);
        float tierMulti = Mathf.Pow(2, quest.Tier - 1);
        int expGain = Mathf.FloorToInt(100 * tierMulti);
        int goldGain = Mathf.FloorToInt(150 * tierMulti);
        player.ExpSystem.GainExp(expGain);
        player.Currency.Gold += goldGain;
        QuestsSystem.TieredQuests.Remove(quest);
        return $"You are rewarded: {expGain}Exp and {goldGain}gold";
    }

    private static string BanditLordReward(PlayerMain player)
    {
        BasicQuest quest = QuestsSystem.GetBasicQuest(Quests.Bandit);
        player.ExpSystem.GainExp(300);
        player.Currency.Gold += 500;
        QuestsSystem.BasicQuests.Remove(quest);
        if (PlayerFlags.BeatBanditLord.Cleared)
        {
            return "You are rewared: 300Exp and 500gold";
        }
        else
        {
            PlayerFlags.BeatBanditLord.Clear();
            return $"We can not thank you enough, as an token of our gratitude we have transfered you the rights of the propery around your home. \n\nYou are rewared: 300Exp and 500gold";
        }
    }
}