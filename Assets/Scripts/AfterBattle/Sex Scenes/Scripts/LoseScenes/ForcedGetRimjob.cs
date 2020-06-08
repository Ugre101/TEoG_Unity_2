using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Rimjob", menuName = "Sex/LoseScenes/ForcedGetRimjob")]
public class ForcedGetRimjob : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        string returnText = string.Empty;
        if (GenderExtensions.Gender(player) == Genders.Doll && other.SexualOrgans.HaveDick())
        {
            returnText += "Rather than use their penis on your ass, they'd rather find a pussy instead. ";
        }
        else if (GenderExtensions.Gender(player) != Genders.Doll)
        {
            returnText += "Rather than pleasuring your more sensitive organs, they've decided to humiliate you instead. ";
        }
        returnText += "Forcing you onto your stomach, your enemy repeatedly smacks your ass, bringing a blush to both sets of cheeks. Despite your humiliation (and your ass getting sore), you soon orgasm";
        if (GenderExtensions.Gender(player) == Genders.Herm)
        {
            returnText += ", spurting cum from your dick onto your belly and soaking your thighs.";
        }
        else if (player.SexualOrgans.HaveDick())
        {
            returnText += ", spurting cum from your dick onto your belly.";
        }
        else if (player.SexualOrgans.HaveVagina())
        {
            returnText += ",  soaking your thighs.";
        }
        else
        {
            returnText += ", shuddering in unwanted pleasure.";
        }
        return returnText;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}