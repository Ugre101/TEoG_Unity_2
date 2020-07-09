using UnityEngine;

[CreateAssetMenu(fileName = "Forced BlowJob", menuName = "Sex/LoseScenes/ForcedBJ")]
public class ForcedBlowJob : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        DickContainer dicks = other.SexualOrgans.Dicks;
        System.Collections.Generic.List<Balls> balls = other.SexualOrgans.Balls.List;
        return $"{other.Identity.FirstName} forces your head to their crotch, and starts thrusting their {dicks.BiggestSizeMorInch} dick into your mouth. Despite your intentions, your body betrays you and orgasms as they cum {Settings.LorGal(balls.Cumming())}.";
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}