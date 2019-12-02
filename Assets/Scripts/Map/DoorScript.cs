using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    private MapEvents mapEvents;
    public Tilemap _fronTilemap, _toTilemap;

    private void Start()
    {
        mapEvents = this.GetComponentInParent<MapEvents>();
    }

    // Simple door script, handles trigger to switch tilemap for movement and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (mapEvents.CurrentMap == _fronTilemap)
            {
                mapEvents.MapChange(_toTilemap);
            }
            else
            {
                mapEvents.MapChange(_fronTilemap);
            }
        }
    }
}