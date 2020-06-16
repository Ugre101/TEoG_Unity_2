using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Fucked Doggystyle", menuName = "Sex/LoseScenes/ForcedGetFuckedDoggystyle")]
public class ForcedGetFuckedDoggystyle : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        string returnText = $"{other.Identity.FirstName} forces your beaten body to its hands and knees; you can't even muster the energy to collapse. Your body, however, has other plans, and you feel your pussy start to drip. Your opponent wastes little time, and quickly gets into position, soon thrusting deeply into you. ";

        Organs PsexualOrgans = player.SexualOrgans;
        if (PsexualOrgans.HaveBalls())
        {
            returnText += "With every thrust, you feel their balls tapping into yours, sending little bursts of unintended pleasure through your dick. ";
        }
        returnText += "\n\nIt doesn't take long for you to cum, your pussy's walls quivering around their dick. ";
        if (PsexualOrgans.HaveBalls() && PsexualOrgans.HaveDick())
        {
            returnText += "Your balls refuse to be left out, and unload themselves onto your stomach and the ground. ";
        }
        returnText += "Your enemy cums soon after, quickly filling your pussy and collapses onto your back, spent. ";
        if (PsexualOrgans.Vaginas.List.EmptyWomb())
        {
            if (player.Impregnate(other))
            {
                returnText += $" You feel an odd filling sensation in your belly... They couldn't have gotten you pregnant, could they?";
            }
        }
        return returnText;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}