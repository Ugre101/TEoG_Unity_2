using System.Collections.Generic;
using UnityEngine;

public enum Maps
{
    Start,
    ToVillage,
    Village,
    ToWitch,
    WitchWood,
    WitchHut,
    Forest,
    DeepForest
}

public class Map : MonoBehaviour
{
    [SerializeField] private Maps mapName = Maps.Start;

    [Tooltip("Amount of ememies to spawn.")]
    [SerializeField] private int amountOfEnemies = 6;

    [Header("Map enemies")]
    [Tooltip("Types of enemies that can spawn on this map.")]
    [SerializeField] private List<EnemyPrefab> enemies = new List<EnemyPrefab>();

    [Header("Bosses")]
    [SerializeField] private List<Boss> bosses = new List<Boss>();

    public Maps MapName => mapName;

    public List<EnemyPrefab> Enemies => enemies;
    public List<Boss> Bosses => bosses;
    public int EnemyCount => amountOfEnemies;
}