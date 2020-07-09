using UnityEngine;

[CreateAssetMenu(fileName = "Ride cowgirl", menuName = ("Sex/Vagina/Ride cowgirl"))]
public class RideCowgirl : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        // TODO tightness
        DickContainer dicks = other.SexualOrgans.Dicks;
        return $"Looking down at your defeated opponent, lying on {other.HisHer()} back, you position yourself above their crotch. Spreading your lower lips with one hand and positioning {other.HisHer()} {dicks.BiggestSizeMorInch} {dicks.Biggest().Race} dick with your other, you wrap your {player.SexualOrgans.Vaginas.List.Biggest().Race} pussy around them.\nTheir dick Tightness(enemies[EnemyIndex], player, B) your pussy.";
    }

    public override string ContinueScene(BasicChar player, BasicChar Other)
    {
        DickContainer dicks = Other.SexualOrgans.Dicks;
        string toReturn = $"Planting one hand on their chest, you continue riding {Other.HisHer()} {dicks.BiggestSizeMorInch} {dicks.Biggest().Race} dick at an erratic pace, enjoying their groans of pleasure with each thrust.";
        if (Other.SexualOrgans.HaveBalls())
        {
            toReturn += " Every time your crotch meets theirs, you can feel their balls twitch, getting ready to cum.";
        }
        return toReturn;
    }

    public override string OtherOrgasmed(PlayerMain player, BasicChar other)
    {
        return base.OtherOrgasmed(player, other);
    }

    public override string PlayerOrgasmed(PlayerMain player, BasicChar other)
    {
        return base.PlayerOrgasmed(player, other);
    }
}