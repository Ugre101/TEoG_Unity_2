using UnityEngine;

[CreateAssetMenu(fileName = "Forced Get Fucked", menuName = "Sex/LoseScenes/ForcedGetFucked")]
public class ForcedGetFucked : LoseScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        string returnText = "Forcing you on your back, your enemy fondles your clit just enough for your body to betray you and your pussy to get wet. ";

        Organs sexualOrgans = player.SexualOrgans;
        returnText += sexualOrgans.HaveBalls()
            ? "Moving your balls to the side, they thrust in to you."
            : "Spreading your lips with one hand, they thrust into you.";

        System.Collections.Generic.List<Balls> Oballs = other.SexualOrgans.Balls.List;
        returnText += $"\n\nKnowing how to handle someone with as little experience as you, they pin your arms above your head and quickly bring you to orgasm, your shuddering walls causing them to cum{Settings.LorGal(Oballs.Cumming())} into you. ";
        if (sexualOrgans.Vaginas.List.EmptyWomb())
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
