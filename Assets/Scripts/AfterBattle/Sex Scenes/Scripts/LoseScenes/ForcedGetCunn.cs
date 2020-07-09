using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Cunn", menuName = "Sex/LoseScenes/ForcedGetCunn")]
public class ForcedGetCunn : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string returnText = $"Forcing you onto your back, {other.Identity.FirstName} expertly fingers your pussy, quickly making you wet. ";
        if (player.SexualOrgans.HaveBalls())
        {
            returnText += " They even tease your balls a bit, all to make you cum quicker. ";
        }
        returnText += "\n\nUnable to put up more than a feeble struggle, you find yourself cumming around their tongue seconds after it penetrates your lower lips.";
        return returnText;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
