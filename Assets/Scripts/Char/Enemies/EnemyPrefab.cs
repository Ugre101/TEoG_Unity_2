using UnityEngine;

[System.Serializable]
public class EnemyPrefab : BasicChar
{
    public bool NeedFirstName = true;
    public bool NeedLastName = true;
    public RandomName randomName;
    public AssingRace assingRace;

    [Tooltip("Chosen values get mulitled by random range 0.5f to 1.5f")]
    public Reward reward;

    #region Stats

    [Tooltip("Base value for enemy stats")]
    [Header("Stats")]
    [Range(0, 100)]
    [SerializeField]
    private int assingStr = 0;

    [Tooltip("Base value for enemy stats")]
    [Range(0, 100)]
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

    private float heightRng = 0.1f;
    private int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    [Range(0, 100)]
    public int assingFat = 20;

    private float fatRng = 0.1f;
    private int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    [Range(0, 100)]
    public int assingMuscle = 30;

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
        stats = new StatsContainer(FinalStat(assingStr), FinalStat(assingCharm),
            FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt));
        Init(1, 100f, 100f);
        Femi.Gain(200f);
        Masc.Gain(300f);
        body = new Body(FinalHeight, FinalFat, FinalMuscle);
        RaceSystem.AddRace(assingRace.GetRace(), 100);
        if (NeedFirstName)
        {
            if (GenderType == GenderType.Masculine)
            {
                firstName = randomName.MaleName;
            }
            else
            {
                firstName = randomName.FemaleName;
            }
        }
        if (NeedLastName)
        {
            lastName = randomName.LastName;
        }
        base.Start();
    }
}