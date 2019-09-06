using System.Collections.Generic;
using UnityEngine;

public class CombatEnemies : MonoBehaviour
{
    public List<EnemyPrefab> _enemies = new List<EnemyPrefab>();
    public CombatButtons combatButtons;
    public EnemyTeam enemyTeam;

    public void AddEnemy(EnemyPrefab enemy)
    {
        combatButtons._enemies.Add(enemy);
        _enemies.Add(enemy);
        enemyTeam.StartFight(_enemies);
    }

    private void OnDisable()
    {
        _enemies.Clear();
    }
}