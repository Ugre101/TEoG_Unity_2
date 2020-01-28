using UnityEngine;

[CreateAssetMenu(fileName = "Get cunnilingus", menuName = ("Sex/Vagina/Get cunnilingus"))]
public class GetCunnilingus : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        return $"Your foe lays on their back, chest heaving with exhaustion from the recent fight. You make your way up {other.Identity.FirstName}'s body, licking your lips in anticipation. Squatting above their head, you line your crotch up with their mouth. Grabbing their head, you grind their face against your {player.SexualOrgans.Vaginas.Biggest().Race} pussy, until they start eating you out with {other.HisHer()} {other.Race(true)} tounge.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return $"Keeping them in place, you force them to continue eating you out, electing soft moans from your throat. Barely able to move, all {other.Identity.FirstName} can do is continue eating you out. You reach one hand to their head and gently pet it, telling them they're doing a good job.";
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