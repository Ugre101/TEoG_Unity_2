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

    public Fetus(Races parRace, BasicChar parFather, BasicChar parMother, bool playerFather = false, bool playerMother = false)
    {
        race = parRace;
        father = parFather.Identity;
        mother = parMother.Identity;
        this.playerFather = playerFather;
        this.playerMother = playerMother;
    }

    public bool Grow(float parDaysToGrow = 1f)
    {
        age += parDaysToGrow;
        return ReadyToBeBorn;
    }

    public Child GiveBirth() => new Child(Race, mother, father, PlayerFather, PlayerMother);
}