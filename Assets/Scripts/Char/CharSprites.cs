using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Holder")]
public class CharSprites : ScriptableObject
{
    public Sprite GetSprite(BasicChar who) => BestMatch(who).Sprite;

    [SerializeField]
    private CharSprite defaultSprite = null;

    [SerializeField]
    private List<CharSprite> charSprites = new List<CharSprite>();

    private CharSprite BestMatch(BasicChar who)
    {
        List<CharSprite> matches = charSprites.FindAll(c => c.Race == who.RaceSystem.CurrentRace());
        if (matches.Count > 0)
        {
            List<CharSprite> genderMatch = matches.FindAll(c => c.Gender == who.Gender);
            if (genderMatch.Count > 0)
            {
                return genderMatch[0];
            }
            List<CharSprite> genderTypeMatch = matches.FindAll(c => c.GenderType == who.GenderType);
            if (genderTypeMatch.Count > 0)
            {
                return genderTypeMatch[0];
            }
            return matches[0];
        }
        else
        {
            // Default
            return defaultSprite;
        }
    }
}

public static class AssingGender
{
    private static Genders[] allGenders = (Genders[])Enum.GetValues(typeof(Genders));

    public static void GetEssense(BasicChar who, float amount)
    {
        Genders gender = allGenders[UnityEngine.Random.Range(0, allGenders.Length - 1)];
        switch (gender)
        {
            case Genders.Male:
                who.Masc.Gain(amount);
                break;

            case Genders.Female:
                who.Femi.Gain(amount);
                break;

            case Genders.Herm:
                who.Masc.Gain(amount / 2);
                who.Femi.Gain(amount / 2);
                break;

            case Genders.Dickgirl:
                // who.Femi.Gain(amount / 2);
                who.SexualOrgans.Dicks.AddDick();
                who.SexualOrgans.Boobs.AddBoobs();
                break;

            case Genders.Cuntboy:
                who.SexualOrgans.Vaginas.AddVag();
                break;

            case Genders.Doll:
            default:
                break;
        }
    }
}