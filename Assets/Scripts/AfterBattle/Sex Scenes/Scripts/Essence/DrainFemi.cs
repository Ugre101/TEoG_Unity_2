using UnityEngine;

[CreateAssetMenu(fileName = "Drain femi", menuName = ("Sex/Essence/Drain femi"))]
public class DrainFemi : EssScene
{
    public override bool CanDo(BasicChar target) => target.CanDrainFemi();

    public override string StartScene(BasicChar player, BasicChar other)
    {
        DrainChangeHandler drainChange = new DrainChangeHandler(player, other);
        player.DrainFemi(other);
        return "Drain femi\n" + drainChange.BothChanges;
    }

    public override string ContinueScene(BasicChar player, BasicChar other) => StartScene(player, other);
}