using UnityEngine;

[CreateAssetMenu(fileName = "Get blowjob", menuName = ("Sex/Dick/GetBlowjob"))]
public class GetBlowjob : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string dickSize = Settings.MorInch(player.SexualOrgans.Dicks.BiggestSizeValue);
        return $"You walk up to your defeated adversary as they attempt to get back on their feet.You stop them by catching their head and tilting it up to your face. You look back down at your crotch and nod to your {dickSize} cock expectantly. Just as your prize gets the idea and moves closer you eagerly thrust your hips into their mouth. You hold their head close starting a steady rhythm as you use their hole."
        // "Your last blow sends your foe recoiling back losing their footing and crashing to the floor. You make your way up to them until you cast a shadow of their body. Your adversary groans as they start to rise, only to be met with the sight of your (insert player dick size. small, average, hefty, enormous) member. Stunned by the position they are in you grab the back of their head and guide their mouth to its rightful place and begin to enjoy your prize"
;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        // DocId("SexText").innerHTML = "You continue humping your new toy at a constant pace. Your rhythm doesn’t falter as you use your muscles to the best of their ability. You lean back as you hilt into the back of their throat, eliciting a moan from you as you start breeding deep."
        if (player.SexualOrgans.Balls.BiggestSizeValue > 5)
        {
            string dickSize = player.SexualOrgans.Dicks.BiggestSizeMorInch;
            return $"Continuing to thrust, your {dickSize} balls slap repeatedly against your foe, causing them to grunt in annoyance. Your thrusting continues as you make proper use of your opponent’ s mouth. You grab your foe 's forearm and guide it to your sac, grunting in demand as they start to fondle you.";
        }
        return $"Your thrusting continues as you make proper use of your opponent’s mouth. Your pounding of their throat continues even as your abdomen starts bumping into their nose with each thrust. Muffled groans escape from your mouth as {other.Identity.FirstName}’s mouth is pumped by your throbbing cock."; //They moan as your cock snakes its way through their mouth greedily humps their throat."
    }
}