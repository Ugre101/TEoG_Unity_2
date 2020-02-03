using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartRace
{
    [SerializeField] private Races races;
    public Races Races => races;
    [SerializeField] private int amount = 100;
    public int Amount => amount;

    public StartRace()
    {
        amount = 100;
    }
}

[System.Serializable]
public class EnemyPrefab : BasicChar
{
    [HideInInspector]
    [SerializeField] private bool NeedFirstName = true, NeedLastName = true;

    [HideInInspector]
    [SerializeField] private List<StartRace> startRaces = new List<StartRace>();

    [SerializeField] private Reward reward = new Reward();

    public Reward Reward => reward;

    #region Stats

    [HideInInspector]
    [SerializeField] private int assingStr = 0;

    [HideInInspector]
    [SerializeField] private int assingCharm = 0, assingEnd = 0, assingDex = 0, assingInt = 0;

    [HideInInspector]
    [SerializeField] private float statRngFactor = 0.4f;

    public int FinalStat(int v) => Mathf.FloorToInt(v * Random.Range(1 - statRngFactor, 1 + statRngFactor));

    #endregion Stats

    #region Body stats

    [HideInInspector]
    [SerializeField] private int assingHeight = 160;

    [HideInInspector]
    [SerializeField] private float heightRng = 0.1f;

    private int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    [HideInInspector]
    [SerializeField] public int assingFat = 20;

    [HideInInspector]
    [SerializeField] private float fatRng = 0.1f;

    private int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    [HideInInspector]
    [SerializeField] private int assingMuscle = 30;

    [HideInInspector]
    [SerializeField] private float muscleRng = 0.1f;

    private int FinalMuscle => Mathf.FloorToInt(assingMuscle * Random.Range(1 - muscleRng, 1 + muscleRng));

    #endregion Body stats

    [Header("Dorm related")]
    [SerializeField] private bool canTakeToDorm = true;

    [SerializeField] private int orgsNeeded = 3;

    public bool CanTake(int sessOrg) => canTakeToDorm ? sessOrg >= orgsNeeded : false;

    public override void Start()
    {
        base.Start();
        stats = new StatsContainer(FinalStat(assingStr), FinalStat(assingCharm),
            FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt));
        //    Femi.Gain(200f);
        Essence.Masc.Gain(300f);
        body = new Body(FinalHeight, FinalFat, FinalMuscle);
        startRaces.ForEach(r => RaceSystem.AddRace(r.Races, r.Amount));
        if (NeedFirstName)
        {
            if (GenderType == GenderTypes.Masculine)
            {
                Identity.FirstName = RandomName.MaleName;
            }
            else
            {
                Identity.FirstName = RandomName.FemaleName;
            }
        }
        if (NeedLastName)
        {
            Identity.LastName = RandomName.LastName;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}