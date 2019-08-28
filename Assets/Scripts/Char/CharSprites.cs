using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char sprites")]
public class CharSprites : ScriptableObject
{
    [Header("Human")]
    public Sprite humanMaleScout;
    public Sprite humanMaleWarrior;
    public Sprite humanMaleWorker;
    public Sprite humanMaleBarbarian;
    public Sprite humanMaleThug;
    public Sprite humanMaleMage;
    public Sprite humanFemaleWarrior;
    public Sprite humanFemaleThug;
    public Sprite humanFemaleMage;
    [Header("Orc")]
    public Sprite orcMaleWarrior;
    public Sprite orcMaleShaman;
    [Header("Elf")]
    public Sprite elfMale;
    public Sprite elfFemale;
    [Header("Dwarf")]
    public Sprite dwarfMale;
    public Sprite dwarfFemale;
    public Sprite dwarfFemaleHealer;
    [Header("Drow")]
    public Sprite drowMale;
    public Sprite drowFemale;
    public void OnEnable()
    {
        // Nice this works meaning I can use this to know what sprite to save!
        Debug.Log(humanMaleWarrior.name);
    }
    public Sprite GetSprite(BasicChar who)
    {
        switch (who.raceSystem.CurrentRace())
        {
            case Races.Dwarf:
                if (GetGenderType(who) == GenderType.Feminine)
                {
                    if (who.strength.Value > who.intelligence.Value)
                    {
                        return dwarfFemale;
                    }
                    else
                    {
                        return dwarfFemaleHealer;
                    }
                }else
                {
                    return dwarfMale;
                }
            case Races.Elf:
                if (GetGenderType(who) == GenderType.Feminine)
                {
                    return elfFemale;
                }else
                {
                    return elfMale;
                }
            case Races.Human:
                if (GetGenderType(who) == GenderType.Feminine)
                {
                    return humanFemaleWarrior;
                }else
                {
                    return humanMaleScout;
                }
            case Races.Orc:
                if (who.strength.Value > who.intelligence.Value)
                {
                    return orcMaleWarrior;
                }else
                {
                    return orcMaleShaman;
                }
            default:
                return humanMaleScout;
        }
    }
    enum GenderType
    {
        Feminine,
        Masculine
    }
    private GenderType GetGenderType(BasicChar who)
    {
        switch (who.Gender)
        {
            case Genders.Cuntboy:
            case Genders.Doll:
            case Genders.Male:
                return GenderType.Masculine;
            case Genders.Dickgirl:
            case Genders.Female:
            case Genders.Herm:
            default:
                return GenderType.Feminine;
        }
    }
}
