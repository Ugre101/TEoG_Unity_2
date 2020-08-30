using UnityEngine;

[CreateAssetMenu(fileName = "Drain masc", menuName = ("Sex/Essence/Drain masc"))]
public class DrainMasc : EssScene
{
    public override bool CanDo(BasicChar target) => target.CanDrainMasc();

    public override string StartScene(BasicChar player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        player.DrainMasc(other);
        return "Drain masc" + drainChange.BothChanges;
    }

    public override string ContinueScene(BasicChar player, BasicChar other) => StartScene(player, other);
}
