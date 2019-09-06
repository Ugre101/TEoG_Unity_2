using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEvents : MonoBehaviour
{
    public delegate void worldMapChange();

    public static event worldMapChange WorldMapChange;

    public playerMain player;

    public Tilemap CurrentMap;
    public WorldMaps ActiveMap;
    public GameObject CurrentWorld { get { return worldMaps.Find(m => m.map == ActiveMap).transform.gameObject; } }

    [SerializeField]
    private List<WorldMap> worldMaps;

    private List<Transform> Maps { get { return new List<Transform>(CurrentWorld.GetComponentsInChildren<Transform>()); } }

    private void Awake()
    {
        worldMaps = new List<WorldMap>(GetComponentsInChildren<WorldMap>());
    }

    public void Teleport(WorldMaps toWorld, Tilemap toMap, Tilemap teleportPlatform = null)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        ActiveMap = toWorld;
        CurrentWorld.SetActive(true);
        player.transform.position = teleportPlatform == null ? toMap.cellBounds.center : teleportPlatform.cellBounds.center;
        //CurrentWorld = toWorld;
        CurrentMap = toMap;
        WorldMapChange?.Invoke();
    }

    public void MapChange(Tilemap newMap)
    {
        CurrentMap = newMap;
        WorldMapChange?.Invoke();
    }

    public void WorldChange(WorldMaps newWorld, Tilemap newMap)
    {
        // CurrentWorld = newWorld;
        ActiveMap = newWorld;
        CurrentMap = newMap;
        WorldMapChange?.Invoke();
    }

    public void Load(PosSave save)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        ActiveMap = save.world;
        CurrentWorld.SetActive(true);
        CurrentMap = Maps.Find(m => m.name == save.map).transform.gameObject.GetComponent<Tilemap>();
        WorldMapChange?.Invoke();
        player.transform.position = save.pos;
        Debug.Log(ActiveMap + " " + CurrentMap);
    }
}