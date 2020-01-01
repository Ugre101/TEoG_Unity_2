using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : EssScene
{
    public override string StartScene(PlayerMain player, BasicChar other)
    {
        float have = other.LoseFemi(ToDrain(player));
        player.Femi.Gain(have);
        return "Drain femi";
    }
    public override bool CanDo(BasicChar target)
    {
        return target.CanDrainFemi;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}