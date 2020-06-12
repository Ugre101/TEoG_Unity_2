using UnityEngine;

public class BossHolder : EnemyHolder
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
    public override BasicChar BasicChar { get; protected set; } = new BasicChar();

    public void Setup(AssingBoss assingBoss) => BasicChar = assingBoss.Setup(BasicChar);
}