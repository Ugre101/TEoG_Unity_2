using UnityEngine;

[CreateAssetMenu(fileName = "Give femi", menuName = ("Sex/Essence/Give femi"))]
public class GiveFemi : EssScene
{
    public override bool CanDo(BasicChar basicChar)
    {
        return basicChar.EssGive() > 0;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        float toGive = player.EssGive();
        player.LoseFemi(toGive);
        other.Essence.Femi.Gain(toGive);
        return "Give femi" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}