using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Holder")]
public class CharSprites : ScriptableObject
{
    public void OnEnable()
    {
    }

    public Sprite GetSprite(BasicChar who) => BestMatch(who).sprite;

    public CharSprite defaultSprite;
    public List<CharSprite> charSprites;

    private CharSprite BestMatch(BasicChar who)
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