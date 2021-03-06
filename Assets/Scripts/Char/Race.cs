﻿using UnityEngine;

public enum Races
{
    Humanoid,
    Human,
    Elf,
    Orc,
    Troll,
    Dwarf,
    Halfling,
    Fairy,
    Incubus,
    Succubus,
    Equine,
    Dragon,
    DragonKin, // Humaniod dragon
    Amazon, // Human sub-species
    DarkElf,
    Bovine,
}

public enum HalfRaces
{
    Centaur,
    HalfElf
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

    public void Gain(int gain)
    {
        essence += Mathf.Max(0, gain);
        DirtyEvent?.Invoke();
    }

    /// <summary>ess -= Abs(lose), returns true if race goes to zero</summary>
    public bool Lose(int lose)
    {
        essence -= Mathf.Abs(lose);
        DirtyEvent?.Invoke();
        return essence <= 0;
    }

    public void LoseAll()
    {
        essence = 0;
        DirtyEvent?.Invoke();
    }

    public delegate void RaceEssenceChange();

    public event RaceEssenceChange DirtyEvent;
}

public static class RaceExtensions
{
    public static int AwakeTimeModifer(this Races race)
    {
        switch (race)
        {
            case Races.Humanoid:
            case Races.Human:
            case Races.Elf:
            case Races.Orc:
            case Races.Troll:
            case Races.Dwarf:
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
            default: return 0;
        }
    }
}