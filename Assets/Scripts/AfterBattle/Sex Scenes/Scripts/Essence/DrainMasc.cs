using UnityEngine;

[CreateAssetMenu(fileName = "Drain masc", menuName = ("Sex/Essence/Drain masc"))]
public class DrainMasc : EssScene
{
    public override bool CanDo(BasicChar target)
    {
        return target.CanDrainMasc;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float have = other.LoseMasc(ToDrain(player));
        player.Masc.Gain(have);
        return "Drain masc";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}