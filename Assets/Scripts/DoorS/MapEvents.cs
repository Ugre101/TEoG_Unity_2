using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class MapEvents : MonoBehaviour
{
    public delegate void WorldMapChange();
    public static event WorldMapChange worldMapChange;
    public playerMain player;

    public GameObject CurrentWorld;
    public Tilemap CurrentMap;
    public List<WorldMap> WorldMaps;
    public WorldMaps ActiveMap;
    public void Teleport(GameObject toWorld, Tilemap toMap, Tilemap teleportPlatform = null)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        toWorld.SetActive(true);
        player.transform.position = teleportPlatform == null ? toMap.cellBounds.center : teleportPlatform.cellBounds.center;
        CurrentWorld = toWorld;
        CurrentMap = toMap;
        worldMapChange();
    }
    public void MapChange(Tilemap newMap)
    {
        CurrentMap = newMap;
        worldMapChange?.Invoke();

    }
    public void WorldChange(GameObject newWorld, Tilemap newMap)
    {
        CurrentWorld = newWorld;
        CurrentMap = newMap;
        worldMapChange();
    }
}
