using UnityEngine;
using UnityEngine.Tilemaps;

public class TelePortLocation
{
    private Vector3 landPlatform;
    public Tilemap Map { get; }
    public WorldMaps World { get; }
    private readonly MapEvents mapEvents;
    public CanTelePortTo CanTelePortTo { get; }

    public TelePortLocation(CanTelePortTo canTele, MapEvents mapEvents)
    {
        CanTelePortTo = canTele;
        Map = canTele.Map;
        landPlatform = canTele.LandPlatform;
        World = canTele.World;
        this.mapEvents = mapEvents;
    }

    public void TelePortTo()
    {
        CanTelePortTo.TeleportTo();
        mapEvents.Teleport(World, Map, landPlatform);
    }
}