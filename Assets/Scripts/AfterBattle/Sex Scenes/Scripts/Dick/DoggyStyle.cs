using UnityEngine;

[CreateAssetMenu(fileName = "Doggystyle", menuName = ("Sex/Dick/Doggystyle"))]
public class DoggyStyle : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        DickContainer dicks = player.SexualOrgans.Dicks;
        return $"Commanding {other.Identity.FirstName} to get down on their all fours you fuck HisHer from behind.\n " +
        $"Their {other.SexualOrgans.Vaginas.List[0].Race} pussy Tightness(player, enemies[EnemyIndex],) + to your +{dicks.BiggestSizeValue}" +
        $" {dicks.Biggest().Race} dick.";
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return $"You continue fucking them from behind. Their {other.SexualOrgans.Vaginas.List[0].Race} pussy Tightness(player, enemies[EnemyIndex], A) +to your {player.SexualOrgans.Dicks.BiggestSizeValue} dick.";
    }
}