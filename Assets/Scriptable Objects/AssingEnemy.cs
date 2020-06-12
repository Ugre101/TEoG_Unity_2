using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assing Enemy", menuName = "Enemy/Assing Enemy")]
public class AssingEnemy : ScriptableObject
{
    [SerializeField] protected string firstName = string.Empty, lastName = string.Empty;
    [SerializeField] protected bool NeedFirstName = true, NeedLastName = true;
    [SerializeField] protected int age = 18;
    [SerializeField] protected List<StartRace> startRaces = new List<StartRace>();

    [SerializeField] protected StartGender startGender = new StartGender();

    #region Stats

    [SerializeField] protected int assingStr = 0, assingCharm = 0, assingEnd = 0, assingDex = 0, assingInt = 0, assingWill = 0;

    [SerializeField] protected float statRngFactor = 0.4f;

    public int FinalStat(int v) => Mathf.FloorToInt(v * Random.Range(1 - statRngFactor, 1 + statRngFactor));

    #endregion Stats

    #region Body stats

    [SerializeField] protected int assingHeight = 160, assingFat = 20, assingMuscle = 30;

    [SerializeField] protected float heightRng = 0.1f, fatRng = 0.1f, muscleRng = 0.1f;

    protected int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    protected int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    protected int FinalMuscle => Mathf.FloorToInt(assingMuscle * Random.Range(1 - muscleRng, 1 + muscleRng));

    #endregion Body stats

    [SerializeField] protected IsQuest isQuest = new IsQuest();

    [SerializeField] protected Reward reward = new Reward();

    [Header("Dorm related")]
    [SerializeField] protected bool canTakeToDorm = true;

    [SerializeField] protected int orgsNeeded = 3;

    public virtual BasicChar Setup(BasicChar basicChar)
    {
        Body body = new Body(FinalHeight, FinalFat, FinalMuscle);
        BasicChar newChar = new EnemyPrefab(reward, isQuest, canTakeToDorm, orgsNeeded, new Age(age), body, new ExpSystem());
        basicChar.Stats.SetBaseValues(FinalStat(assingStr), FinalStat(assingCharm), FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt), assingWill);
        startRaces.ForEach(r => basicChar.RaceSystem.AddRace(r.Races, r.Amount));
        startGender.Assing(basicChar);

        if (NeedFirstName)
        {
            if (basicChar.GenderType == GenderTypes.Masculine)
            {
                basicChar.Identity.FirstName = RandomName.MaleName;
            }
            else
            {
                basicChar.Identity.FirstName = RandomName.FemaleName;
            }
        }
        else
        {
            basicChar.Identity.FirstName = firstName;
        }
        if (NeedLastName)
        {
            basicChar.Identity.LastName = RandomName.LastName;
        }
        else
        {
            basicChar.Identity.LastName = lastName;
        }
        return newChar;
    }
}