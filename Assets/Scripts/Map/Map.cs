﻿using System.Collections.Generic;
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
        foreach (AssingEnemy enemyPrefab in Enemies)
        {
            if (enemyPrefab == null)
            {
                Debug.LogWarning(name + " is missing enemys refs");
                break;
            }
        }
        foreach (AssingBoss boss in Bosses)
        {
            if (boss == null)
            {
                Debug.LogWarning(name + " is missing boss refs");
                break;
            }
        }
    }
}