using UnityEngine;
using UnityEngine.Tilemaps;
public class MapEvents : MonoBehaviour
{
    public delegate void WorldMapChange();
    public static event WorldMapChange worldMapChange;
    public playerMain player;

    public GameObject CurrentWorld;
    public Tilemap CurrentMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Teleport(GameObject toWorld, Tilemap toMap, Tilemap teleportPlatform = null)
    {
        CurrentWorld.SetActive(false);
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
