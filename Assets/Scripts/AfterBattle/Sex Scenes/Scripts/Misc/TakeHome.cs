using UnityEngine;

public class TakeHome : MonoBehaviour
{
    private CanvasMain gameUI;
    private AfterBattleMain afterBattle;

    public void Start()
    {
        gameUI = GetComponentInParent<CanvasMain>();
        afterBattle = GetComponentInParent<AfterBattleMain>();
    }

    public void DoScene()
    {
        Dorm.AddToDorm(afterBattle.Target);
        // Destroy(afterBattle.Target.transform.gameObject);
        // TODO get rid of charholder
        gameUI.Resume();
        // Needs testing
    }
}