using System.Collections.Generic;
using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField] private BigPanel Battle = null;
    [SerializeField] private CombatMain combatMain = null;
    private bool inBattle = false;

    private void Start()
    {
        Movement.TriggerEnemy += StartCombat;
        GameManager.GameStateChangeEvent += LeaveCombat;
    }

    private void StartCombat(CharHolder enemy) => StartCombat(enemy.BasicChar);
    private void StartCombat(BasicChar enemy) => StartCombat(new List<BasicChar>() { enemy });
    /// <summary>Team of enemies </summary>
    /// <param name="enemies"></param>
    private void StartCombat(List<BasicChar> enemies)
    {
        GameManager.SetCurState(GameState.Battle);
        Battle.transform.SleepChildren();
        transform.SleepChildren(Battle.transform);
        combatMain.SetUpCombat(enemies);
        inBattle = true;
    }

    private void LeaveCombat(GameState gameState)
    {
        if (!inBattle || (gameState == GameState.Battle)) return;
        
        inBattle = false;
        transform.SleepChildren();
    }
}