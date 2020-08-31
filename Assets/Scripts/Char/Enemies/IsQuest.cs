using UnityEngine;

[System.Serializable]
public class IsQuest
{
    [SerializeField] private bool isQuest = false;
    [SerializeField] private Quests quest = Quests.Bandit;

    public void CheckQuest()
    {
        if (isQuest)
        {
            QuestsSystem.ProgressQuests(quest);
        }
    }
}