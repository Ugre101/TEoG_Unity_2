using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestButton
{
    [SerializeField] private Button btn = null;
    [SerializeField] private Quests quest = Quests.Bandit;

    public Button Btn => btn;
    public Quests Quest => quest;
}
