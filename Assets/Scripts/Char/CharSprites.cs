using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Holder")]
public class CharSprites : ScriptableObject
{
    public void OnEnable()
    {
    }

    public Sprite GetSprite(ThePrey who)
    {
        return BestMatch(who).sprite;

        #region old code

        /* switch (who.raceSystem.CurrentRace())
         {
             case Races.Dwarf:
                 if (who.GenderType == GenderType.Feminine)
                 {
                     if (who.strength.Value > who.intelligence.Value)
                     {
                         return dwarfFemale;
                     }
                     else
                     {
                         return dwarfFemaleHealer;
                     }
                 }
                 else
                 {
                     return dwarfMale;
                 }
             case Races.Elf:
                 if (who.GenderType == GenderType.Feminine)
                 {
                     return elfFemale;
                 }
                 else
                 {
                     return elfMale;
                 }
             case Races.Human:
                 if (who.GenderType == GenderType.Feminine)
                 {
                     return humanFemaleWarrior;
                 }
                 else
                 {
                     return humanMaleScout;
                 }
             case Races.Orc:
                 if (who.strength.Value > who.intelligence.Value)
                 {
                     return orcMaleWarrior;
                 }
                 else
                 {
                     return orcMaleShaman;
                 }
             default:
                 return humanMaleScout;
         } */

        #endregion old code
    }

    public CharSprite defaultSprite;
    public List<CharSprite> charSprites;

    private CharSprite BestMatch(ThePrey who)
    {
        if (charSprites.Exists(c => c.race.ToString() == who.Race))
        {
            if (charSprites.Exists(c => c.gender == who.Gender))
            {
                // if class exist chose class
                if (false)
                {
                }
                else
                {
                    return charSprites.Find(c => c.gender == who.Gender);
                }
            }
            else
            {
                // first hit
                return charSprites.Find(c => c.race.ToString() == who.Race);
            }
        }
        else
        {
            // Default
            return defaultSprite;
        }
    }
}