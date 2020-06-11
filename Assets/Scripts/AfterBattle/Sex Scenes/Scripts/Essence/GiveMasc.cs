using UnityEngine;

[CreateAssetMenu(fileName = "Give Masc", menuName = ("Sex/Essence/Give Masc"))]
public class GiveMasc : EssScene
{
    public override bool CanDo(BasicChar basicChar)
    {
        return basicChar.GiveEssence() > 0;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        player.GiveMasc(other);
        return "Give masc" + drainChange.BothChanges;
    }

    public override string ContinueScene(PlayerMain player, BasicChar other)
    {
        return StartScene(player, other);
    }
}