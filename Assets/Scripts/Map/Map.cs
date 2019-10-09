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
    public string map;
    public string testMap { get { return this.transform.name; } }

    [Header("Map enemies")]
    [Tooltip("Types of enemies that can spawn on this map.")]
    [SerializeField]
    private List<GameObject> enemies;

    public List<GameObject> Enemies => enemies;
    [Tooltip("Amount of ememies to spawn.")]
    [SerializeField]
    private int enemyCount = 6;

    public int EnemyCount => enemyCount;
}