using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GiveQuests
{
    [SerializeField] private List<QuestButton> questButtons = new List<QuestButton>();
    [SerializeField] private TakeQuest questPanelPrefab = null;
    public List<QuestButton> QuestButtons => questButtons;
    public TakeQuest QuestPanelPrefab => questPanelPrefab;

    public void AlreadyHasQuest()
        => questButtons.ForEach(qg => qg.Btn.gameObject.SetActive(!QuestsSystem.HasQuest(qg.Quest)));
}

[System.Serializable]
public class GiveQuestRewards
{
    [SerializeField] private List<QuestRewardButton> rewardButtons = new List<QuestRewardButton>();
    public List<QuestRewardButton> RewardButtons => rewardButtons;

    public List<QuestRewardButton> CompletedQuests() => RewardButtons.FindAll(q => QuestsSystem.QuestIsCompleted(q.Quest));

    // TODO dialog prefab
}