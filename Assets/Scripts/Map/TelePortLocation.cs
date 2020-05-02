using UnityEngine.Tilemaps;

public class TelePortLocation
{
    private readonly Tilemap landPlatform;
    public Tilemap Map { get; }
    public WorldMaps World { get; }
    private readonly MapEvents mapEvents;
    public CanTelePortTo CanTelePortTo { get; }

    public TelePortLocation(CanTelePortTo canTele, MapEvents mapEvents)
    {
        CanTelePortTo = canTele;
        Map = canTele.Map;
        landPlatform = canTele.LandPlatform;
        World = canTele.WorldMaps;
        this.mapEvents = mapEvents;
    }

    public void TelePortTo() => mapEvents.Teleport(World, Map, landPlatform);
}