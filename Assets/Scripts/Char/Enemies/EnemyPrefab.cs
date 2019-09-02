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
    [Tooltip("Multiplied by 0.8f to 1.2f")]
    [Header("Stats")]
    [SerializeField]
    [Range(0, 100)]
    private int assingStr = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingCharm = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingEnd = 0;
    [SerializeField]
    [Range(0, 100)]
    private int assingDex = 0;
    [Tooltip("Multiplied by 0.9f to 1.1f")]
    [Header("Body")]
    [SerializeField]
    [Range(0, 300)]
    private int assingHeight;
    [SerializeField]
    [Range(0, 200)]
    private int assingWeight;
    [SerializeField]
    [Range(0, 100)]
    private int assingFat;
    [SerializeField]
    [Range(0, 100)]
    private int assingMuscle;
    public override void Start()
    {
        strength._baseValue = Mathf.FloorToInt(assingStr * Random.Range(0.8f, 1.2f));
        charm._baseValue = Mathf.FloorToInt(assingCharm * Random.Range(0.8f, 1.2f));
        endurance._baseValue = Mathf.FloorToInt(assingEnd * Random.Range(0.8f, 1.2f));
        dexterity._baseValue = Mathf.FloorToInt(assingDex * Random.Range(0.8f, 1.2f));
        init(1, 100f, 100f);
        Femi.Gain(200f);
        Masc.Gain(300f);
        Body = new Body(Mathf.FloorToInt(assingHeight * Random.Range(0.9f,1.1f)), Mathf.FloorToInt(assingWeight * Random.Range(0.9f,1.1f)), 
            Mathf.FloorToInt(assingFat * Random.Range(0.9f,1.1f)), Mathf.FloorToInt(assingMuscle * Random.Range(0.9f,1.1f)));
        raceSystem.AddRace(assingRace.GetRace(),100);
        if (NeedFirstName)
        {
           if (GenderType == GenderType.Masculine)
            {
                firstName = randomName.MaleName;
            }else
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