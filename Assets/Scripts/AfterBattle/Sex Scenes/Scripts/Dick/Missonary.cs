using UnityEngine;

[CreateAssetMenu(fileName = "Missonary", menuName = ("Sex/Dick/Missonary"))]
public class Missonary : SexScenes
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        // TODO add his/her and tightness
        System.Collections.Generic.List<Dick> dicks = player.SexualOrgans.Dicks;
        return $"Positioning your opponent on{"his/her"} back you fuck {"hisher"} {other.SexualOrgans.Vaginas[0].Race} pussy with your {Settings.MorInch(dicks.Biggest())} {dicks[0].Race} dick.\n\nTheir pussy Tightness(player, enemies[EnemyIndex] to you.";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        ArousalGain(player, other);
        return $"You continue fucking HisHer(enemies[EnemyIndex]) {other.SexualOrgans.Vaginas[0].Race} pussy with your {Settings.MorInch(player.SexualOrgans.Dicks.Biggest())} {player.SexualOrgans.Dicks[0].Race} dick.";
    }
}