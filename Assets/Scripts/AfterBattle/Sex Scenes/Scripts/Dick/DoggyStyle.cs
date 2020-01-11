using UnityEngine;

[CreateAssetMenu(fileName = "Doggystyle", menuName = ("Sex/Dick/Doggystyle"))]
public class DoggyStyle : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        System.Collections.Generic.List<Dick> dicks = player.SexualOrgans.Dicks;
        return $"Commanding {other.Identity.FirstName} to get down on their all fours you fuck HisHer from behind.\n " +
        $"Their {other.SexualOrgans.Vaginas[0].Race} pussy Tightness(player, enemies[EnemyIndex],) + to your +{dicks.BiggestSize()}" +
        $" {dicks.Biggest().Race} dick.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return $"You continue fucking them from behind. Their {other.SexualOrgans.Vaginas[0].Race} pussy Tightness(player, enemies[EnemyIndex], A) +to your {player.SexualOrgans.Dicks.BiggestSize()} dick.";
    }
}