using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TakeHome : MonoBehaviour
{
    public Dorm dorm;
    private GameUI gameUI;
    private AfterBattleActions afterBattle;

    public void Start()
    {
        gameUI = GetComponentInParent<GameUI>();
        afterBattle = GetComponentInParent<AfterBattleActions>();
    }
    public void DoScene()
    {
        dorm.AddTo(afterBattle.enemies[0].transform.gameObject);
        Destroy(afterBattle.enemies[0].transform.gameObject);
        gameUI.Resume();
        // Needs testing
    }
}
