using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Quests
{
    Bandit,
    ElfsHunt,
    ImpregnateMaidens,
    GetImpregnated, // Children birthed in name of shrine gets raised at shrine not home
    TheExperimentalPotion,
    FairyQueen, // After beating certain amount of faires special area opens
}

public static class QuestsSystem
{
    public static Dictionary<Quests, BasicQuest> BasicQuests { get; private set; } = new Dictionary<Quests, BasicQuest>();

    public static BasicQuest GetBasicQuest(Quests quests) => BasicQuests[quests];

    public static bool TryGetBasicQuest(Quests quests, out BasicQuest basicQuest) => BasicQuests.TryGetValue(quests, out basicQuest); // Not tested

    public static Dictionary<Quests, CountQuest> CountQuests { get; private set; } = new Dictionary<Quests, CountQuest>();

    public static CountQuest GetCountQuest(Quests quests) => CountQuests[quests];

    public static Dictionary<Quests, TieredQuest> TieredQuests { get; private set; } = new Dictionary<Quests, TieredQuest>();

    public static TieredQuest GetTieredQuest(Quests quests) => TieredQuests[quests];

    public static bool HasQuest(Quests parQuest) => BasicQuests.ContainsKey(parQuest)
                                                    || CountQuests.ContainsKey(parQuest)
                                                    || TieredQuests.ContainsKey(parQuest);

    public static bool QuestIsCompleted(Quests quests)
    {
        if (BasicQuests.TryGetValue(quests, out BasicQuest quest))
            return quest.Completed;
        else if (CountQuests.TryGetValue(quests, out CountQuest countQuest))
            return countQuest.Completed;
        else if (TieredQuests.TryGetValue(quests, out TieredQuest tieredQuest))
            return tieredQuest.Completed;
        return false;
    }

    public static void AddQuest(Quests which)
    {
        switch (which)
        {
            case Quests.Bandit:
                if (!HasQuest(Quests.Bandit))
                {
                    BasicQuests.Add(Quests.Bandit, new BanditQuest());
                    PlayerFlags.BanditMap.Know = true;
                }
                break;

            case Quests.ElfsHunt:
                if (!HasQuest(Quests.ElfsHunt))
                {
                    TieredQuests.Add(Quests.ElfsHunt, new ElfQuest());
                }
                break;

            default:
                Debug.LogWarning($"The quest {which} isn't added in switch.");
                break;
        }
        GotQuestEvent?.Invoke();
    }

    public static QuestSave Save => new QuestSave(BasicQuests.Values.ToList(), CountQuests.Values.ToList(), TieredQuests.Values.ToList());

    public static void Load(QuestSave toLoad)
    {
        BasicQuests = toLoad.BasicQuests.ToDictionary(id => id.Type);
        CountQuests = toLoad.CountQuests.ToDictionary(id => id.Type);
        TieredQuests = toLoad.TieredQuests.ToDictionary(id => id.Type);
        GotQuestEvent?.Invoke();
    }

    public delegate void GotQuest();

    public static event GotQuest GotQuestEvent;

    public static void ProgressQuests(Quests quest, int count = 1)
    {
        switch (quest)
        {
            case Quests.Bandit:
                if (HasQuest(quest))
                {
                    GetBasicQuest(quest).SetCompleted();
                    PlayerFlags.TimesBeatenBanditLord.Increase();
                }
                break;

            case Quests.ElfsHunt:
                if (HasQuest(quest))
                    GetTieredQuest(quest).Count += count;
                break;

            default:
                Debug.LogWarning($"{quest.ToString()} isn't handled in progressQuests switch.");
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
                return BanditLordReward(PlayerMain.Player);

            case Quests.ElfsHunt:
                return ElfHuntReward(PlayerMain.Player);

            default:
                return "";
        }
    }

    private static string ElfHuntReward(BasicChar player)
    {
        TieredQuest quest = QuestsSystem.GetTieredQuest(Quests.ElfsHunt);
        float tierMulti = Mathf.Pow(2, quest.Tier - 1);
        int expGain = Mathf.FloorToInt(100 * tierMulti);
        int goldGain = Mathf.FloorToInt(150 * tierMulti);
        player.ExpSystem.GainExp(expGain);
        player.Currency.Gold += goldGain;
        QuestsSystem.TieredQuests.Remove(Quests.ElfsHunt);
        return $"You are rewarded: {expGain}Exp and {goldGain}gold";
    }

    private static string BanditLordReward(BasicChar player)
    {
        player.ExpSystem.GainExp(300);
        player.Currency.Gold += 500;
        QuestsSystem.BasicQuests.Remove(Quests.Bandit);
        PlayerFlags.BeatBanditLord.Clear();
        return PlayerFlags.BeatBanditLord.Cleared
            ? "You are rewared: 300Exp and 500gold"
            : $"We can not thank you enough, as an token of our gratitude we have transfered you the rights of the propery around your home. \n\nYou are rewared: 300Exp and 500gold";
    }
}