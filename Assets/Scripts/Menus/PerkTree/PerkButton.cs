public class PerkButton : PerkTreeBasicBtn
{
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