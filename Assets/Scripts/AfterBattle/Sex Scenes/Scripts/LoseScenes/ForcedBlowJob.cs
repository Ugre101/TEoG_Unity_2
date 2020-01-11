using UnityEngine;

[CreateAssetMenu(fileName = "Forced BlowJob", menuName = "Sex/LoseScenes/ForcedBJ")]
public class ForcedBlowJob : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        System.Collections.Generic.List<Dick> dicks = other.SexualOrgans.Dicks;
        System.Collections.Generic.List<Balls> balls = other.SexualOrgans.Balls;
        return $"{other.Identity.FirstName} forces your head to their crotch, and starts thrusting their { Settings.MorInch(dicks.BiggestSize())} dick into your mouth. Despite your intentions, your body betrays you and orgasms as they cum {Settings.LorGal(balls.Cumming())}.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
