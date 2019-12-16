using System;
using UnityEngine;

public enum StatTypes
{
    Str,
    Charm,
    End,
    Dex,
    Int
}

[System.Serializable]
public class StatsContainer
{
    public StatsContainer()
    {
        strength = new CharStats();
        charm = new CharStats();
        dexterity = new CharStats();
        endurance = new CharStats();
        intelligence = new CharStats();
    }

    public StatsContainer(int parStr = 10, int parCharm = 10, int parDex = 10, int parEnd = 10, int parInt = 10)
    {
        strength = new CharStats(parStr);
        charm = new CharStats(parCharm);
        dexterity = new CharStats(parDex);
        endurance = new CharStats(parEnd);
        intelligence = new CharStats(parInt);
    }

    [SerializeField]
    private CharStats strength;

    public float Str => Strength.Value;

    [SerializeField]
    private CharStats charm;

    public float Cha => Charm.Value;

    [SerializeField]
    private CharStats endurance;

    public float End => Endurance.Value;

    [SerializeField]
    private CharStats dexterity;

    public float Dex => Dexterity.Value;

    [SerializeField]
    private CharStats intelligence;

    public float Int => Intelligence.Value;

    public CharStats Strength => strength;
    public CharStats Charm => charm;
    public CharStats Endurance => endurance;
    public CharStats Dexterity => dexterity;
    public CharStats Intelligence => intelligence;

    public CharStats GetStat(StatTypes stat)
    {
        switch (stat)
        {
            case StatTypes.Charm: return Charm;
            case StatTypes.Dex: return Dexterity;
            case StatTypes.End: return Endurance;
            case StatTypes.Str: return Strength;
            case StatTypes.Int: return Intelligence;
            default: throw new ArgumentOutOfRangeException();
        }
    }
    public void AddMods(object hasMods)
    {

    }
}