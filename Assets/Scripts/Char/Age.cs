using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum AgeClass
{
    Baby,
    Child,
    Teenager,
    YoungAdult,
    Adult,
    Senior
}

[Serializable]
public class Age
{
    public Age(int ageInYears = 18)
    {
        ageYears = ageInYears;
    }

    [SerializeField] private int ageYears, ageDays;

    public int AgeDays => ageDays;
    public int Agemonth => Mathf.FloorToInt(ageDays / 30);
    public int AgeYears => ageYears;

    public bool AgeUp(int byDays = 1)
    {
        ageDays += byDays;
        if (ageDays <= 365) return false;
        ageYears++;
        ageDays -= 365;
        BirthdDay?.Invoke(AgeYears);
        return true;
    }

    public bool AgeDown(int byDays = 1)
    {
        ageDays -= byDays;
        if (ageDays > 0) return false;
        ageYears--;
        ageDays += 365;
        return true;
    }

    // Diffent races age different a 100 years old elf is quite young, while a 100 years old human is dying.
    public AgeClass AgeByRace(BasicChar who)
    {
        int babyAge = 5, childAge = 13, teenAge = 18, youngAdultAge = 25, adultAge = 65;
        switch (who.RaceSystem.CurrentRace())
        {
            case Races.Humanoid:
            case Races.Elf:
                babyAge = 7; // This is not set in stone can change
                childAge = 18;
                teenAge = 25;
                youngAdultAge = 100;
                adultAge = 200;
                break;
            case Races.Orc:
            case Races.Troll:
            case Races.Dwarf:
                youngAdultAge = 35;
                adultAge = 100;
                break;
            case Races.Halfling:
            case Races.Fairy:
            case Races.Incubus:
            case Races.Succubus:
            case Races.Equine:
            case Races.Dragon:
            case Races.DragonKin:
            case Races.Amazon:
            case Races.DarkElf:
            case Races.Bovine:
            case Races.Human:
            default:
                break;
        }

        if (AgeYears < babyAge)
        {
            return AgeClass.Baby;
        }
        else if (AgeYears < childAge)
        {
            return AgeClass.Child;
        }
        else if (AgeYears < teenAge)
        {
            return AgeClass.Teenager;
        }
        else if (AgeYears < youngAdultAge)
        {
            return AgeClass.YoungAdult;
        }
        else if (AgeYears < adultAge)
        {
            return AgeClass.Adult;
        }
        else
        {
            return AgeClass.Senior;
        }
    }


    public Action<int> BirthdDay;
}