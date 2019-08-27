using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Doggystyle", menuName = ("Sex/Dick/Doggystyle"))]
public class DoggyStyle : SexScenes
{
    public override string StartScene(playerMain player, BasicChar other)
    {
        return $"Commanding {other.firstName} to get down on their all fours you fuck HisHer from behind.\n " +
        $"Their {other.Vaginas[0].Race} pussy Tightness(player, enemies[EnemyIndex],) + to your +{BiggestDick(player)}" +
        $" {player.Dicks[0].Race} dick.";
    }
    public override string ContinueScene(playerMain player, BasicChar other)
    {
        return $"You continue fucking them from behind. Their {other.Vaginas[0].Race} pussy Tightness(player, enemies[EnemyIndex], A) +to your {BiggestDick(player)} dick.";
    }
}