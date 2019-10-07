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

    private List<Transform> Children { get { return new List<Transform>(CurrentWorld.GetComponentsInChildren<Transform>()); } }
    #region mapScript
    private List<Map> GetMaps => new List<Map>(CurrentWorld.GetComponentsInChildren<Map>());
    private Map lastMap;
    private bool mapDirty = true;

    public Map CurMapScript
    {
        get
        {
            if (mapDirty)
            {
                lastMap = GetMaps.Find(m => m.name == CurrentMap.name);
                mapDirty = false;
            }
            return lastMap;
        }
    }
    #endregion
    private void Awake()
    {
        worldMaps = new List<WorldMap>(GetComponentsInChildren<WorldMap>());
        mapDirty = true;
    }

    public void Teleport(WorldMaps toWorld, Tilemap toMap, Tilemap teleportPlatform = null)
    {
        mapDirty = true;
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
        mapDirty = true;
        CurrentMap = newMap;
        WorldMapChange?.Invoke();
    }

    public void WorldChange(WorldMaps newWorld, Tilemap newMap)
    {
        mapDirty = true;
        // CurrentWorld = newWorld;
        ActiveMap = newWorld;
        CurrentMap = newMap;
        WorldMapChange?.Invoke();
    }

    public void Load(PosSave save)
    {
        mapDirty = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        ActiveMap = save.world;
        CurrentWorld.SetActive(true);
        CurrentMap = Children.Find(m => m.name == save.map).transform.gameObject.GetComponent<Tilemap>();
        WorldMapChange?.Invoke();
        player.transform.position = save.pos;
        Debug.Log(ActiveMap + " " + CurrentMap);
    }
}