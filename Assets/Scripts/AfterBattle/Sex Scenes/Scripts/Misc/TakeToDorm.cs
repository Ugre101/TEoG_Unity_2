using UnityEngine;

[CreateAssetMenu(fileName = "TakeToDorm", menuName = ("Sex/Misc/TakeToDorm"))]
public class TakeToDorm : SexScenes
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        if (Other is EnemyPrefab ep)
        {
            return ep.CanTake;
        }
        return false;
    }

    public override string StartScene(BasicChar player, BasicChar other)
    {
        Dorm.AddToDorm(other);
        TakenToDorm?.Invoke();
        other.RelationshipTracker.MoveFromTemp(player);
        player.RelationshipTracker.MoveFromTemp(other);
        other.IfHaveHolderDestoryIt();
        return $"";
    }

    public override string ContinueScene(BasicChar player, BasicChar other) => StartScene(player, other);

    public override void ArousalGain(BasicChar player, BasicChar other)
    {
    }

    public delegate void Taken();

    public static event Taken TakenToDorm;
}