using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEvents : MonoBehaviour
{
    public static MapEvents GetMapEvents { get; private set; }

    public static event Action<Tilemap> WorldMapChange;

    private PlayerMain Player => PlayerMain.GetPlayer;

    public static Tilemap CurrentMap { get; private set; }

    [SerializeField] private Tilemap startMap = null;

    public static WorldMaps ActiveMap { get; private set; }
    private WorldMap CurrentWorld => worldMaps.Find(m => m.World == ActiveMap);

    [SerializeField] private List<WorldMap> worldMaps = new List<WorldMap>();

    private List<Transform> lastChildren = new List<Transform>();

    private List<Transform> WorldChildren
    {
        get
        {
            if (lastChildren.Count < 1)
            {
                lastChildren = new List<Transform>(CurrentWorld.GetComponentsInChildren<Transform>());
            }
            return lastChildren;
        }
    }

    #region mapScript

    private List<Map> lastMaps = new List<Map>();

    private List<Map> GetMaps
    {
        get
        {
            if (lastMaps.Count < 1)
            {
                lastMaps = CurrentWorld.Maps;
            }
            return lastMaps;
        }
    }

    private Map lastMap;
    private static bool mapDirty = true;

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

    #endregion mapScript

    private void Awake()
    {
        if (GetMapEvents == null)
        {
            GetMapEvents = this;
        }
        else if (GetMapEvents != this)
        {
            Destroy(gameObject);
        }
        if (startMap != null)
        {
            CurrentMap = startMap;
        }
        else
        {
            // Yolo
            Debug.LogError("MapEvents had to pick random startmap, not good...");
            CurrentMap = GetComponent<Tilemap>();
        }
        worldMaps = new List<WorldMap>(GetComponentsInChildren<WorldMap>());
        mapDirty = true;
    }

    public static void MapChange(Tilemap newMap)
    {
        mapDirty = true;
        CurrentMap = newMap;
        WorldMapChange?.Invoke(CurrentMap);
    }

    public void WorldChange(WorldMaps newWorld, Tilemap newMap)
    {
        transform.SleepChildren();
        ActiveMap = newWorld;
        CurrentWorld.gameObject.SetActive(true);
        MapChange(newMap);
    }

    public void Teleport(WorldMaps toWorld, Tilemap toMap)
    {
        WorldChange(toWorld, toMap);
        Player.transform.position = toMap.cellBounds.center;
    }

    public void Teleport(WorldMaps toWorld, Tilemap toMap, Tilemap teleportPlatform)
    {
        WorldChange(toWorld, toMap);
        Player.transform.position = teleportPlatform == null ? toMap.cellBounds.center : teleportPlatform.cellBounds.center;
    }

    public void Load(PosSave save)
    {
        WorldChange(save.World, WorldChildren.Find(m => m.name == save.Map).transform.gameObject.GetComponent<Tilemap>());
        Player.transform.position = save.Pos;
    }
}