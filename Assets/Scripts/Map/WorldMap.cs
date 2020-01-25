using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WorldMaps
{
    StartMap,
    SecondMap,
    Home
}

public class WorldMap : MonoBehaviour
{
    private List<Map> maps = new List<Map>();
    [SerializeField] private WorldMaps worldMap;
    public WorldMaps World => worldMap;

    public List<Map> Maps
    {
        get
        {
            if (maps.Count < 1)
            {
                maps = GetComponentsInChildren<Map>().ToList();
            }
            return maps;
        }
    }
}