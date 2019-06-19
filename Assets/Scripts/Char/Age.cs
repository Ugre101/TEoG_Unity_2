using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Age
{
    private int ageDays;
    public int AgeDays { get { return ageDays; } }
    public int Agemonth { get { return Mathf.FloorToInt(ageDays / 30); } }
    public int AgeYears { get { return Mathf.FloorToInt(ageDays / 365); } }
    public void AgeUp(int by = 1)
    {
        ageDays += by;
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
