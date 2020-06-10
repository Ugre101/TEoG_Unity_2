using UnityEngine;

[CreateAssetMenu(fileName = "Give Masc", menuName = ("Sex/Essence/Give Masc"))]
public class GiveMasc : EssScene
{
    public override bool CanDo(BasicChar basicChar)
    {
        return basicChar.EssGive() > 0;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);

        float toGive = player.EssGive();
        player.LoseMasc(toGive);
        other.Essence.Masc.Gain(toGive);
        return "Give masc" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}
