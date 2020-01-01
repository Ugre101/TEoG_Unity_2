using UnityEngine;

public class TakeHome : MonoBehaviour
{
    public Dorm dorm;
    private CanvasMain gameUI;
    private AfterBattleMain afterBattle;

    public void Start()
    {
        gameUI = GetComponentInParent<CanvasMain>();
        afterBattle = GetComponentInParent<AfterBattleMain>();
    }

    public void DoScene()
    {
        dorm.AddTo(afterBattle.Target.transform.gameObject);
        Destroy(afterBattle.Target.transform.gameObject);
        gameUI.Resume();
        // Needs testing
    }
}