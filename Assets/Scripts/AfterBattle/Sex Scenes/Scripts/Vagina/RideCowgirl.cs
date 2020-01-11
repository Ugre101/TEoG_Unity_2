using UnityEngine;

[CreateAssetMenu(fileName = "Ride cowgirl", menuName = ("Sex/Vagina/Ride cowgirl"))]
public class RideCowgirl : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        // TODO tightness
        return $"Looking down at your defeated opponent, lying on {other.HisHer()} back, you position yourself above their crotch. Spreading your lower lips with one hand and positioning {other.HisHer()} {Settings.MorInch(other.SexualOrgans.Dicks.BiggestSize())} {other.SexualOrgans.Dicks.Biggest().Race} dick with your other, you wrap your {player.SexualOrgans.Vaginas.Biggest().Race} pussy around them.\nTheir dick Tightness(enemies[EnemyIndex], player, B) your pussy.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar Other)
    {
        string toReturn = $"Planting one hand on their chest, you continue riding {Other.HisHer()} {Settings.MorInch(Other.SexualOrgans.Dicks.BiggestSize())} {Other.SexualOrgans.Dicks.Biggest().Race} dick at an erratic pace, enjoying their groans of pleasure with each thrust.";
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