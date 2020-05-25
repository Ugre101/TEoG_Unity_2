using System;
using UnityEngine;

[Serializable]
public class Fetus
{
    [SerializeField] private float age;

    [SerializeField] private Races race;

    [SerializeField] private Identity father, mother;

    [SerializeField] private bool playerFather = false, playerMother = false;

    public bool PlayerFather => playerFather;
    public bool PlayerMother => playerMother;
    public string FatherName => father.FullName;
    public string MotherName => mother.FullName;
    public Races Race => race;
    public int DaysOld => Mathf.FloorToInt(age);
    public bool ReadyToBeBorn => DaysOld >= IncubationPeriod();

    /// <summary>
    /// Returns the incubation period, is dependent on the fetus race.
    /// </summary>
    /// <returns></returns>
    public int IncubationPeriod()
    {
        switch (Race)
        {
            case Races.Humanoid:
            case Races.Human:
            case Races.Elf:
            case Races.Orc:
            case Races.Dwarf:
            default:
                return 274;
        }
    }

    public Fetus(Races parRace, BasicChar parFather, BasicChar parMother)
    {
        race = parRace;
        father = parFather.Identity;
        if (parFather is PlayerMain)
        {
            playerFather = true;
        }
        mother = parMother.Identity;
        if (parMother is PlayerMain)
        {
            playerMother = true;
        }
    }

    public bool Grow(float parDaysToGrow = 1f)
    {
        age += parDaysToGrow;
        return ReadyToBeBorn;
    }

    public Child GiveBirth() => new Child(Race, mother, father);
}