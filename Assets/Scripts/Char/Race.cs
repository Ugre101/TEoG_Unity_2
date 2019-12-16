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

    private Races lastCurrent;

    public List<Race> RaceList
    {
        get
        {
            if (raceList.Exists(r => r.Dirty))
            {
                CleanRaces();
            }
            return raceList;
        }
    }

    public void AddRace(Races race, int amount = 100)
    {
        if (raceList.Exists(r => r.Name == race))
        {
            raceList.Find(r => r.Name == race).Gain(amount);
        }
        else
        {
            raceList.Add(new Race(race, amount));
        }
    }

    public Races CurrentRace()
    {
        if (raceList.Count < 1)
        {
            lastCurrent = Races.Humanoid;
            return Races.Humanoid;
        }
        Races race = FirstRace;
        // TODO import & improve old race system from javascript version
        if (lastCurrent != race)
        {
            // not sure if this is a good place to trigger event, will it always trigger when it should?
            RaceChangeEvent?.Invoke();
            lastCurrent = race;
        }
        return race;
    }

    public Races FirstRace => RaceList[0].Amount >= 100 ? raceList[0].Name : Races.Humanoid;

    public Races SecondRace => RaceList[1].Amount >= 50 ? raceList[1].Name : raceList[0].Amount >= 50 ? raceList[0].Name : Races.Humanoid;

    private void CleanRaces()
    {
        raceList.RemoveAll(r => r.Amount <= 0);
        raceList.Sort((r1, r2) => r1.Amount.CompareTo(r2.Amount));
        foreach (Race race in raceList.FindAll(r => r.Dirty))
        {
            race.Clean();
        }
    }

    public delegate void RaceChange();

    public event RaceChange RaceChangeEvent;
}

[System.Serializable]
public class Race
{
    public Race(Races parRace, int parAmount)
    {
        race = parRace;
        essence = parAmount;
    }

    [SerializeField]
    private Races race;

    public Races Name => race;

    [SerializeField]
    private int essence;

    public int Amount => essence;

    public bool Dirty => dirty;

    [SerializeField]
    private bool dirty = true;

    public void Clean() => dirty = false;

    public void Gain(int gain)
    {
        essence += Mathf.Max(0, gain);
        dirty = true;
    }

    /// <summary>ess -= Abs(lose), returns true if race goes to zero</summary>
    public bool Lose(int lose)
    {
        essence -= Mathf.Abs(lose);
        dirty = true;
        return essence <= 0;
    }
}