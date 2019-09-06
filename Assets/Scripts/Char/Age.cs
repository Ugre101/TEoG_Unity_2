using UnityEngine;

public class Age
{
    public Age(int AgeInYears = 18)
    {
        ageYears = AgeInYears;
    }

    private int ageYears;
    private int ageDays;
    public int AgeDays { get { return ageDays; } }
    public int Agemonth { get { return Mathf.FloorToInt(ageDays / 30); } }
    public int AgeYears { get { return ageYears; } set { ageYears = value; } }

    public bool AgeUp(int by = 1)
    {
        ageDays += by;
        if (ageDays > 365)
        {
            ageYears++;
            ageDays -= 365;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AgeDown(int by = 1)
    {
        ageDays -= by;
        if (ageDays <= 0)
        {
            ageYears--;
            ageDays += 365;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Diffent races age different a 100 years old elf is quite young, while a 100 years old human is dying.
    public string AgeByRace(BasicChar who)
    {
        switch (who.Race)
        {
            default:
                return AgeYears >= 18 ? "adult" : "minor";
        }
    }
}