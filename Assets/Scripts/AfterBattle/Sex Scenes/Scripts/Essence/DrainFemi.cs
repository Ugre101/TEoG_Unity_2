using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : EssScene
{
    public override bool CanDo(BasicChar target) => target.CanDrainFemi();

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        float have = other.LoseFemi(player.EssenceDrain(other));
        player.Essence.Femi.Gain(have);
        return "Drain femi\n" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other) => StartScene(player, other);
}