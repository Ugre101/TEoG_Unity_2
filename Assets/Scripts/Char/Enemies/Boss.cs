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
    [SerializeField] private HasCustomScenes hasCustomScene = new HasCustomScenes();
    public HasCustomScenes CustomScenes => hasCustomScene;
    [SerializeField] private bool lockedPosistion = false;
    public bool LockedPosistion => lockedPosistion;
    [SerializeField] private Vector3 pos = new Vector3();
    public Vector3 Pos => pos;
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