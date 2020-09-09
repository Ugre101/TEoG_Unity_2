using UnityEngine;

public class PerkTree : MonoBehaviour
{
    [SerializeField] private Sprite runeIMG = null;

    public void SetRuneIMGs()
    {
        if (runeIMG == null) return;
        PerkTreeBasicBtn[] perkButtons = GetComponentsInChildren<PerkTreeBasicBtn>();
        foreach (PerkTreeBasicBtn btn in perkButtons)
        {
            btn.SetRune(runeIMG);
        }
    }
}