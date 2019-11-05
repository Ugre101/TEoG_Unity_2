using UnityEngine;
public class PerkButton : PerkTreeBasicBtn
{
    [Space]
    public PerksTypes perk;

    public override void Use()
    {
        if (player.PerkBool)
        { 
            taken = true;
            player.Perk.GainPerk(perk);
        }
    }
}