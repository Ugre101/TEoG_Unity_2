using UnityEngine;

[System.Serializable]
public class Boss : EnemyPrefab
{
    // talk before fight?
    [SerializeField] private bool hasPreBattleDialog = false;

    // talk after fight?
    [SerializeField] private bool hasPostBattleDialog = false;

    [SerializeField] private bool hasCustomScene = false;
}

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