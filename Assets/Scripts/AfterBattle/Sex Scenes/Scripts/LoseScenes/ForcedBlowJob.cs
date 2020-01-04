using UnityEngine;

[CreateAssetMenu(fileName = "Forced BlowJob", menuName = "Sex/LoseScenes/ForcedBJ")]
public class ForcedBlowJob : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        return $"{other.Identity.FirstName} forces your head to their crotch, and starts thrusting their { Settings.MorInch(other.SexualOrgans.Dicks.Biggest())} dick into your mouth. Despite your intentions, your body betrays you and orgasms as they cum {Settings.LorGal(other.SexualOrgans.Balls.Cumming())}.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
