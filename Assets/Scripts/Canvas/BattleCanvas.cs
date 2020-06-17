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

    public void StartCombat(CharHolder enemy) => StartCombat(enemy.BasicChar);

    /// <summary>Team of enemies </summary>
    /// <param name="enemies"></param>
    public void StartCombat(params BasicChar[] enemies)
    {
        GameManager.SetCurState(GameState.Battle);
        Battle.transform.SleepChildren();
        transform.SleepChildren(Battle.transform);
        combatMain.SetUpCombat(enemies);
        inBattle = true;
    }

    public void LeaveCombat(GameState gameState)
    {
        if (inBattle && (gameState != GameState.Battle))
        {
            inBattle = false;
            transform.SleepChildren();
        }
    }
}