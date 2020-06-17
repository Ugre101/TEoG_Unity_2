using UnityEngine;

[CreateAssetMenu(fileName = "LeaveAfterBattle", menuName = ("Sex/Misc/LeaveAfterBattle"))]
public class LeaveAfterBattle : SexScenes
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        return true;
    }

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        GameManager.ReturnToLastState();
        return $"";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other) => StartScene(player, other);

    public override void ArousalGain(PlayerMain player, BasicChar other)
    {
    }
}