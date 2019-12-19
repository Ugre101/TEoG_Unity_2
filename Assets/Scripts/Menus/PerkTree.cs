using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerkTree : MonoBehaviour
{
    public List<GameObject> perkRunes;
    public Sprite runeIMG;

    public void SetRuneIMGs()
    {
        if (perkRunes.Count > 0 && runeIMG != null)
        {
            foreach (GameObject rune in perkRunes)
            {
                Image img = rune.GetComponent<Image>();
                img.sprite = runeIMG;
            }
        }
    }
}
