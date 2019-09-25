using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicStatButton : PerkTreeBasicBtn
{
    public StatType stat;
    public int statAmount = 1;
    public override void Use()
    {
        if (player.PerkBool)
        {
            taken = true;
            switch (stat)
            {
                case StatType.Charm:
                    player.charm._baseValue += statAmount;
                    break;
                case StatType.Dex:
                    player.dexterity._baseValue += statAmount;
                    break;
                case StatType.End:
                    player.dexterity._baseValue += statAmount;
                    break;
                case StatType.Str:
                    player.strength._baseValue += statAmount;
                    break;
            }
        }
    }
}
