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

    [SerializeField] private CharStats strength, charm, endurance, dexterity, intelligence, willpower;

    public float Str => Strength.Value;

    public float Cha => Charm.Value;

    public float End => Endurance.Value;

    public float Dex => Dexterity.Value;

    public float Int => Intelligence.Value;

    public float Will => willpower.Value;
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

    public void SetBaseValues(int str, int cha, int end, int dex, int inte, int will)
    {
        Strength.BaseValue = str;
        Charm.BaseValue = cha;
        Endurance.BaseValue = end;
        Dexterity.BaseValue = dex;
        Intelligence.BaseValue = inte;
        Willpower.BaseValue = will;
    }
}

public static class StatExtensions
{
    // Seperated from container to make it easier to handle perks
    public static int Strength(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Strength.Value;

        return baseVal;
    }

    public static int Charm(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Charm.Value;

        return baseVal;
    }

    public static int Endurance(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Endurance.Value;

        return baseVal;
    }

    public static int Dexterity(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Dexterity.Value;

        return baseVal;
    }

    public static int Intelligence(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Intelligence.Value;

        return baseVal;
    }

    public static int Willpower(this BasicChar basicChar)
    {
        int baseVal = basicChar.Stats.Willpower.Value;

        return baseVal;
    }
}