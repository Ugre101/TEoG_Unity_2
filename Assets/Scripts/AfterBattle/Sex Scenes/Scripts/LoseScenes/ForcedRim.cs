using UnityEngine;

[CreateAssetMenu(fileName = "Forced Rimjob", menuName = "Sex/LoseScenes/ForcedRimjob")]
public class ForcedRim : LoseScene
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        string returnText;
        if (GenderExtensions.Gender(other) != Genders.Doll)
        {
            returnText = $"Despite having more sensitive erogenous zones, { other.Identity.FirstName} wants to maximize your humiliation by forcing you to eat their ass out. They force you to the ground and sit on your face, giving you no other option than to eat their ass out for their pleasure.";
        }
        else
        {
            returnText = GenderExtensions.Gender(other) == Genders.Doll && !player.SexualOrgans.HaveDick()
                ? $"With no other way to get pleasure, {other.Identity.FirstName} forces you to the ground and sit on your face, giving you no other option than to eat their ass out for pleasure. "
                : $"Rather that let you use your dick on their only hole, {other.Identity.FirstName} decides to force you to use your tongue. They force you to the ground and sit on your face, giving you no other option than to eat their ass out for their pleasure.";
        }
        returnText += "\n\nDespite your humiliating position, you find your body responding, reaching orgasm as you feel them shudder above you.";
        return returnText;
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return StartScene(player, other);
    }
}