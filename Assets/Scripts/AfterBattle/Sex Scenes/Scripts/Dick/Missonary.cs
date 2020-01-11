using UnityEngine;

[CreateAssetMenu(fileName = "Missonary", menuName = ("Sex/Dick/Missonary"))]
public class Missonary : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        // TODO add and tightness
        System.Collections.Generic.List<Dick> dicks = player.SexualOrgans.Dicks;
        return $"Positioning your opponent on {other.HisHer()} back you fuck {"hisher"} {other.SexualOrgans.Vaginas[0].Race} pussy with your {Settings.MorInch(dicks.BiggestSize())} {dicks.Biggest().Race} dick.\n\nTheir pussy Tightness(player, enemies[EnemyIndex] to you.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return $"You continue fucking HisHer(enemies[EnemyIndex]) {other.SexualOrgans.Vaginas[0].Race} pussy with your {Settings.MorInch(player.SexualOrgans.Dicks.BiggestSize())} {player.SexualOrgans.Dicks[0].Race} dick.";
    }
}