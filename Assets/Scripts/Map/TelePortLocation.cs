using UnityEngine;
using UnityEngine.Tilemaps;

public class TelePortLocation
{
    private readonly Vector3 landPlatform;
    private Tilemap Map => CanTelePortTo.Map;
    public WorldMaps World => CanTelePortTo.World;
    private readonly MapEvents mapEvents;

    public CanTelePortTo CanTelePortTo { get; }

    public TelePortLocation(CanTelePortTo canTele, MapEvents mapEvents)
    {
        CanTelePortTo = canTele;
        landPlatform = canTele.LandCordinations;
        this.mapEvents = mapEvents;
    }

    public void TelePortTo()
    {
        CanTelePortTo.TeleportTo();
        mapEvents.Teleport(World, Map, landPlatform);
    }
}