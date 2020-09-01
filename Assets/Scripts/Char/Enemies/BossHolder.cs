using UnityEngine;

public class BossHolder : EnemyHolder
{
    // talk before fight?
    [SerializeField] private bool hasPreBattleDialog = false, hasPostBattleDialog = false, lockedPosistion = false;

    public bool PreBattleDialog => hasPreBattleDialog;
    public bool PostBattleDialog => hasPostBattleDialog;
    public bool LockedPosistion => lockedPosistion;

    // talk after fight?

    [SerializeField] private HasCustomScenes hasCustomScene = new HasCustomScenes();
    public HasCustomScenes CustomScenes => hasCustomScene;
    [SerializeField] private Vector3 pos = new Vector3();
    public Vector3 Pos => pos;

    public void SetupBoss(AssingBoss assingBoss)
    {
        BasicChar = assingBoss.Setup(BasicChar);
        Bind();
    }
}