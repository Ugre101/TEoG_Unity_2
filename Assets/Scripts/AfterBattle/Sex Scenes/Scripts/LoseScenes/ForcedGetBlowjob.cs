using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Blowjob", menuName = "Sex/LoseScenes/ForcedGetBlowjob")]
public class ForcedGetBlowjob : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string returnText = $"Forcing you onto your back, {other.Identity.FirstName} expertly massages your cock and balls, quickly bringing you erect. ";
        SexualOrgans sexualOrgans = player.SexualOrgans;
        if (sexualOrgans.HaveVagina())
        {
            returnText += " They even tease your pussy a bit, all to make you cum quicker.";
        }
        System.Collections.Generic.List<Balls> balls = sexualOrgans.Balls.List;
        returnText += $"\n\nUnable to put up more than a feeble struggle, you find yourself cumming {Settings.LorGal(balls.Cumming())} down their throat seconds after their lips meet your dick's head.";
        return returnText;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
