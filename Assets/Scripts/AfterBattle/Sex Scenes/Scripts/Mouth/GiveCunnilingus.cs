using UnityEngine;

[CreateAssetMenu(fileName = "Give cunnilingus", menuName = ("Sex/Mouth/Give cunnilingus"))]
public class GiveCunnilingus : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        SexualOrgans sexualOrgans = other.SexualOrgans;
        Identity identity = other.Identity;
        return $"{identity.FirstName} lays on their back, chest heaving with exhaustion from the recent fight. You make your way up to {identity.FirstName}'s body and crouch between their legs, spreading them apart. In-between lies their {sexualOrgans.Vaginas.List[0].Race} pussy, engorged and dripping slightly, their clit twitching lewdly with each breath of your foe. You lick your lips in anticipation as you lower your head to your prize, spreading their lower lips with your fingers, and start eating them out.";
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return $"Keeping them in place, you continue your tongue-lashing, electing soft moans from your opponent. Barely able to move, all {other.Identity.FirstName} can do is rest their hand on your head. You reach one hand to their clit and gently pinch it, sending a fresh wave of pleasure through their body.";
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