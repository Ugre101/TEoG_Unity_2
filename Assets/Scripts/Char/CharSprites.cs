﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Holder")]
public class CharSprites : ScriptableObject
{
    public Sprite GetSprite(BasicChar who) => BestMatch(who).Sprite;

    [SerializeField] private CharSprite defaultSpriteMasc = null, defaultSpriteFemi = null;

    [SerializeField] private List<CharSprite> charSprites = new List<CharSprite>();
    public List<CharSprite> List => charSprites;
    private readonly System.Random rnd = new System.Random();

    public CharSprite BestMatch(BasicChar who)
    {
        List<CharSprite> matches = charSprites.FindAll(c => c.Race == who.RaceSystem.CurrentRace());
        if (matches.Count > 0)
        {
            List<CharSprite> genderMatch = matches.FindAll(c => c.Gender == GenderExtensions.Gender(who));
            if (genderMatch.Count > 0)
            {
                return genderMatch[rnd.Next(genderMatch.Count)];
            }
            List<CharSprite> genderTypeMatch = matches.FindAll(c => c.GenderType == who.GenderType);
            if (genderTypeMatch.Count > 0)
            {
                return genderTypeMatch[rnd.Next(genderTypeMatch.Count)];
            }
            return matches[rnd.Next(matches.Count)];
        }
        else
        {
            return who.GenderType == GenderTypes.Feminine ? defaultSpriteFemi : defaultSpriteMasc;
        }
    }
}