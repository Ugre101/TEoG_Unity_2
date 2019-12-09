using UnityEngine;

[CreateAssetMenu(fileName = "Cock vore", menuName = ("Sex/Vore/Cock vore"))]
public class CockVore : VoreScene
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (player.SexualOrgans.Balls.Count < 1)
        {
            return false;
        }
        else if (player.SexualOrgans.Dicks.Count < 1)
        {
            return false;
        }
        return player.Vore.Balls.CanVore(Other);
    }

    public override string Vore(PlayerMain player, BasicChar other)
    {
        _ = player.Vore.Balls.Vore(other);
        player.VoreChar.AddPrey(other.gameObject);
        return base.Vore(player, other);
    }
}