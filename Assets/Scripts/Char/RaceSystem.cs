using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceSystem
{
    [SerializeField] private List<Race> raceList = new List<Race>();

    [SerializeField] private bool dirty = true;

    public void SetDirty() => Dirty = true;

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

    [SerializeField] private Races lastCurrent, secondaryLastCurrent;

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
            toAdd.DirtyEvent += SetDirty;
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
        if (raceList.Count == 0)
        {
            lastCurrent = Races.Humanoid;
            secondaryLastCurrent = Races.Humanoid;
            return Races.Humanoid;
        }
        // TODO import & improve old race system from javascript version
        if (lastCurrent != FirstRace)
        {
            // not sure if this is a good place to trigger event, will it always trigger when it should?
            Races oldRace = lastCurrent;
            lastCurrent = FirstRace;
            RaceChange?.Invoke(oldRace, FirstRace);
        }
        if (secondaryLastCurrent != SecondRace)
        {
            Races oldRace = secondaryLastCurrent;
            secondaryLastCurrent = SecondRace;
            SecondRaceChange?.Invoke(oldRace, SecondRace);
        }
        return lastCurrent;
    }

    public Races FirstRace
    {
        get
        {
            if (RaceList.Count > 0)
            {
                return RaceList[0].Amount >= 100 ? RaceList[0].Name : Races.Humanoid;
            }
            return Races.Humanoid;
        }
    }

    public Races SecondRace
    {
        get
        {
            if (RaceList.Count > 0)
            {
                if (RaceList.Count > 1)
                {
                    return RaceList[1].Amount >= 50 ? RaceList[1].Name : RaceList[0].Amount >= 50 ? RaceList[0].Name : Races.Humanoid;
                }
                return RaceList[0].Amount >= 50 ? RaceList[0].Name : Races.Humanoid;
            }
            return Races.Humanoid;
        }
    }

    private void CleanRaces()
    {
        RaceList.RemoveAll(r => r.Name == Races.Humanoid); // Humanoid is absentnce of race, not a race.
        raceList.RemoveAll(r => r.Amount <= 0);
        raceList.Sort((r1, r2) => r1.Amount.CompareTo(r2.Amount));
        Dirty = false;
    }

    // Maybe overkill but I prefer to have the parameters named
    public delegate void RaceChanged<in t1, in t2>(t1 oldRace, t2 newRace);

    public event RaceChanged<Races, Races> RaceChange;

    public event RaceChanged<Races, Races> SecondRaceChange;
}