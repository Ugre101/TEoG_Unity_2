using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : CharHolder
{
    [HideInInspector]
    [SerializeField] protected bool NeedFirstName = true, NeedLastName = true;

    [HideInInspector]
    [SerializeField] protected List<StartRace> startRaces = new List<StartRace>();

    [HideInInspector]
    [SerializeField] protected StartGender startGender = new StartGender();

    #region Stats

    [HideInInspector]
    [SerializeField] protected int assingStr = 0, assingCharm = 0, assingEnd = 0, assingDex = 0, assingInt = 0, assingWill = 0;

    [HideInInspector]
    [SerializeField] protected float statRngFactor = 0.4f;

    public int FinalStat(int v) => Mathf.FloorToInt(v * Random.Range(1 - statRngFactor, 1 + statRngFactor));

    #endregion Stats

    #region Body stats

    [HideInInspector]
    [SerializeField] protected int assingHeight = 160, assingFat = 20, assingMuscle = 30;

    [HideInInspector]
    [SerializeField] protected float heightRng = 0.1f, fatRng = 0.1f, muscleRng = 0.1f;

    protected int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    protected int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    protected int FinalMuscle => Mathf.FloorToInt(assingMuscle * Random.Range(1 - muscleRng, 1 + muscleRng));

    #endregion Body stats

    [HideInInspector]
    [SerializeField] protected IsQuest isQuest = new IsQuest();

    [HideInInspector]
    [SerializeField] protected Reward reward = new Reward();

    [Header("Dorm related")]
    [SerializeField] protected bool canTakeToDorm = true;

    [SerializeField] protected int orgsNeeded = 3;

    public override BasicChar BasicChar { get; protected set; } = new BasicChar();

    private void Awake()
    {
        Setup();
    }

    public override void Setup()
    {
        Body body = new Body(FinalHeight, FinalFat, FinalMuscle);
        BasicChar = new EnemyPrefab(reward, isQuest, canTakeToDorm, orgsNeeded, new Age(), body, new ExpSystem(), new EssenceSystem(new Essence(), new Essence(), new CharStats()));
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