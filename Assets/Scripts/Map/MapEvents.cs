using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class MapEvents : MonoBehaviour
{
    public delegate void WorldMapChange();
    public static event WorldMapChange worldMapChange;
    public playerMain player;

    public Tilemap CurrentMap;
    public WorldMaps ActiveMap;
    public GameObject CurrentWorld { get { return worldMaps.Find(m => m.map == ActiveMap).transform.gameObject; }}

    private List<WorldMap> worldMaps;
    private List<Map> maps { get { return new List<Map>(CurrentWorld.GetComponentsInChildren<Map>()); } }
    private void Awake()
    {
        worldMaps = new List<WorldMap>(GetComponentsInChildren<WorldMap>());
        Debug.Log(maps.Count);
    }
    public void Teleport(WorldMaps toWorld, Tilemap toMap, Tilemap teleportPlatform = null)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        ActiveMap = toWorld;
        CurrentWorld.SetActive(true);
        player.transform.position = teleportPlatform == null ? toMap.cellBounds.center : teleportPlatform.cellBounds.center;
        //CurrentWorld = toWorld;
        CurrentMap = toMap;
        worldMapChange?.Invoke();
    }
    public void MapChange(Tilemap newMap)
    {
        CurrentMap = newMap;
        worldMapChange?.Invoke();

    }
    public void WorldChange(WorldMaps newWorld, Tilemap newMap)
    {
       // CurrentWorld = newWorld;
        ActiveMap = newWorld;
        CurrentMap = newMap;
        worldMapChange?.Invoke();
    }
    public void Load(PosSave save)
    {
        ActiveMap = save.world;
        CurrentMap = maps.Find(m => m.name == save.map).transform.gameObject.GetComponent<Tilemap>();
        worldMapChange?.Invoke();
        player.transform.position = save.pos;
        Debug.Log(ActiveMap + " " + CurrentMap);
    }
}
