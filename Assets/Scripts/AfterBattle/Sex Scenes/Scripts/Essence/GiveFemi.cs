using UnityEngine;

[CreateAssetMenu(fileName = "Give femi", menuName = ("Sex/Essence/Give femi"))]
public class GiveFemi : EssScene
{
    public override bool CanDo(BasicChar basicChar)
    {
        return basicChar.GiveEssence() > 0;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        player.GiveFemi(other);
        return "Give femi" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}