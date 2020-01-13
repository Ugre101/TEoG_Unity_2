using UnityEngine;
using UnityEngine.Tilemaps;

public class TelePort : MonoBehaviour
{
    [SerializeField] private MapEvents mapEvents = null;

    [SerializeField] private WorldMaps toWorld = WorldMaps.StartMap;

    [SerializeField] private Tilemap toMap = null;

    [Header("Optional landing platform")]
    [SerializeField] private Tilemap toPlatform = null;

    public void Start()
    {
        mapEvents = mapEvents != null ? mapEvents : MapEvents.GetMapEvents;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerMain.GetPlayer.tag))
        {
            if (toPlatform == null)
            {
                mapEvents.Teleport(toWorld, toMap);
            }
            else
            {
                mapEvents.Teleport(toWorld, toMap, toPlatform);
            }
        }
    }
}