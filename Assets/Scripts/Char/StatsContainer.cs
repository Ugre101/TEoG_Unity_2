using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatTypes
{
    Str,
    Charm,
    End,
    Dex
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
    public StatsContainer(int parStr = 10,int parCharm = 10, int parDex = 10, int parEnd = 10, int parInt = 10)
    {
        strength = new CharStats(parStr);
        charm = new CharStats(parCharm);
        dexterity = new CharStats(parDex);
        endurance = new CharStats(parEnd);
        intelligence = new CharStats(parInt);
    }
    public CharStats strength;

    public float Str => strength.Value;
    public CharStats charm;
    public float Charm => charm.Value;
    public CharStats endurance;
    public float End => endurance.Value;
    public CharStats dexterity;
    public float Dex => dexterity.Value;
    public CharStats intelligence;
    public float Int => intelligence.Value;

    public CharStats GetStat(StatTypes stat)
    {
        switch (stat)
        {
            case StatTypes.Charm: return charm;
            case StatTypes.Dex: return dexterity;
            case StatTypes.End: return endurance;
            case StatTypes.Str: return strength;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}
