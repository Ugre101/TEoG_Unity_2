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

    public override string StartScene(PlayerMain player, BasicChar other)
    {
        Dorm.GetDrom.MoveToDorm(other);
        TakenToDorm?.Invoke();
        other.RelationshipTracker.MoveFromTemp(player);
        player.RelationshipTracker.MoveFromTemp(other);
        return $"";
    }

    public override string ContinueScene(PlayerMain player, BasicChar other) => StartScene(player, other);

    public delegate void Taken();

    public static event Taken TakenToDorm;
}