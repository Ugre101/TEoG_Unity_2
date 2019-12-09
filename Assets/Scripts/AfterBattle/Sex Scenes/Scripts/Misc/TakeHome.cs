using UnityEngine;

public class TakeHome : MonoBehaviour
{
    public Dorm dorm;
    private GameUI gameUI;
    private AfterBattleMain afterBattle;

    public void Start()
    {
        gameUI = GetComponentInParent<GameUI>();
        afterBattle = GetComponentInParent<AfterBattleMain>();
    }

    public void DoScene()
    {
        dorm.AddTo(afterBattle.enemies[0].transform.gameObject);
        Destroy(afterBattle.enemies[0].transform.gameObject);
        gameUI.Resume();
        // Needs testing
    }
}