using UnityEngine;

[CreateAssetMenu(fileName = "Breast vore", menuName = ("Sex/Vore/Breast vore"))]
public class BreastVore : VoreScene
{
    public override bool CanDo(BasicChar player, Vore.ThePrey Other)
    {
        if (player.SexualOrgans.Boobs.BiggestSize() < 3)
        {
            return false;
        }
        return player.Vore.Boobs.CanVore(Other);
    }

    public override string Vore(PlayerMain player, Vore.ThePrey other)
    {
        if (player.Vore.Boobs.Vore(other))
        {
            player.VoreChar.Boobs.AddPrey(other);
            return $"";
        }
        return $"";
    }
}