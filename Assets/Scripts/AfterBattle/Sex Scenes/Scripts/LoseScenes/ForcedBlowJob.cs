using UnityEngine;

[CreateAssetMenu(fileName = "Forced BlowJob", menuName = ("Sex/LoseScenes/ForcedBJ"))]
public class ForcedBlowJob : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        return $"{other.Identity.FirstName} forces your head to their crotch, and starts thrusting their { Settings.MorInch(other.SexualOrgans.Dicks.Biggest())} dick into your mouth. Despite your intentions, your body betrays you and orgasms as they cum {5 }";
        // TODO add cumming amount
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return base.ContinueScene(player, other);
    }
}

[CreateAssetMenu(fileName = "Forced Cunn", menuName = ("Sex/LoseScenes/ForcedCunn"))]
public class ForcedCunn : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        string returnText = $"{ other.Identity.FirstName} forces your head to their crotch, forcing you to start eating them out.";
        if (other.SexualOrgans.Balls.Count > 0)
        {
            returnText += $"Their { Settings.MorInch(other.SexualOrgans.Balls.Biggest())} balls cover your face, forcing their musky scent into your nose. ";
        }
        returnText += "Despite your intentions, your body betrays you and orgasms as they cover your face in girlcum.";
        if (other.Gender == Genders.Herm)
        {
            returnText += "\nYou feel their balls twitch on your face, shooting cum over your back, eventually dripping into your hair.";
        }
        return returnText;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return base.ContinueScene(player, other);
    }
}