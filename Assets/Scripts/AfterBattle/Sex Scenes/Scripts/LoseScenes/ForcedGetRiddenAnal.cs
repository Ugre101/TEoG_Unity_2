using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get RiddenAnal", menuName = "Sex/LoseScenes/ForcedGetRiddenAnal")]
public class ForcedGetRiddenAnal : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        string returnText = "Pushing you over, your enemy fondles your balls, quickly giving you an erection. Straddling your groin, they quickly thrust down, riding your dick. ";
        if (player.SexualOrgans.HaveBoobs() && other.SexualOrgans.HaveBoobs())
        {
            returnText += "As they bounce up and down on your rod, they hug you close, mashing your nipples and theirs together, sending shivers of pleasure through your chest. ";
        }
        returnText += "\n\nIt doesn't take long before you cum, emptying your balls into their ass. They're not satisfied yet, though, and continue to ride you for several orgasms. ";
       // TODO anal preg, eggs etc..
        return returnText;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}