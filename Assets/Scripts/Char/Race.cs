using System.Collections.Generic;
using UnityEngine;

public enum Races
{
    Humanoid,
    Human,
    Elf,
    Orc,
    Dwarf
}

[System.Serializable]
public class RaceSystem 
{
    [SerializeField]
    private List<Race> raceList = new List<Race>();
    public List<Race> RaceList { get { return raceList; } }
    public void AddRace(Races race, int amount = 1)
    {
        if (raceList.Exists(r => r.Name == race))
        {
            raceList.Find(r => r.Name == race).Gain(amount);
        }else
        {
            raceList.Add(new Race(race, amount));
        }
    }
    public Races CurrentRace()
    {
        if (raceList.Count < 1)
        {
            return Races.Humanoid;
        }
        Races race = FirstRace();
        // import & improve old race system from javascript version
        return race;
    }
    public Races FirstRace()
    {
        CleanRaces();
        return raceList[0].Amount >= 100 ? raceList[0].Name : Races.Humanoid;
    }
    public Races SecondRace()
    {
        CleanRaces();
        return raceList[1].Amount >= 50 ? raceList[1].Name : raceList[0].Amount >= 50 ? raceList[0].Name : Races.Humanoid ;
    }
    private void CleanRaces()
    {
        if (raceList.Exists(r => r.Dirty == true))
        {
            raceList.Sort((r1, r2) => r1.Amount.CompareTo(r2.Amount));
            foreach (Race race in raceList.FindAll(r => r.Amount <= 0))
            {
                raceList.Remove(race);
            }
            foreach(Race race in raceList.FindAll(r => r.Dirty == true))
            {
                race.Dirty = false;
            }
        }
    }
}
[System.Serializable]
public class Race
{
    public Race(Races r, int a) { race = r; essence = a; }
    [SerializeField]
    private Races race;
    public Races Name { get { return race; } }
    [SerializeField]
    private int essence;
    public int Amount { get { return essence; } }
    public bool Dirty = true;

    public void Gain(int gain)
    {
        essence += Mathf.Max(0, gain);
        Dirty = true;
    }

    public bool Lose(int lose)
    {
        essence -= Mathf.Max(0, lose);
        Dirty = true;
        return essence <= 0;
    }
}