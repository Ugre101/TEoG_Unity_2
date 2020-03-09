using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestRewardButton
{
    [SerializeField] private Button btn = null;
    [SerializeField] private Quests quest = Quests.Bandit;

    [Header("Reward has dialog / options")]
    [SerializeField] private bool rewardDialog = false;

    public Button Btn => btn;
    public Quests Quest => quest;
    public bool RewardDialog => rewardDialog;
}