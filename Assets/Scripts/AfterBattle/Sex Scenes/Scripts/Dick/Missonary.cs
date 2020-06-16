using UnityEngine;

[CreateAssetMenu(fileName = "Missonary", menuName = ("Sex/Dick/Missonary"))]
public class Missonary : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        // TODO add and tightness
        DickContainer dicks = player.SexualOrgans.Dicks;
        return $"Positioning your opponent on {other.HisHer()} back you fuck {"hisher"} {other.SexualOrgans.Vaginas.List[0].Race} pussy with your {Settings.MorInch(dicks.BiggestSizeValue)} {dicks.Biggest().Race} dick.\n\nTheir pussy Tightness(player, enemies[EnemyIndex] to you.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return $"You continue fucking HisHer(enemies[EnemyIndex]) {other.SexualOrgans.Vaginas.List[0].Race} pussy with your {Settings.MorInch(player.SexualOrgans.Dicks.BiggestSizeValue)} {player.SexualOrgans.Dicks.Biggest().Race} dick.";
    }
}