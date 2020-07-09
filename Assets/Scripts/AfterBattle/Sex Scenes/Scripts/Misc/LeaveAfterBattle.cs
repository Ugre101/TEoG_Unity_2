using UnityEngine;

[CreateAssetMenu(fileName = "LeaveAfterBattle", menuName = ("Sex/Misc/LeaveAfterBattle"))]
public class LeaveAfterBattle : SexScenes
{
    public bool CanDo(BasicChar player) => true;

    public override bool CanDo(BasicChar player, BasicChar Other) => true;

    public override string StartScene(BasicChar player, BasicChar other)
    {
        GameManager.ReturnToLastState();
        return $"";
    }

    public override string ContinueScene(BasicChar player, BasicChar other) => StartScene(player, other);

    public override void ArousalGain(BasicChar player, BasicChar other)
    {
    }
}