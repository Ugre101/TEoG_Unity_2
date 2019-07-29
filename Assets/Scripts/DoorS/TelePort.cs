using UnityEngine;
using UnityEngine.Tilemaps;
public class TelePort : MonoBehaviour
{
    public MapEvents mapEvents;
    public GameObject toWorld;
    public Tilemap toMap;
    [Header("Optional landing platform")]
    public Tilemap toPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mapEvents.Teleport(toWorld, toMap,toPlatform == null ? null : toPlatform);
    }
}
