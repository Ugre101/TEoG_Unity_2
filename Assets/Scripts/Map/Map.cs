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
    [field: SerializeField]
    public Maps MapName { get; private set; } = Maps.Start;

    [field: Header("Map enemies")]
    [field: Tooltip("Types of enemies that can spawn on this map.")]
    [field: SerializeField]
    public List<EnemyPrefab> Enemies { get; private set; } = new List<EnemyPrefab>();

    [field: Tooltip("Amount of ememies to spawn.")]
    [field: SerializeField]
    public int EnemyCount { get; private set; } = 6;
}