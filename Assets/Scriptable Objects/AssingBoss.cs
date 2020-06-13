using UnityEngine;
[CreateAssetMenu(fileName = "Assing Boss", menuName = "Enemy/Assing Boss")]

public class AssingBoss : AssingEnemy
{
    [SerializeField] private bool hasPreBattleDialog = false, hasPostBattleDialog = false;
    [SerializeField] private HasCustomScenes hasCustomScene = new HasCustomScenes();
    [SerializeField] private bool lockedPosistion = false;
    [SerializeField] private Vector3 pos = new Vector3();

    public override BasicChar Setup(BasicChar basicChar)
    {
        Body body = new Body(FinalHeight, FinalFat, FinalMuscle);
        BasicChar newChar = new Boss(hasPreBattleDialog,hasPostBattleDialog,hasCustomScene,lockedPosistion,pos,reward, isQuest, canTakeToDorm, orgsNeeded, new Age(age), body, new ExpSystem());
        newChar.Stats.SetBaseValues(FinalStat(assingStr), FinalStat(assingCharm), FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt), assingWill);
        startRaces.ForEach(r => newChar.RaceSystem.AddRace(r.Races, r.Amount));
        startGender.Assing(newChar);
        if (NeedFirstName)
        {
            if (newChar.GenderType == GenderTypes.Masculine)
            {
                newChar.Identity.FirstName = RandomName.MaleName;
            }
            else
            {
                newChar.Identity.FirstName = RandomName.FemaleName;
            }
        }
        else
        {
            newChar.Identity.FirstName = firstName;
        }
        if (NeedLastName)
        {
            newChar.Identity.LastName = RandomName.LastName;
        }
        else
        {
            newChar.Identity.LastName = lastName;
        }
        return newChar;
    }
}