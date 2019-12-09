using UnityEngine;

[CreateAssetMenu(fileName = "Cock vore", menuName = ("Sex/Vore/Cock vore"))]
public class CockVore : VoreScene
{
    public override bool CanDo(ThePrey player, ThePrey Other)
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

    public override string Vore(PlayerMain player, ThePrey other)
    {
        if (player.Vore.Balls.Vore(other))
        {
            Debug.Log(true);
            player.VoreChar.Balls.AddPrey(other);
        }
        return base.Vore(player, other);
    }
}