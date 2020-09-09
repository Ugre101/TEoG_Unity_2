using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private string mapName = string.Empty;

    [Tooltip("Amount of ememies to spawn.")]
    [SerializeField] private int amountOfEnemies = 6;

    [Header("Map enemies")]
    [Tooltip("Types of enemies that can spawn on this map.")]
    [SerializeField] private List<AssingEnemy> enemies = new List<AssingEnemy>();

    [Header("Bosses")]
    [SerializeField] private List<AssingBoss> bosses = new List<AssingBoss>();

    public string MapTitle => mapName;

    public List<AssingEnemy> Enemies => enemies;
    public List<AssingBoss> Bosses => bosses;
    public int EnemyCount => amountOfEnemies;

    private void Start()
    {
        if (Enemies.Any(enemyPrefab => enemyPrefab == null))
        {
            Debug.LogWarning(name + " is missing enemys refs");
        }

        if (Bosses.Any(boss => boss == null))
        {
            Debug.LogWarning(name + " is missing boss refs");
        }
    }
}