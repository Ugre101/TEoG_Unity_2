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
    [Range(0, 100)]
    public int assingStr = 0;

    [Range(0, 100)]
    public int assingCharm = 0;

    [Range(0, 100)]
    public int assingEnd = 0;

    [Range(0, 100)]
    public int assingDex = 0;

    [Tooltip("Multiplied by 0.9f to 1.1f")]
    [Header("Body")]
    [Range(0, 300)]
    public int assingHeight = 160;

    [Range(0, 200)]
    public int assingWeight = 60;

    [Range(0, 100)]
    public int assingFat = 20;

    [Range(0, 100)]
    public int assingMuscle = 30;

    public override void Start()
    {
        strength._baseValue = Mathf.FloorToInt(assingStr * Random.Range(0.8f, 1.2f));
        charm._baseValue = Mathf.FloorToInt(assingCharm * Random.Range(0.8f, 1.2f));
        endurance._baseValue = Mathf.FloorToInt(assingEnd * Random.Range(0.8f, 1.2f));
        dexterity._baseValue = Mathf.FloorToInt(assingDex * Random.Range(0.8f, 1.2f));
        Init(1, 100f, 100f);
        Femi.Gain(200f);
        Masc.Gain(300f);
        Body = new Body(Mathf.FloorToInt(assingHeight * Random.Range(0.9f, 1.1f)), Mathf.FloorToInt(assingWeight * Random.Range(0.9f, 1.1f)),
            Mathf.FloorToInt(assingFat * Random.Range(0.9f, 1.1f)), Mathf.FloorToInt(assingMuscle * Random.Range(0.9f, 1.1f)));
        raceSystem.AddRace(assingRace.GetRace(), 100);
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