using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyPrefab
{
    // talk before fight?
    [SerializeField] private bool hasPreBattleDialog = false;

    public bool PreBattleDialog => hasPreBattleDialog;

    // talk after fight?
    [SerializeField] private bool hasPostBattleDialog = false;

    public bool PostBattleDialog => hasPostBattleDialog;
    [SerializeField] private HasCustomScenes hasCustomScene = new HasCustomScenes();
    public HasCustomScenes CustomScenes => hasCustomScene;
    [SerializeField] private bool lockedPosistion = false;
    public bool LockedPosistion => lockedPosistion;
    [SerializeField] private Vector3 pos = new Vector3();

    public Boss(bool hasPreBattleDialog, bool hasPostBattleDialog, HasCustomScenes hasCustomScene, bool lockedPosistion, Vector3 pos, Reward reward, IsQuest isQuest, bool canTakeToDorm, int orgsNeeded, Age age, Body body, ExpSystem expSystem)
        : base(reward, isQuest, canTakeToDorm, orgsNeeded, age, body, expSystem)
    {
        this.hasPreBattleDialog = hasPreBattleDialog;
        this.hasPostBattleDialog = hasPostBattleDialog;
        this.hasCustomScene = hasCustomScene;
        this.lockedPosistion = lockedPosistion;
        this.pos = pos;
    }

    public Vector3 Pos => pos;
}

[System.Serializable]
public class HasCustomScenes
{
    [SerializeField] private bool haveCustomScenes = false, blockNormalScenes = false;
    [SerializeField] private List<SexScenes> customScenes = new List<SexScenes>();
    public bool HaveCustomScenes => haveCustomScenes;
    public bool BlockNormalScenes => blockNormalScenes;
    public List<SexScenes> CustomScenes => customScenes;
}
