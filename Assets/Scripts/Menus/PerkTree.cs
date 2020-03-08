using UnityEngine;

public class PerkTree : MonoBehaviour
{
    [SerializeField] private Sprite runeIMG = null;

    public void SetRuneIMGs()
    {
        if (runeIMG != null)
        {
            PerkButton[] perkButtons = GetComponentsInChildren<PerkButton>();
            foreach (PerkButton btn in perkButtons)
            {
                btn.SetRune(runeIMG);
            }
        }
    }
}