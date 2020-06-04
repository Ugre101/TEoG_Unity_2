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

    public override void Setup()
    {
        Body body = new Body(FinalHeight, FinalFat, FinalMuscle);
        BasicChar = new Boss(hasPreBattleDialog, hasPostBattleDialog, CustomScenes, LockedPosistion, pos, reward, isQuest, canTakeToDorm, orgsNeeded, new Age(), body, new ExpSystem(), new EssenceSystem(new Essence(), new Essence(), new CharStats()));
        BasicChar.Stats.SetBaseValues(FinalStat(assingStr), FinalStat(assingCharm), FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt), assingWill);
        startRaces.ForEach(r => BasicChar.RaceSystem.AddRace(r.Races, r.Amount));
        startGender.Assing(BasicChar);
        if (NeedFirstName)
        {
            if (BasicChar.GenderType == GenderTypes.Masculine)
            {
                BasicChar.Identity.FirstName = RandomName.MaleName;
            }
            else
            {
                BasicChar.Identity.FirstName = RandomName.FemaleName;
            }
        }
        if (NeedLastName)
        {
            BasicChar.Identity.LastName = RandomName.LastName;
        }
        BasicChar.Setup();
    }
}