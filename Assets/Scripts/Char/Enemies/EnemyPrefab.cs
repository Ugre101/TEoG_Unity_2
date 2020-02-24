﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab : BasicChar
{
    #region Assing stuff

    [HideInInspector]
    [SerializeField] private bool NeedFirstName = true, NeedLastName = true;

    [HideInInspector]
    [SerializeField] private List<StartRace> startRaces = new List<StartRace>();

    [HideInInspector]
    [SerializeField] private StartGender startGender = new StartGender();

    [HideInInspector]
    [SerializeField] private Reward reward = new Reward();

    [HideInInspector]
    [SerializeField] private IsQuest isQuest = new IsQuest();

    #endregion Assing stuff

    public Reward Reward => reward;
    public IsQuest IsQuest => isQuest;

    #region Stats

    [HideInInspector]
    [SerializeField] private int assingStr = 0, assingCharm = 0, assingEnd = 0, assingDex = 0, assingInt = 0, assingWill;

    [HideInInspector]
    [SerializeField] private float statRngFactor = 0.4f;

    public int FinalStat(int v) => Mathf.FloorToInt(v * Random.Range(1 - statRngFactor, 1 + statRngFactor));

    #endregion Stats

    #region Body stats

    [HideInInspector]
    [SerializeField] private int assingHeight = 160, assingFat = 20, assingMuscle = 30;

    [HideInInspector]
    [SerializeField] private float heightRng = 0.1f, fatRng = 0.1f, muscleRng = 0.1f;

    private int FinalHeight => Mathf.FloorToInt(assingHeight * Random.Range(1 - heightRng, 1 + heightRng));

    private int FinalFat => Mathf.RoundToInt(assingFat * Random.Range(1 - fatRng, 1 + fatRng));

    private int FinalMuscle => Mathf.FloorToInt(assingMuscle * Random.Range(1 - muscleRng, 1 + muscleRng));

    #endregion Body stats

    [Header("Dorm related")]
    [SerializeField] private bool canTakeToDorm = true;

    [SerializeField] private int orgsNeeded = 3;

    public bool CanTake => canTakeToDorm ? SexStats.SessionOrgasm >= orgsNeeded : false;

    public override void Start()
    {
        base.Start();
        stats = new StatsContainer(FinalStat(assingStr), FinalStat(assingCharm),
            FinalStat(assingDex), FinalStat(assingEnd), FinalStat(assingInt), assingWill);
        body = new Body(FinalHeight, FinalFat, FinalMuscle);
        startRaces.ForEach(r => RaceSystem.AddRace(r.Races, r.Amount));
        startGender.Assing(this);
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
        InitHealth();
    }

    public override void Update() => base.Update();
}