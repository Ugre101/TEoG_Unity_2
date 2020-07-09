using UnityEngine;

[CreateAssetMenu(fileName = "Forced Cunn", menuName = "Sex/LoseScenes/ForcedCunn")]
public class ForcedCunn : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string returnText = $"{other.Identity.FirstName} forces your head to their crotch, forcing you to start eating them out.";
        SexualOrgans sexualOrgans = other.SexualOrgans;
        if (sexualOrgans.HaveBalls())
        {
            System.Collections.Generic.List<Balls> balls = sexualOrgans.Balls.List;
            returnText += $" {other.HisHer(true)} { Settings.MorInch(balls.BiggestSize())} balls cover your face, forcing their musky scent into your nose. ";
        }
        returnText += " Despite your intentions, your body betrays you and orgasms as they cover your face in girlcum.";
        if (sexualOrgans.HaveBalls() && sexualOrgans.HaveDick())
        {
            returnText += "\nYou feel their balls twitch on your face, shooting cum over your back, eventually dripping into your hair.";
        }
        return returnText;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}