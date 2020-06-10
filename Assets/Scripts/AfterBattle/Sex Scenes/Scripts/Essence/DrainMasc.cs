using UnityEngine;

[CreateAssetMenu(fileName = "Drain masc", menuName = ("Sex/Essence/Drain masc"))]
public class DrainMasc : EssScene
{
    public override bool CanDo(BasicChar target) => target.CanDrainMasc();

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        float have = other.LoseMasc(player.EssenceDrain(other));
        player.Essence.Masc.Gain(have);
        return "Drain masc" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other) => StartScene(player, other);
}