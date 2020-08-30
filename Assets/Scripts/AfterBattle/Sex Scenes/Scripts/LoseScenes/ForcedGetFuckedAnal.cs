using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Fucked Anal", menuName = "Sex/LoseScenes/ForcedGetFuckedAnal")]
public class ForcedGetFuckedAnal : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string returnText = $"{other.Identity.FirstName} forces your beaten body to its hands and knees; you can't even muster the energy to collapse. ";

        SexualOrgans sexualOrgans = player.SexualOrgans;
        if (sexualOrgans.HaveVagina())
        {
            returnText += "Your body, however, has other plans, and you feel your pussy start to drip. Even with such a clear signal, though, your enemy positions themselves a little higher. ";
        }
        returnText += "Having gotten into position, your enemy spreads your ass cheeks, and slowly works their dick into your bowels. Unable to respond, you feel your ass getting stretched. ";

        if (GenderExtensions.Gender(player) == Genders.Herm && sexualOrgans.HaveBalls())
        {
            returnText += "With every thrust, you feel their balls tapping your balls and clit, sending little bursts of unintended pleasure through your organs.\nIt doesn't take long for you to orgasm, your pussy's walls quivering, milking a nonexistent dick. Your balls refuse to be left out, and unload themselves onto your stomach and the ground. ";
        }
        else if (sexualOrgans.HaveVagina())
        {
            returnText += "With every thrust, you feel their balls tapping your clit, sending little bursts of unintended pleasure through your pussy.\nIt doesn't take long for you to orgasm, your pussy's walls quivering, milking a nonexistent dick. ";
        }
        else if (sexualOrgans.HaveBalls())
        {
            returnText += "With every thrust, you feel their balls tapping yours, sending little bursts of unintended pleasure through your dick.\nIt doesn't take long for you to orgasm, your balls unloading themselves onto your stomach and the ground. ";
        }
        else
        {
            returnText += "\n\nIt doesn't take long for you to orgasm, a pleasurable heat spreading from your ass. ";
        }
        returnText += "Your enemy cums soon after, quickly filling your ass and collapses onto your back, spent. ";
        // TODO anal preg, eggs etc...
        return returnText;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}