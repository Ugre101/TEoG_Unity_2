using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceSystem
{
    [SerializeField] private List<Race> raceList = new List<Race>();

    [SerializeField] private bool dirty = true;

    public bool Dirty
    {
        get => dirty;
        set
        {
            if (value)
            {
                CleanRaces();
                _ = CurrentRace();
            }
            dirty = false;
        }
    }

    private Races lastCurrent;

    public List<Race> RaceList => raceList;

    public void AddRace(Races race, int amount = 100)
    {
        if (raceList.Exists(r => r.Name == race))
        {
            raceList.Find(r => r.Name == race).Gain(amount);
        }
        else
        {
            Race toAdd = new Race(race, amount);
            toAdd.DirtyEvent += () => { Dirty = true; };
            raceList.Add(toAdd);
        }
        Dirty = true;
    }

    public bool RemoveRace(Races race)
    {
        if (RaceList.Exists(r => r.Name == race))
        {
            RaceList.Remove(RaceList.Find(r => r.Name == race));
            Dirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveRace(Races race, int toRemove)
    {
        if (RaceList.Exists(r => r.Name == race))
        {
            if (RaceList.Find(r => r.Name == race).Lose(toRemove))
            {
                RaceList.Remove(RaceList.Find(r => r.Name == race));
                return true;
            }
        }
        Dirty = true;
        return false;
    }

    public Races CurrentRace()
    {
        if (raceList.Count < 1)
        {
            lastCurrent = Races.Humanoid;
            return Races.Humanoid;
        }
        // TODO import & improve old race system from javascript version
        if (lastCurrent != FirstRace)
        {
            // not sure if this is a good place to trigger event, will it always trigger when it should?
            lastCurrent = FirstRace;
            RaceChangeEvent?.Invoke();
        }
        return lastCurrent;
    }

    public Races FirstRace => RaceList[0].Amount >= 100 ? RaceList[0].Name : Races.Humanoid;

    public Races SecondRace => RaceList[1].Amount >= 50 ? RaceList[1].Name : RaceList[0].Amount >= 50 ? RaceList[0].Name : Races.Humanoid;

    private void CleanRaces()
    {
        RaceList.RemoveAll(r => r.Name == Races.Humanoid); // Humanoid is absentnce of race, not a race.
        raceList.RemoveAll(r => r.Amount <= 0);
        raceList.Sort((r1, r2) => r1.Amount.CompareTo(r2.Amount));
        Dirty = false;
    }

    public delegate void RaceChange();

    public event RaceChange RaceChangeEvent;
}