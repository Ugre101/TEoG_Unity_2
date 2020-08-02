using UnityEngine;

[CreateAssetMenu(fileName = "Missonary", menuName = ("Sex/Dick/Missonary"))]
public class Missonary : SexScenes
{
    public override string StartScene(BasicChar player, BasicChar other)
    {
        // TODO add and tightness
        DickContainer dicks = player.SexualOrgans.Dicks;
        return $"Positioning your opponent on {other.HisHer()} back you fuck {"hisher"} {other.SexualOrgans.Vaginas.List[0].Race} pussy with your {dicks.BiggestSizeValue.MorInch()} {dicks.Biggest().Race} dick.\n\nTheir pussy Tightness(player, enemies[EnemyIndex] to you.";
    }

    public override string ContinueScene(BasicChar player, BasicChar other)
    {
        return $"You continue fucking HisHer(enemies[EnemyIndex]) {other.SexualOrgans.Vaginas.List[0].Race} pussy with your {player.SexualOrgans.Dicks.BiggestSizeValue.MorInch()} {player.SexualOrgans.Dicks.Biggest().Race} dick.";
    }
}