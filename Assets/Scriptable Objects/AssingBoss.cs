using UnityEngine;
[CreateAssetMenu(fileName = "Assing Boss", menuName = "Enemy/Assing Boss")]

public class AssingBoss : AssingEnemy
{
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

    public override BasicChar Setup(BasicChar basicChar)
    {
        BasicChar newChar = base.Setup(basicChar);
        return newChar;
    }
}