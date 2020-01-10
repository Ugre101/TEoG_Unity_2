using UnityEngine;

[CreateAssetMenu(fileName = "Give blowjob", menuName = ("Sex/Mouth/Give blowjob"))]
public class GiveBlowjob : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        System.Collections.Generic.List<Dick> dicks = other.SexualOrgans.Dicks;
        Identity identity = other.Identity;
        return $"Your foe lays on their back, chest heaving with exhaustion from the recent fight. You make your way up to {identity.FirstName}'s body and crouch between their legs, spreading them apart.In - between lies their {Settings.MorInch(dicks.Biggest())} { dicks[0].Race} cock lewdly pulsing as it bobs side-to - side with each breath of your foe. You lick your lips in anticipation as you lower your head to your prize, wrapping your lips around the head of their dick and start sucking.";
        // They still lies on their stomach still spent from the battle. You grab their torso and flip them on their back for your viewing pleasure. Their dick lays flat against their stomach twitching almost expectantly. You lower your head between their legs and just before the they has time to protest you begin tending to their cock causing them to gasp.
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        string toReturn = $"Your head continues to bob on their length as your tongue plays with their tip, electing soft moans from your opponent. As {other.Identity.FirstName} begins to softly hump into your throat, you meet each thrust by pushing your head down as far as you can.You emphasize their thrusts by sucking hard on their throbbing length.You feel their hand being placed on your head, pushing you deeper into their crotch.";
        Organs sexualOrgans = other.SexualOrgans;
        if (sexualOrgans.HaveBalls(5))
        {
            System.Collections.Generic.List<Dick> dicks = sexualOrgans.Dicks;
            toReturn += $" Your lips meet their crotch as their ${Settings.MorInch(dicks.Biggest())} nuts start to bump into your chin with each hump.";
        }
        toReturn += " Their breathing becomes deep and labored as you milk their cock for all it’s worth.";
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