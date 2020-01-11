using UnityEngine;

[CreateAssetMenu(fileName = "Scissoring", menuName = ("Sex/Vagina/Scissoring"))]
public class Scissoring : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        return $"Looking down at {other.Identity.FirstName}, you feel a twinge of arousal pulse through your crotch. Straddling {other.HisHer()} thigh, you lift their other leg and bring your crotches together, grinding your pussy against theirs. Not leaving it at that, you bring their hand to your clit, and moan as they start playing with it.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return $"As you continue to grind your pussy against {other.HisHer()} {other.SexualOrgans.Vaginas.Biggest().Race} pussy, you reach down and toy with their clit, bringing more pleasure-filled moans to your ears.";
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