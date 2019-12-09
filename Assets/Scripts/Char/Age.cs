using System;
using UnityEngine;

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
    public Age(int AgeInYears = 18)
    {
        ageYears = AgeInYears;
    }

    [SerializeField]
    private int ageYears;

    [SerializeField]
    private int ageDays;

    public int AgeDays => ageDays;
    public int Agemonth => Mathf.FloorToInt(ageDays / 30);
    public int AgeYears => ageYears;

    public bool AgeUp(int byDays = 1)
    {
        ageDays += byDays;
        if (ageDays > 365)
        {
            ageYears++;
            ageDays -= 365;
            return true;
        }
        return false;
    }

    public bool AgeDown(int byDays = 1)
    {
        ageDays -= byDays;
        if (ageDays <= 0)
        {
            ageYears--;
            ageDays += 365;
            return true;
        }
        return false;
    }

    // Diffent races age different a 100 years old elf is quite young, while a 100 years old human is dying.
    public AgeClass AgeByRace(ThePrey who)
    {
        switch (who.RaceSystem.CurrentRace())
        {
            case Races.Human:
            default:
                if (AgeYears < 5)
                {
                    return AgeClass.Baby;
                }else if (AgeYears < 13)
                {
                    return AgeClass.Child;
                }else if (AgeYears < 18)
                {
                    return AgeClass.Teenager;
                }else if (AgeYears < 25)
                {
                    return AgeClass.YoungAdult;
                }else if (AgeYears < 65)
                {
                    return AgeClass.Adult;
                }else
                {
                    return AgeClass.Senior;
                }
        }
    }
}