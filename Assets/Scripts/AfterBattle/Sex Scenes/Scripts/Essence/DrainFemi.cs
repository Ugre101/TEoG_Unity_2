using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : EssScene
{
    public override bool CanDo(EnemyPrefab target) => target.CanDrainFemi();

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float have = other.LoseFemi(player.EssenceDrain(other));
        player.Essence.Femi.Gain(have);
        return "Drain femi";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other) => StartScene(player, other);
}