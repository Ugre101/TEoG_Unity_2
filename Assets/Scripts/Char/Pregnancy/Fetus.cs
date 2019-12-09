using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fetus 
{
    [SerializeField]
    private float age;
    [SerializeField]
    private Races race;
    [SerializeField]
    private ThePrey father, mother;
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
    public Fetus(Races parRace,  ThePrey parFather, ThePrey parMother)
    {
        race = parRace;
        father = parFather;
        mother = parMother;
    }
    public bool Grow(float parDaysToGrow = 1f)
    {
        age += parDaysToGrow;
        return ReadyToBeBorn;
    }
   public Child GiveBirth()
    {
        return new Child(Race,mother,father );
    }
}
