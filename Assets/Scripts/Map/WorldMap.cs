using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WorldMaps
{
    #region old Enums don't reorder will break stuff

    StartMap,
    SecondMap,
    Home,
    Mountain,

    #endregion old Enums don't reorder will break stuff
}

public class WorldMap : MonoBehaviour
{
    private List<Map> maps = new List<Map>();
    [SerializeField] private WorldMaps worldMap = WorldMaps.StartMap;
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