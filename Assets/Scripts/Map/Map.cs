using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private string mapName = string.Empty;

    [Tooltip("Amount of ememies to spawn.")]
    [SerializeField] private int amountOfEnemies = 6;

    [Header("Map enemies")]
    [Tooltip("Types of enemies that can spawn on this map.")]
    [SerializeField] private List<EnemyHolder> enemies = new List<EnemyHolder>();

    [Header("Bosses")]
    [SerializeField] private List<BossHolder> bosses = new List<BossHolder>();

    public string MapTitle => mapName;

    public List<EnemyHolder> Enemies => enemies;
    public List<BossHolder> Bosses => bosses;
    public int EnemyCount => amountOfEnemies;

    private void Start()
    {
        foreach (EnemyHolder enemyPrefab in Enemies)
        {
            if (enemyPrefab == null)
            {
                Debug.LogWarning(name + " is missing enemys refs");
                break;
            }
        }
        foreach (BossHolder boss in Bosses)
        {
            if (boss == null)
            {
                Debug.LogWarning(name + " is missing boss refs");
                break;
            }
        }
    }
}