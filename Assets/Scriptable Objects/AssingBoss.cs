using UnityEngine;

[CreateAssetMenu(fileName = "Assing Boss", menuName = "Enemy/Assing Boss")]
public class AssingBoss : AssingEnemy
{
    [SerializeField] private bool hasPreBattleDialog = false, hasPostBattleDialog = false;
    [SerializeField] private HasCustomScenes hasCustomScene = new HasCustomScenes();
    [SerializeField] private bool lockedPosistion = false;
    [SerializeField] private Vector3 pos = new Vector3();

    public bool LockedPosistion => lockedPosistion;
    public Vector3 Pos => pos;
    public HasCustomScenes HasCustomScene => hasCustomScene;
    public bool HasPreBattleDialog => hasPreBattleDialog;
    public bool HasPostBattleDialog => hasPostBattleDialog;

    public override BasicChar Setup(BasicChar basicChar)
    {
        Body body = new Body(FinalHeight, FinalFat, FinalMuscle);
        BasicChar newChar = new Boss(HasPreBattleDialog, HasPostBattleDialog, HasCustomScene, LockedPosistion, Pos, reward, isQuest, canTakeToDorm, orgsNeeded, new Age(age), body, new ExpSystem());
        newChar.Stats.SetBaseValues(FinalStat(assingStr), FinalStat(assingCharm), FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt), assingWill);
        startRaces.ForEach(r => newChar.RaceSystem.AddRace(r.Races, r.Amount));
        startGender.Assing(newChar);
        if (NeedFirstName)
            newChar.Identity.SetFirstName(newChar.GenderType == GenderTypes.Masculine ? RandomName.MaleName : RandomName.FemaleName);
        else
            newChar.Identity.SetFirstName(firstName);

        newChar.Identity.SetLastName(NeedLastName ? RandomName.LastName : lastName);
        return newChar;
    }
}