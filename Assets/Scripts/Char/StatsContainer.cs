using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatTypes
{
    Str,
    Charm,
    End,
    Dex,
    Int,
    Will
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
        willpower = new CharStats();
        GetAll = new List<CharStats>() { strength, charm, dexterity, endurance, intelligence, willpower };
    }

    public StatsContainer(int parStr = 10, int parCharm = 10, int parDex = 10, int parEnd = 10, int parInt = 10, int parWill = 10)
    {
        strength = new CharStats(parStr);
        charm = new CharStats(parCharm);
        dexterity = new CharStats(parDex);
        endurance = new CharStats(parEnd);
        intelligence = new CharStats(parInt);
        willpower = new CharStats(parWill);
        GetAll = new List<CharStats>() { strength, charm, dexterity, endurance, intelligence, willpower };
    }

    [SerializeField] private CharStats strength;

    public float Str => Strength.MaxValue;

    [SerializeField] private CharStats charm;

    public float Cha => Charm.MaxValue;

    [SerializeField] private CharStats endurance;

    public float End => Endurance.MaxValue;

    [SerializeField] private CharStats dexterity;

    public float Dex => Dexterity.MaxValue;

    [SerializeField] private CharStats intelligence;

    public float Int => Intelligence.MaxValue;

    [SerializeField] private CharStats willpower;

    public float Will => willpower.MaxValue;
    public CharStats Strength => strength;
    public CharStats Charm => charm;
    public CharStats Endurance => endurance;
    public CharStats Dexterity => dexterity;
    public CharStats Intelligence => intelligence;
    public CharStats Willpower => willpower;
    public List<CharStats> GetAll { get; }

    public CharStats GetStat(StatTypes stat)
    {
        switch (stat)
        {
            case StatTypes.Charm: return Charm;
            case StatTypes.Dex: return Dexterity;
            case StatTypes.End: return Endurance;
            case StatTypes.Str: return Strength;
            case StatTypes.Int: return Intelligence;
            case StatTypes.Will: return Willpower;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void AddMods(List<AssingStatmod> mods)
    {
        mods.ForEach(m => GetStat(m.StatTypes).AddMods(m.StatMod));
    }

    public void AddTempMods(List<AssingTempStatMod> mods)
    {
        mods.ForEach(m => GetStat(m.StatTypes).AddTempMod(m.TempStatMod));
    }
}