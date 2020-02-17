using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss : EnemyPrefab
{
    // talk before fight?
    [SerializeField] private bool hasPreBattleDialog = false;

    public bool PreBattleDialog => hasPreBattleDialog;

    // talk after fight?
    [SerializeField] private bool hasPostBattleDialog = false;

    public bool PostBattleDialog => hasPreBattleDialog;
    [SerializeField] private bool hasCustomScene = false;
    public bool CustomScene => hasCustomScene;
}

[System.Serializable]
public class HasCustomScenes
{
    [SerializeField] private bool haveCustomScenes = false;
    public bool HaveCustomScenes => haveCustomScenes;
    [SerializeField] private bool blockNormalScenes = false;
    public bool BlockNormalScenes => blockNormalScenes;
    [SerializeField] private List<SexScenes> customScenes = new List<SexScenes>();
    public List<SexScenes> CustomScenes => customScenes;
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