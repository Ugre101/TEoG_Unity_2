using UnityEngine;

[System.Serializable]
public class EnemyPrefab : BasicChar
{
    [HideInInspector]
    [SerializeField]
    private bool NeedFirstName = true, NeedLastName = true;

    public AssingRace assingRace = new AssingRace();

    [Tooltip("Chosen values get mulitled by random range 0.5f to 1.5f")]
    public Reward reward = new Reward();

    #region Stats

    [HideInInspector]
    [SerializeField]
    private int assingHP = 100, assingWP = 100;

    [HideInInspector]
    [SerializeField]
    private int assingStr = 0;

    [HideInInspector]
    [SerializeField]
    private int assingCharm = 0, assingEnd = 0, assingDex = 0, assingInt = 0;

    [Tooltip("RNG factor; range(1 - rng,1 + rng), so 1 rng can either double the value or make it zero. 0.4f is standard.")]
    [Range(0, 1f)]
    [SerializeField]
    private float statRngFactor = 0.4f;

    public int FinalStat(int v) => Mathf.FloorToInt(v * Random.Range(1 - statRngFactor, 1 + statRngFactor));

    #endregion Stats

    #region Body stats

    [Tooltip("Multiplied by 0.9f to 1.1f")]
    [Header("Body")]
    [Range(0, 300)]
    public int assingHeight = 160;

    [SerializeField]
    private float heightRng = 0.1f;

    private int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    [Range(0, 100)]
    public int assingFat = 20;

    [SerializeField]
    private float fatRng = 0.1f;

    private int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    [Range(0, 100)]
    public int assingMuscle = 30;

    [SerializeField]
    private float muscleRng = 0.1f;

    private int FinalMuscle => Mathf.FloorToInt(assingMuscle * Random.Range(1 - muscleRng, 1 + muscleRng));

    #endregion Body stats

    [Header("Dorm related")]
    [SerializeField]
    private bool canTakeToDorm = true;

    [SerializeField]
    private int orgsNeeded = 3;

    public bool CanTake(int sessOrg) => canTakeToDorm ? sessOrg >= orgsNeeded : false;

    public override void Start()
    {
        base.Start();
        stats = new StatsContainer(FinalStat(assingStr), FinalStat(assingCharm),
            FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt));
        //    Femi.Gain(200f);
        Essence.Masc.Gain(300f);
        body = new Body(FinalHeight, FinalFat, FinalMuscle);
        RaceSystem.AddRace(assingRace.GetRace(), 100);
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